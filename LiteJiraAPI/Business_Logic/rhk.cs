using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Net;
using static LJAPI.Business_Logic.RateHawkClass;
using static QRCoder.PayloadGenerator.ShadowSocksConfig;

namespace LJAPI.Business_Logic
{
    public class RateHawkAPI
    {
        public RateHawkAPI() { }

        public const string APIName = "RTHWK";
        public const string APISupplierName = "RTHWK";
        public const string APISupplierID = "53";

        List<string> ratehawkapi_bookingerrorList = new List<string>
        {
            "3ds", "block", "book_limit", "booking_finish_did_not_succeed", "charge",
            "decoding_json", "endpoint_exceeded_limit", "endpoint_not_active",
            "endpoint_not_found", "incorrect_credentials", "invalid_auth_header",
            "invalid_params", "lock", "no_auth_header", "not_allowed",
            "not_allowed_host", "order_not_found", "overdue_debt", "provider",
            "soldout", "unexpected_method"
        };

        List<string> ratehawkapi_bookinginprocessList = new List<string>
        {
            "processing", "timeout", "unknown", "5xx"
        };

        public string WebsiteIPAddress = "127.0.0.1";
        public string RateHawk_defaultCurrency = "USD";

        private static string APIFolderPath = "~/ApiRequestResponse/RateHawk/";

        public int FreeCancellationBufferPeriod = 2;
        public static string SendHttpRequest(string endpoint, string method = "POST", string body = null, Dictionary<string, string> headers = null, string contentType = "application/json")
        {
            string responseStr = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
                request.Method = method;
                request.ContentType = contentType;

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        if (header.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase))
                            request.Headers[header.Key] = header.Value;
                        else
                            request.Headers.Add(header.Key, header.Value);
                    }
                }

                if (!string.IsNullOrEmpty(body) && (method == "POST" || method == "PUT" || method == "PATCH"))
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(body);
                    }
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();
                    if (response.ContentEncoding.ToLower().Contains("gzip"))
                        responseStream = new System.IO.Compression.GZipStream(responseStream, System.IO.Compression.CompressionMode.Decompress);
                    else if (response.ContentEncoding.ToLower().Contains("deflate"))
                        responseStream = new System.IO.Compression.DeflateStream(responseStream, System.IO.Compression.CompressionMode.Decompress);

                    using (var reader = new StreamReader(responseStream))
                    {
                        responseStr = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException webex)
            {
                if (webex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)webex.Response)
                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                    {
                        responseStr = reader.ReadToEnd(); // can also log the error details here
                    }
                }
                else
                {
                    responseStr = webex.Message;
                }
            }
            return responseStr;
        }

        public RateHawkClass.HotelSearchClass.Root SearchByRegion(HotelSearchRequest SearchReq, string RateHawkRegionId, Dictionary<string, string> ApiC, string FolderName)
        {
            var Result = new RateHawkClass.HotelSearchClass.Root();



            // 1. Build your body as before
            var requestBody = new
            {
                checkin = SearchReq.CheckInDate.ToString("yyyy-MM-dd"),
                checkout = SearchReq.CheckOutDate.ToString("yyyy-MM-dd"),
                residency = SearchReq.Nationality,
                language = "en",
                guests = "1",
                region_id = RateHawkRegionId,
                currency = RateHawk_defaultCurrency
            };
            string bodyStr = JsonConvert.SerializeObject(requestBody);

            // 2. Compose headers
            string credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{ApiC["UserName"]}:{ApiC["Password"]}"));
            var headers = new Dictionary<string, string>
{
    { "Authorization", $"Basic {credentials}" }
    // Add other headers if needed
};

            // 3. Call the method
            string responseJson = SendHttpRequest(ApiC["RegionwiseSearch"], "POST", bodyStr, headers, "application/json");

            // 4. Deserialize or process the response
            var result = JsonConvert.DeserializeObject<RateHawkClass.HotelSearchClass.Root>(responseJson);

            return Result;
        }

        public RateHawkClass.RateHawkHotelDetail.Root SearchByHotelID(HotelDetailRequest SearchReq, string Hotelid, Dictionary<string, string> ApiC, string FolderName)
        {
            var Result = new RateHawkHotelDetail.Root();



            // 1. Build your body as before
            var requestBody = new
            {
                checkin = SearchReq.CheckInDate.ToString("yyyy-MM-dd"),
                checkout = SearchReq.CheckOutDate.ToString("yyyy-MM-dd"),
                residency = SearchReq.Nationality,
                language = "en",
                guests = "1",
                ids = new List<string>() { Hotelid },
                currency = RateHawk_defaultCurrency
            };
            string bodyStr = JsonConvert.SerializeObject(requestBody);

            // 2. Compose headers
            string credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{ApiC["UserName"]}:{ApiC["Password"]}"));
            var headers = new Dictionary<string, string>
{
    { "Authorization", $"Basic {credentials}" }
    // Add other headers if needed
};

            // 3. Call the method
            string responseJson = SendHttpRequest(ApiC["RegionwiseSearch"], "POST", bodyStr, headers, "application/json");

            // 4. Deserialize or process the response
            var result = JsonConvert.DeserializeObject<RateHawkClass.RateHawkHotelDetail.Root>(responseJson);

            return Result;
        }

        public List<HotelList> HotelSearch(HotelSearchRequest SearchReq, Dictionary<string, string> ApiCredentials, string FolderName, Dictionary<string, int> ProviderHotelCodes, Dictionary<string, dynamic> RateHawkStaticData, RateHawkClass.RegionSearchClass.Root HotelSearchResponse, Dictionary<int, string> DicSuppilerAliasName, List<HotelDumpclass.Root> RateHawkHotelDumpDataHotelList)
        {
            List<HotelList> LstHotelList = new List<HotelList>();
            DataTable Dt_MarkUpDetails = new DataTable();
            bool IsCorporate = false;
            bool IsMarkValueOrPercentage_ = false;
            decimal MarkUpVal_ = 0;
            Dictionary<string, HotelDumpclass.Root> hotelStaticData = new Dictionary<string, HotelDumpclass.Root>();
            try
            {
                if (HotelSearchResponse.data != null && HotelSearchResponse.data.hotels != null)
                {

                    //DataTable IsCorporatedt = GetAgentIsCorporate(HotelSearchRequestobj.AgentId);
                    //if (IsCorporatedt != null && IsCorporatedt.Rows.Count != 0)
                    //{
                    //    IsCorporate = Convert.ToInt32(IsCorporatedt.Rows[0]["IsCorporate"]).Equals(0);
                    //}
                    //if (IsCorporate == true)
                    //{
                    //    Dt_MarkUpDetails = GetCorprateSupplierTypeMarkUp("9", HotelSearchRequestobj.AgentId);
                    //}
                    //else
                    //{
                    //    Dt_MarkUpDetails = B2CBookingBLL.GetMarkDetailsBySupplierType_NEW("9", HotelSearchRequestobj.AgentId != 0 ? "B2B" : "B2C", HotelSearchRequestobj.AgentId);
                    //}
                    //Dt_MarkUpDetails = B2CBookingBLL.GetMarkDetailsBySupplierType_NEW("9", HotelSearchRequestobj.AgentId != 0 ? "B2B" : "B2C", HotelSearchRequestobj.AgentId);

                    //if (Dt_MarkUpDetails.Rows.Count != 0)
                    //{
                    //    if (Dt_MarkUpDetails.Rows[0]["MarkupType"].ToString() != "")
                    //    {
                    //        IsMarkValueOrPercentage_ = Convert.ToBoolean(int.Parse(Dt_MarkUpDetails.Rows[0]["MarkupType"].ToString()));
                    //    }
                    //    if (Dt_MarkUpDetails.Rows[0]["MarkupValue"].ToString() != "")
                    //    {
                    //        MarkUpVal_ = Convert.ToDecimal(Dt_MarkUpDetails.Rows[0]["MarkupValue"].ToString());
                    //    }
                    //}
                    // var ss = HotelResultList.AsEnumerable().Where(x => x.id == "aldar_hotel").Select(x=>x.id).ToList().FirstOrDefault();
                    var HotelResultList = HotelSearchResponse.data.hotels;
                    foreach (var item in HotelResultList)
                    {
                        if (hotelStaticData.ContainsKey(item.id))
                        {
                            var hotelinfo = hotelStaticData[item.id];
                            List<HotelImageGallary> LstHotelImageGallary = new List<HotelImageGallary>();
                            List<HotelAminities> LstHotelAminities = new List<HotelAminities>();
                            if (hotelinfo.images != null && hotelinfo.images.Count > 0)
                            {
                                LstHotelImageGallary = hotelinfo.images.Select(g => new HotelImageGallary()
                                { HotelName = hotelinfo.name, ImageActualPath = g, ImagePath = g, ImageThumPath = g, HotelId = item.id, ImageName = hotelinfo.name }).ToList();
                            }
                            var ObjHotelList = new HotelList();
                            ObjHotelList.HotelIndex = "0";
                            ObjHotelList.HotelName = hotelinfo.name;
                            ObjHotelList.HotelId = item.id.ToString();
                            ObjHotelList.Address = hotelinfo.address;
                            ObjHotelList.Category = Convert.ToString(hotelinfo.star_rating);
                            ObjHotelList.CheckInTime = hotelinfo.check_in_time;
                            ObjHotelList.CheckOutTime = hotelinfo.check_out_time;
                            ObjHotelList.CurrencyName = "USD";//dsHotelult_Dt.Rows[0]["currencyShort"].ToString();
                            ObjHotelList.hotelId_service = APIName + "~" + item.id;
                            ObjHotelList.rate_supplier = APIName;
                            ObjHotelList.HotelIdsString = APIName + "~" + item.id;
                            if (LstHotelImageGallary.Count > 0)
                            {
                                ObjHotelList.ImageName = LstHotelImageGallary.FirstOrDefault().ImageActualPath;
                            }
                            ObjHotelList.Latitude = Convert.ToString(hotelinfo.longitude);
                            ObjHotelList.Longitude = Convert.ToString(hotelinfo.latitude);
                            ObjHotelList.local = "0";
                            if (hotelinfo.region != null)
                            {
                                ObjHotelList.Location = hotelinfo.region.name;
                            }
                            else
                            {
                                ObjHotelList.Location = "";
                            }
                            ObjHotelList.h_Desc = "<p style='padding: 10px 10px 0px 10px;'>" + Convert.ToString(hotelinfo.description_struct) + "</p>";
                            //ObjHotelList.transportation = "";
                            ObjHotelList.OfferCount = "0";
                            decimal TotalRate = 0;
                            TotalRate = item.rates.SelectMany(rate => rate.payment_options.payment_types.Select(pt => Convert.ToDecimal(pt.show_amount))).Min();

                            if (!IsMarkValueOrPercentage_)
                            {
                                TotalRate = TotalRate + (MarkUpVal_.Equals(0) ? 0 : Convert.ToDecimal((TotalRate / 100) * MarkUpVal_));
                            }
                            else
                            {
                                TotalRate = TotalRate + MarkUpVal_;
                            }

                            decimal PerRoomRate = TotalRate / SearchReq.Nights;

                            ObjHotelList.TotalRate = PerRoomRate.ToString("0.00");

                            ObjHotelList.HotelRate = PerRoomRate.ToString("0.00");

                            ObjHotelList.HotelImageGallary = LstHotelImageGallary;
                            ObjHotelList.HotelAminities = LstHotelAminities;

                            LstHotelList.Add(ObjHotelList);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                //Errorlog.InsertLogError(ex, 0);
            }
            finally
            {
                File.WriteAllText(FolderName + "/RateHawk_" + Guid.NewGuid().ToString().Replace("-", "_") + ".txt", JsonConvert.SerializeObject(LstHotelList));
            }
            return LstHotelList;
        }

        public HotelList HotelDetail(Dictionary<string, string> Apic, HotelDetailRequest Sreq, string HotelCode, string FolderName, Dictionary<string, HotelDumpclass.Root> StaticData, RateHawkHotelDataSearch.Root RateHawkHotelDataSearchObj)
        {
            HotelList ObjHotelList = new HotelList();
            List<HotelImageGallary> LstHotelImageGallary = new List<HotelImageGallary>();
            List<HotelAminities> LstHotelAminities = new List<HotelAminities>();
            var EHotelRoomDetailLst = new List<HotelRoomDetail>();

            string passengerCountryOfResidence = Convert.ToString(Sreq.Nationality);
            string APICurrencyName = "USD";

            decimal APIROE = 1;
            decimal UserROE = 1;
            string basecurrency = string.Empty;
            DataTable Dt_MarkUpDetails = new DataTable();
            bool IsCorporate = false;
            bool IsMarkValueOrPercentage_ = false;
            decimal MarkUpVal_ = 0;
            try
            {
                var HotelSearchResponseLst = SearchByHotelID(Sreq, HotelCode, Apic, FolderName);
                if (HotelSearchResponseLst != null && HotelSearchResponseLst.data != null && HotelSearchResponseLst.data.hotels != null)
                {
                    if (HotelSearchResponseLst != null && HotelSearchResponseLst.data.hotels.Count > 0)
                    {
                        //B2CBookingBLL B2CBookingBLL = new B2CBookingBLL();
                        //DataTable IsCorporatedt = GetAgentIsCorporate(Sreq.AgentId);
                        //if (IsCorporatedt != null && IsCorporatedt.Rows.Count != 0)
                        //{
                        //    IsCorporate = Convert.ToInt32(IsCorporatedt.Rows[0]["IsCorporate"]).Equals(0);
                        //}
                        //if (IsCorporate == true)
                        //{
                        //    Dt_MarkUpDetails = GetCorprateSupplierTypeMarkUp("9", Sreq.AgentId);
                        //}
                        //else
                        //{
                        //    Dt_MarkUpDetails = B2CBookingBLL.GetMarkDetailsBySupplierType_NEW("9", Sreq.AgentId != 0 ? "B2B" : "B2C", Sreq.AgentId);
                        //}
                        //Dt_MarkUpDetails = B2CBookingBLL.GetMarkDetailsBySupplierType_NEW("9", Sreq.AgentId != 0 ? "B2B" : "B2C", Sreq.AgentId);
                        //if (Dt_MarkUpDetails.Rows.Count != 0)
                        //{
                        //    if (Dt_MarkUpDetails.Rows[0]["MarkupType"].ToString() != "")
                        //    {
                        //        IsMarkValueOrPercentage_ = Convert.ToBoolean(int.Parse(Dt_MarkUpDetails.Rows[0]["MarkupType"].ToString()));
                        //    }
                        //    if (Dt_MarkUpDetails.Rows[0]["MarkupValue"].ToString() != "")
                        //    {
                        //        MarkUpVal_ = Convert.ToDecimal(Dt_MarkUpDetails.Rows[0]["MarkupValue"].ToString());
                        //    }
                        //}
                        foreach (var hotel in HotelSearchResponseLst.data.hotels)
                        {
                            var hotelinfo = StaticData[hotel.id];
                            if (hotelinfo != null)
                            {
                                ObjHotelList.HotelName = hotelinfo.name;
                                ObjHotelList.HotelId = hotelinfo.id;
                                ObjHotelList.Address = hotelinfo.address;
                                ObjHotelList.Category = Convert.ToString(hotelinfo.star_rating);
                                ObjHotelList.CheckInTime = hotelinfo.check_in_time;
                                ObjHotelList.CheckOutTime = hotelinfo.check_out_time;
                                ObjHotelList.CurrencyName = APICurrencyName;
                                ObjHotelList.hotelId_service = APIName + "~" + hotelinfo.id;
                                ObjHotelList.rate_supplier = APIName;
                                ObjHotelList.HotelIdsString = APIName + "~" + hotelinfo.id;
                                ObjHotelList.Latitude = Convert.ToString(hotelinfo.latitude);
                                ObjHotelList.Longitude = Convert.ToString(hotelinfo.longitude);
                                ObjHotelList.local = "0";
                                ObjHotelList.Location = hotelinfo.region.name;
                                ObjHotelList.h_Desc = "<p style='padding: 10px 10px 0px 10px;'>" + hotelinfo.description_struct + "</p>";
                                if (hotelinfo.images != null)
                                {
                                    if (hotelinfo.images.Count > 0)
                                    {
                                        foreach (var image in hotelinfo.images)
                                        {
                                            HotelImageGallary ObjHotelImageGallary = new HotelImageGallary();
                                            ObjHotelImageGallary.ImagePath = image;
                                            LstHotelImageGallary.Add(ObjHotelImageGallary);
                                        }
                                        ObjHotelList.ImageName = Convert.ToString(LstHotelImageGallary.FirstOrDefault().ImagePath);
                                    }
                                }
                                if (hotelinfo.amenity_groups != null)
                                {
                                    if (hotelinfo.amenity_groups.Count > 0)
                                    {
                                        foreach (var amenitie in hotelinfo.amenity_groups)
                                        {
                                            foreach (var item in amenitie.amenities)
                                            {
                                                HotelAminities ObjHotelAminities = new HotelAminities();
                                                ObjHotelAminities.AminitieName = item;
                                                LstHotelAminities.Add(ObjHotelAminities);
                                            }
                                        }
                                    }
                                }
                                ObjHotelList.HotelImageGallary = LstHotelImageGallary;
                                ObjHotelList.HotelAminities = LstHotelAminities;
                            }
                            ObjHotelList.Category = Convert.ToString(hotelinfo.star_rating);
                            int RoomIndex_ = 0;
                            for (int i = 0; i < hotel.rates.Count; i++)
                            {
                                ////to display non - included taxes 
                                string nonincludedtaxes = string.Empty;
                                decimal totalNonIncludedTax = 0;
                                var nonIncludedTaxesList = hotel.rates[i].payment_options.payment_types[0].tax_data.taxes.Where(t => !t.included_by_supplier).ToList();

                                if (hotel.rates[i].payment_options.payment_types[0].tax_data.taxes.Any(t => !t.included_by_supplier))
                                {
                                    nonincludedtaxes += string.Join("<br/>", hotel.rates[i].payment_options.payment_types[0].tax_data.taxes
                                        .Where(t => !t.included_by_supplier)
                                        .Select(t => $"Non-included tax : <br/> {t.name} - {t.currency_code}  {t.amount}"));//string of non included taxes

                                    totalNonIncludedTax = nonIncludedTaxesList.Sum(t => Convert.ToDecimal(t.amount));//sum of non included taxes
                                    totalNonIncludedTax = 0;

                                }
                                ////to display non - included taxes

                                decimal DefaultAllNightRate = 0, AllNightTotalRate = 0;
                                decimal RoomRatePer = (Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount) / Sreq.Nights) / Sreq.TotalNoRoom;
                                RoomRatePer = (RoomRatePer / APIROE) * UserROE;

                                decimal AllNightOfferTotalRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount); //* NoOfRooms;
                                AllNightOfferTotalRate = (AllNightOfferTotalRate / APIROE) * UserROE;

                                decimal Discount = Convert.ToDecimal(0);
                                Discount = (Discount / APIROE) * UserROE;
                                AllNightTotalRate = AllNightOfferTotalRate + Discount;
                                var DayWiseRatesLst = new List<DateWiseRoomRate>();


                                for (int k = 0; k < Sreq.Nights; k++)
                                {
                                    var OneDayRate = RoomRatePer;
                                    //var DicRates1sd = APIMarkupSection.AddMarkupByRule(APIConstant.RateHawkAPIID, Convert.ToDecimal(OneDayRate), MarkupClassObj, APICurrencyName, 1, Sreq.TotalNoRoom);
                                    DayWiseRatesLst.Add(new DateWiseRoomRate() { Date = Sreq.CheckInDate.AddDays(k).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), Rate = Convert.ToDecimal(OneDayRate) + ((totalNonIncludedTax / Sreq.Nights) / Sreq.TotalNoRoom) });
                                }





                                //var DicRates1 = APIMarkupSection.AddMarkupByRule(APIConstant.RateHawkAPIID, Convert.ToDecimal(AllNightOfferTotalRate), MarkupClassObj, APICurrencyName, Sreq.Nights, Sreq.TotalNoRoom);
                                //AllNightOfferTotalRate = (Convert.ToDecimal(DicRates1[MarkupSectionObj.AR]));
                                DefaultAllNightRate = AllNightOfferTotalRate;

                                //var DicAllNightTotalRate = APIMarkupSection.AddMarkupByRule(APIConstant.RateHawkAPIID, Convert.ToDecimal(AllNightTotalRate), MarkupClassObj, APICurrencyName, Sreq.Nights, Sreq.TotalNoRoom);
                                //AllNightTotalRate = (Convert.ToDecimal(DicAllNightTotalRate[APIMarkupSection.AR]));



                                var EHotelRoomDetailobj = new HotelRoomDetail();
                                decimal DefaultTotalRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount);
                                EHotelRoomDetailobj.RoomNo = Convert.ToString(Sreq.TotalNoRoom);
                                EHotelRoomDetailobj.RoomIndex = Convert.ToString(RoomIndex_);


                                EHotelRoomDetailobj.CurrencyName = APICurrencyName;
                                string OccupancyStr = string.Empty;
                                if (true)
                                {

                                    //OccupancyStr = GetRoomOccupancy(Convert.ToString("2"), Convert.ToString("0"), Convert.ToString("2"), Convert.ToString(0));
                                }
                                else
                                {
                                    //OccupancyStr = GetRoomOccupancy(Convert.ToString(2), Convert.ToString(0), "0", "0");

                                }
                                EHotelRoomDetailobj.OccuapncyAdult = OccupancyStr;
                                EHotelRoomDetailobj.OccuapncyChild = string.Empty;
                                EHotelRoomDetailobj.OccuapncyAdult2 = string.Empty;
                                EHotelRoomDetailobj.OccuapncyChild2 = string.Empty;
                                EHotelRoomDetailobj.RoomOccupancy = OccupancyStr;
                                EHotelRoomDetailobj.CurrencyName = APICurrencyName;
                                EHotelRoomDetailobj.RoomType = hotel.rates[i].room_name;
                                EHotelRoomDetailobj.RoomTypeId = hotel.rates[i].room_name;
                                EHotelRoomDetailobj.Alias = hotel.rates[i].room_name;
                                EHotelRoomDetailobj.IsOffer = "0";
                                EHotelRoomDetailobj.AllNightTotalRate = AllNightOfferTotalRate.ToString("0.00");
                                EHotelRoomDetailobj.TotalRate = RoomRatePer.ToString("0.00");
                                EHotelRoomDetailobj.RoomRate = RoomRatePer.ToString("0.00");
                                EHotelRoomDetailobj.SupplierRate = APIName;
                                EHotelRoomDetailobj.hotelRoomTypeId = Convert.ToString(hotel.rates[i].room_name);
                                EHotelRoomDetailobj.ApiRoomRate = AllNightTotalRate.ToString("0.00");
                                EHotelRoomDetailobj.ApiCurrency = APICurrencyName;
                                EHotelRoomDetailobj.ApiAllNightTotalRate = AllNightTotalRate.ToString("0.00");
                                var metapolicy_struct_sentences = new List<string>();
                                if (hotelinfo.metapolicy_struct != null)
                                {

                                    var metapolicy_struct = string.Join(", ", hotelinfo.metapolicy_struct.GetType().GetProperties()
                                                            .Select(prop =>
                                                            {
                                                                var value = prop.GetValue(hotelinfo.metapolicy_struct);
                                                                if (value is IEnumerable<object> list && !(value is string))
                                                                {
                                                                    return string.Join(", ", list);
                                                                }
                                                                else if (value != null)
                                                                {
                                                                    return JsonConvert.SerializeObject(value);
                                                                }
                                                                return string.Empty;
                                                            })
                                                            .Where(s => !string.IsNullOrWhiteSpace(s))
                                                        );

                                    EHotelRoomDetailobj.Errata += "<br/>" + metapolicy_struct;
                                }
                                else
                                {
                                    var metapolicy_struct = string.Join(", ", RateHawkHotelDataSearchObj.data.metapolicy_struct.GetType().GetProperties()
                                                                .Select(prop =>
                                                                {
                                                                    var value = prop.GetValue(RateHawkHotelDataSearchObj.data.metapolicy_struct);
                                                                    if (value is IEnumerable<object> list && !(value is string))
                                                                    {
                                                                        return string.Join(", ", list);
                                                                    }
                                                                    else if (value != null)
                                                                    {
                                                                        return JsonConvert.SerializeObject(value);
                                                                    }
                                                                    return string.Empty;
                                                                })
                                                                .Where(s => !string.IsNullOrWhiteSpace(s))
                                                            );




                                    var metapolicy = RateHawkHotelDataSearchObj.data.metapolicy_struct;
                                    // Children
                                    metapolicy_struct_sentences.AddRange(metapolicy.children.Select(c =>
                                        $"Children aged between {c.age_start} and {c.age_end} years are charged {c.price} {c.currency}. Extra bed is {c.extra_bed}."));

                                    // Children Meal
                                    metapolicy_struct_sentences.AddRange(metapolicy.children_meal.Select(cm =>
                                        $"Breakfast for children aged {cm.age_start} to {cm.age_end} years is {cm.inclusion.Replace("_", " ")} at {cm.price} {cm.currency}."));

                                    // Cot
                                    metapolicy_struct_sentences.AddRange(metapolicy.cot.Select(c =>
                                        $"Cot is {c.inclusion.Replace("_", " ")} at {c.price} {c.currency} {c.price_unit.Replace("_", " ")}."));

                                    // Deposit
                                    metapolicy_struct_sentences.AddRange(metapolicy.deposit.Select(d =>
                                        $"{(string.IsNullOrWhiteSpace(d.deposit_type) ? "Deposit" : d.deposit_type)} deposit of {d.price} {d.currency} is required per {d.price_unit.Replace("_", " ")} via {d.payment_type}."));

                                    // Extra bed
                                    metapolicy_struct_sentences.AddRange(metapolicy.extra_bed.Select(e =>
                                        $"Extra bed for {e.amount} person(s) costs {e.price} {e.currency} {e.price_unit.Replace("_", " ")}."));

                                    // Internet
                                    metapolicy_struct_sentences.AddRange(metapolicy.internet.Select(ii =>
                                        $"Internet in {ii.work_area} is {ii.inclusion.Replace("_", " ")} at {ii.price} {ii.currency} {ii.price_unit.Replace("_", " ")}."));

                                    // Meals
                                    metapolicy_struct_sentences.AddRange(metapolicy.meal.Select(m =>
                                        $"{m.meal_type} is {m.inclusion.Replace("_", " ")} at {m.price} {m.currency}."));

                                    // No show
                                    if (metapolicy.no_show != null)
                                    {
                                        metapolicy_struct_sentences.Add($"No-show policy applies {metapolicy.no_show.day_period.Replace("_", " ")} after {metapolicy.no_show.time}.");
                                    }

                                    // Parking
                                    metapolicy_struct_sentences.AddRange(metapolicy.parking.Select(p =>
                                        $"Parking is {p.inclusion.Replace("_", " ")} at {p.price} {p.currency} {p.price_unit.Replace("_", " ")}."));

                                    // Pets
                                    metapolicy_struct_sentences.AddRange(metapolicy.pets.Select(p =>
                                        $"Pets are {p.inclusion.Replace("_", " ")} at {p.price} {p.currency} {p.price_unit.Replace("_", " ")}."));

                                    // Shuttle
                                    metapolicy_struct_sentences.AddRange(metapolicy.shuttle.Select(s =>
                                        $"{s.destination_type} shuttle ({s.shuttle_type.Replace("_", " ")}) is {s.inclusion.Replace("_", " ")} at {s.price} {s.currency}."));

                                    // Visa
                                    if (metapolicy.visa?.visa_support == "support_enable")
                                    {
                                        metapolicy_struct_sentences.Add("Visa support is available.");
                                    }






                                    // EHotelRoomDetailobj.Errata += "<br/>" + metapolicy_struct;
                                    EHotelRoomDetailobj.Errata += string.Join("<br/>", metapolicy_struct_sentences);
                                }
                                EHotelRoomDetailobj.IsErrata = EHotelRoomDetailobj.Errata.Length > 0 ? "1" : "0";


                                Dictionary<string, string> RTHWKAPIExtraInfo = new Dictionary<string, string>();
                                RTHWKAPIExtraInfo.Add("RIndex", String.Empty);
                                RTHWKAPIExtraInfo.Add("BHRDID", "0");
                                RTHWKAPIExtraInfo.Add("book_hash", hotel.rates[i].book_hash);
                                RTHWKAPIExtraInfo.Add("match_hash", hotel.rates[i].match_hash);
                                RTHWKAPIExtraInfo.Add("HotelPackage", "0");
                                RTHWKAPIExtraInfo.Add("metapolicy_extra_info", Convert.ToString(hotelinfo.metapolicy_extra_info));
                                RTHWKAPIExtraInfo.Add("metapolicy_struct", string.Join("<br/>", metapolicy_struct_sentences));
                                RTHWKAPIExtraInfo.Add("IsUpsell", (hotel.rates[i].payment_options.payment_types[0].perks != null && hotel.rates[i].payment_options.payment_types[0].perks.early_checkin != null && hotel.rates[i].payment_options.payment_types[0].perks.late_checkout != null) ? "1" : "0");
                                EHotelRoomDetailobj.ExtraInfo = JsonConvert.SerializeObject(RTHWKAPIExtraInfo);
                                EHotelRoomDetailobj.AllNigntOfferRate = AllNightOfferTotalRate.ToString("0.00");
                                EHotelRoomDetailobj.AllRoomOfferRate = AllNightTotalRate.ToString("0.00");

                                EHotelRoomDetailobj.AllNightTotalRate = AllNightTotalRate.ToString("0.00");
                                EHotelRoomDetailobj.AllRoomRate = AllNightTotalRate.ToString("0.00");

                                EHotelRoomDetailobj.OfferCountForHeader = "0";
                                EHotelRoomDetailobj.OfferCountName = "0";
                                RoomDefaultInfo ObjRoomDefaultInfo = new RoomDefaultInfo();
                                ObjRoomDefaultInfo.DefaultCurrancyName = APICurrencyName;
                                ObjRoomDefaultInfo.DefaultOneNightRate = (RoomRatePer).ToString("0.00");
                                ObjRoomDefaultInfo.DefaultAllNightRate = DefaultAllNightRate.ToString();
                                ObjRoomDefaultInfo.AgentMarkupType = Convert.ToInt32(IsMarkValueOrPercentage_).ToString();// Dt_MarkUpDetails.Rows[0]["MarkupType"].ToString();
                                ObjRoomDefaultInfo.AgentMarkupValue = MarkUpVal_.ToString("0.00");
                                ObjRoomDefaultInfo.ApiCurrencyRate = "1";
                                EHotelRoomDetailobj.DefaultInfo = ObjRoomDefaultInfo;
                                EHotelRoomDetailobj.IsCanclePolicy = "0";

                                DateTime LastCancellationDate = new DateTime();
                                LastCancellationDate = DateTime.Now.AddDays(FreeCancellationBufferPeriod);
                                string LancellationPolicyStr = string.Empty;
                                DateTime fromdate = DateTime.Now;
                                DateTime todate = DateTime.Now;

                                List<DateTime> CancelDate = new List<DateTime>();

                                if (hotel.rates[i].payment_options.payment_types[0].cancellation_penalties != null || hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies.Count > 0)
                                {
                                    for (int cp = 0; cp < hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies.Count; cp++)
                                    {
                                        if (hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].start_at != null)
                                        {

                                            fromdate = Convert.ToDateTime(hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].start_at);
                                            todate = Convert.ToDateTime(hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].end_at);
                                            //if (hotel.roomDetails[i].cancellationPolicyType != "NRF")
                                            if (fromdate != null)
                                            {
                                                if (!string.IsNullOrEmpty(hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].amount_show.ToString()))
                                                {

                                                    decimal amount = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].amount_show);
                                                    amount = (amount / APIROE) * UserROE;
                                                    //amount = Convert.ToDecimal(APIMarkupSection.AddPolicyMarkupByRule(APIConstant.RateHawkAPIID, amount, MarkupClassObj, APICurrencyName, 1, 1));
                                                    amount = Math.Ceiling(amount);
                                                    //if()



                                                    //LancellationPolicyStr += "From  <b>" + fromdate.AddDays(APIConstant.FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture) + "To <b>" + todate.AddDays(APIConstant.FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture) + "</b>  Cancellation Penalty will be <b>" + Sreq.CurrencyCode + " " + amount.ToString("0.00") + "</b>  <br/>";

                                                    if (hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].end_at == null)
                                                    {
                                                        LancellationPolicyStr += "From  <b>" + fromdate.AddDays(FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture) + "</b>  Cancellation Penalty will be <b>" + APICurrencyName + " " + amount.ToString("0.00") + "</b>  <br/>";
                                                    }
                                                    else
                                                    {
                                                        LancellationPolicyStr += "From  <b>" + fromdate.AddDays(FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture) + "To <b>" + todate.AddDays(FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture) + "</b>  Cancellation Penalty will be <b>" + APICurrencyName + " " + amount.ToString("0.00") + "</b>  <br/>";
                                                    }

                                                }


                                            }
                                        }
                                        CancelDate.Add(fromdate);
                                        CancelDate.Add(todate);

                                    }
                                }

                                //string freecanceldate = CancelDate.Min().ToString("dd MMM yyyy");
                                string freecanceldate = hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.free_cancellation_before != null ? DateTime.ParseExact(
                                    hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.free_cancellation_before.ToString(),
                                    "M/d/yyyy h:mm:ss tt",
                                    CultureInfo.InvariantCulture
                                ).ToString("dd MM yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact(
                                   DateTime.Now.Date.ToString(),
                                    "M/d/yyyy h:mm:ss tt",
                                    CultureInfo.InvariantCulture
                                ).ToString("dd MM yyyy", CultureInfo.InvariantCulture);


                                if (EHotelRoomDetailobj.IsFreeCostCancellationDate.Equals(1))
                                {

                                    //EHotelRoomDetailobj.FreeCostCancellationDate = Convert.ToDateTime(freecanceldate).AddDays(APIConstant.FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);

                                    DateTime parsedFreeCancelDate = DateTime.ParseExact(freecanceldate, "dd MM yyyy", CultureInfo.InvariantCulture);

                                    EHotelRoomDetailobj.FreeCostCancellationDate = parsedFreeCancelDate.AddDays(FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);

                                }
                                else
                                {
                                    EHotelRoomDetailobj.FreeCostCancellationDate = DateTime.Now.AddDays(FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
                                }
                                //EHotelRoomDetailobj.FreeCostCancellationDate = Convert.ToDateTime(EHotelRoomDetailobj.FreeCostCancellationDate).AddDays(-1).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);


                                DateTime today = DateTime.Now;
                                if (today > Convert.ToDateTime(EHotelRoomDetailobj.FreeCostCancellationDate))
                                {
                                    EHotelRoomDetailobj.IsFreeCostCancellationDate = "0";

                                }
                                else
                                {
                                    EHotelRoomDetailobj.IsFreeCostCancellationDate = "1";
                                }
                                EHotelRoomDetailobj.CanclePolicy = LancellationPolicyStr;
                                EHotelRoomDetailobj.Errata = String.Empty;
                                EHotelRoomDetailobj.IsErrata = "0";
                                //added for non included tax
                                EHotelRoomDetailobj.Errata = nonincludedtaxes;
                                //added for non included tax
                                //metapolicy_info
                                EHotelRoomDetailobj.Errata += "<br/><b><u>Extra Info:</u></b><br/>" + hotelinfo.metapolicy_extra_info;
                                EHotelRoomDetailobj.IsErrata = (EHotelRoomDetailobj.Errata.ToCharArray().Length < 5) ? "0" : "1";
                                EHotelRoomDetailobj.AvilableRoom = "1";
                                EHotelRoomDetailobj.View = new ViewCalculation()
                                {
                                    Currency = APICurrencyName,
                                    DiscountRate = Discount,
                                    HotelName = ObjHotelList.HotelName,
                                    IsOffer = EHotelRoomDetailobj.IsOffer.Equals("1"),
                                    OfferStr = "MinPriceRoom",
                                    OtherCharges = 0,
                                    Rates = DayWiseRatesLst,
                                    ROE = 1


                                };
                                EHotelRoomDetailobj.AvilableRoom = Convert.ToString(Sreq.TotalNoRoom);
                                EHotelRoomDetailobj.Issharedbedding = "0";
                                EHotelRoomDetailobj.IsExtraBed = "0";
                                ViewCalculation ViewCalculation = new ViewCalculation()
                                {
                                    Currency = APICurrencyName,
                                    DiscountRate = (-Discount),
                                    HotelName = Convert.ToString(string.Empty),
                                    IsOffer = EHotelRoomDetailobj.IsOffer.Equals("1"),
                                    OfferStr = EHotelRoomDetailobj.OfferCountName,
                                    OtherCharges = 0,
                                    ROE = 1,
                                    Rates = DayWiseRatesLst

                                };

                                var DicGrossPrice = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount);
                                var GrossPrice = ((Convert.ToDecimal(DicGrossPrice) / APIROE) * UserROE);

                                EHotelRoomDetailobj.IsCanclePolicy = LancellationPolicyStr.Length != 0 ? "1" : "0";
                                EHotelRoomDetailobj.CanclePolicy = LancellationPolicyStr;

                                EHotelRoomDetailobj.View = ViewCalculation;
                                EHotelRoomDetailobj.AllNightTotalRate = AllNightTotalRate.ToString("0.00");
                                EHotelRoomDetailobj.AllNigntOfferRate = AllNightOfferTotalRate.ToString("0.00");
                                EHotelRoomDetailobj.TotalRate = RoomRatePer.ToString("0.00");


                                //added nonincluded tax
                                EHotelRoomDetailobj.AllNigntOfferRate = EHotelRoomDetailobj.AllNigntOfferRate;
                                EHotelRoomDetailobj.AllNightTotalRate = EHotelRoomDetailobj.AllNightTotalRate;
                                //added nonincluded tax
                                EHotelRoomDetailLst.Add(EHotelRoomDetailobj);
                            }
                            ObjHotelList.HotelRoomDetail = EHotelRoomDetailLst;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //Errorlog.InsertLogError(ex, 0);
            }
            finally
            {
                File.WriteAllText(FolderName + "/DOTW_" + Guid.NewGuid().ToString().Replace("-", "_") + ".txt", JsonConvert.SerializeObject(ObjHotelList));
            }
            return ObjHotelList;
        }

        #region prebook


        public RateHawkPrebook.Root RateHawkPrebookreq(Dictionary<string, string> ApiC, Etos_EHotelDetailRequest Req, Dictionary<string, string> RateHawkAPIObjExtraInfo, string FolderPath)
        {
            var result = new RateHawkPrebook.Root();
            var client = new RestClient("" + ApiC["PreBook"] + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);

            username = ApiC["UserName"];
            password = ApiC["Password"];
            try
            {

                string hotelhashkey = RateHawkAPIObjExtraInfo["book_hash"];

                var body = @"{
                                'hash': '" + hotelhashkey + @"',
                                'price_increase_percent': " + 0 + @",
                                
                            }";

                string correctedBody = body.Replace("'", "\"").Replace("\"\"", "\"");
                var parsedJson = Newtonsoft.Json.Linq.JObject.Parse(correctedBody);

                //string finalsearchreq = Newtonsoft.Json.Linq.JObject.Parse(body.Replace("'", "\"")).ToString();

                request.AddParameter("application/json", parsedJson, ParameterType.RequestBody);
                string credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{username}:{password}"));
                request.AddHeader("Authorization", $"Basic {credentials}");

                File.WriteAllText(FolderPath + "/RateHawkAPI_Prebook_Request" + Guid.NewGuid().ToString().Replace('-', '_') + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.CreateSpecificCulture("en-US")) + ".RTHWKPrebookReq", parsedJson.ToString());


                IRestResponse response = client.Execute(request);
                string res = response.Content;

                result = JsonConvert.DeserializeObject<RateHawkPrebook.Root>(res, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                File.WriteAllText(FolderPath + "/RateHawkAPI_Prebook_Response" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".RTHWKPrebookRes", response.Content);

            }
            catch (Exception ex)
            {
                Errorlog.InsertLogError(ex, 1);
            }

            return result;
        }


        public APIPreBookingInfo PreHotelBook_V1(Dictionary<string, string> Apic, Etos_EHotelDetailRequest Req, Dictionary<string, string> RateHawkAPIObjExtraInfo, string BHDID, MarkupClass MarkupClassObj, EHotelRoomDetail OldEHRDetails, string FolderPath, int BookingID, EHotelPreBookingRequest PreRequest, EHotelDetail EHotelDetailObj_, int ServiceCartId)
        {
            APIPreBookingInfo result = new APIPreBookingInfo();
            try
            {
                RateHawkPrebook.Root rthwkprebook = RateHawkPrebookreq(Apic, Req, RateHawkAPIObjExtraInfo, FolderPath);
                if (rthwkprebook != null)
                {
                    var RoomDe1 = GetPreBookingHotelRoomDetail(OldEHRDetails, rthwkprebook, Req, Req.HotelID, BHDID, MarkupClassObj, FolderPath);
                    if (RoomDe1 != null)
                    {
                        result.AvailableForBook = true;

                        if (OldEHRDetails.AllNigntOfferRate.ToString("0.00") != Convert.ToDecimal(RoomDe1.AllNightTotalRate).ToString("0.00"))
                        {
                            result.After_Rate = Convert.ToDecimal(RoomDe1.AllNightTotalRate);
                            result.Before_Rate = OldEHRDetails.AllNigntOfferRate;
                            result.IsPriceChange = true;
                            result.RawData = RoomDe1;
                            result.CancellationPolicy = RoomDe1.CanclePolicy;
                            result.CancellationDate = RoomDe1.FreeCostCancellationDate;
                            result.Norms = RoomDe1.Errata;
                        }
                        else
                        {
                            result.After_Rate = OldEHRDetails.AllNigntOfferRate;
                            result.Before_Rate = OldEHRDetails.AllNigntOfferRate;
                            result.IsPriceChange = false;
                            //result.RawData = OldEHRDetails;
                            //result.CancellationPolicy = OldEHRDetails.CanclePolicy;
                            //result.CancellationDate = OldEHRDetails.FreeCostCancellationDate;


                            result.RawData = RoomDe1;
                            result.CancellationPolicy = RoomDe1.CanclePolicy;
                            result.CancellationDate = RoomDe1.FreeCostCancellationDate;
                            result.Norms = OldEHRDetails.Errata;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Errorlog.InsertLogError(ex, 1);
            }
            return result;
        }

        public EHotelRoomDetail GetPreBookingHotelRoomDetail(EHotelRoomDetail OldEHRDetails, RateHawkPrebook.Root roomDet, Etos_EHotelDetailRequest Sreq, string Hotelid, string BHRDID, MarkupClass MarkupClassObj, string FolderPath)
        {
            var EHotelRoomDetailobj = new EHotelRoomDetail();
            string APICurrencyName = string.Empty;
            if (roomDet != null && roomDet.data != null && roomDet.data.hotels != null && roomDet.data.hotels.Count > 0)
            {
                APICurrencyName = roomDet.data.hotels.First().rates.FirstOrDefault().payment_options.payment_types.FirstOrDefault().show_currency_code;
            }
            string Result = string.Empty;


            string checkInDate = Convert.ToString(Sreq.CheckInDate_);
            DateTime dTime = DateTime.Now;
            DateTime.TryParse(checkInDate, out dTime);

            string checkOutDate = Convert.ToString(Sreq.CheckOutDate_);
            DateTime dtTime = DateTime.Now;
            DateTime.TryParse(checkOutDate, out dtTime);
            Dictionary<string, string> OldEHRDetails_ExtraInfo = new Dictionary<string, string>();

            try
            {





                decimal APIROE = APIMarkupSection.GetCurrancyRate(APICurrencyName, Sreq.ClientID);
                decimal UserROE = APIMarkupSection.GetCurrancyRate(Sreq.CurrencyCode, Sreq.ClientID);

                if (roomDet != null && roomDet.data != null && roomDet.data.hotels != null)
                {
                    if (roomDet != null && roomDet.data.hotels.Count > 0)
                    {
                        int x = 0;

                        string HotelCurrency = APICurrencyName;

                        int RoomIndex_ = 0;

                        foreach (var hotel in roomDet.data.hotels)
                        {
                            for (int i = 0; i < hotel.rates.Count; i++)
                            {


                                ////to display non - included taxes 
                                string nonincludedtaxes = string.Empty;
                                decimal totalNonIncludedTax = 0;
                                var nonIncludedTaxesList = hotel.rates[i].payment_options.payment_types[0].tax_data.taxes.Where(t => !t.included_by_supplier).ToList();

                                if (hotel.rates[i].payment_options.payment_types[0].tax_data.taxes.Any(t => !t.included_by_supplier))
                                {
                                    nonincludedtaxes += string.Join("<br/>", hotel.rates[i].payment_options.payment_types[0].tax_data.taxes
                                        .Where(t => !t.included_by_supplier)
                                        .Select(t => $"Non-included tax : <br/> {t.name} - {t.currency_code}  {t.amount}"));//string of non included taxes

                                    totalNonIncludedTax = nonIncludedTaxesList.Sum(t => Convert.ToDecimal(t.amount));//sum of non included taxes
                                    totalNonIncludedTax = 0;
                                }
                                ////to display non - included taxes

                                decimal DefaultAllNightRate = 0, AllNightTotalRate = 0;
                                decimal RoomRatePer = (Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount) / Sreq.Nights) / Sreq.TotalNoRoom;
                                RoomRatePer = (RoomRatePer / APIROE) * UserROE;

                                decimal AllNightOfferTotalRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount); //* NoOfRooms;
                                AllNightOfferTotalRate = (AllNightOfferTotalRate / APIROE) * UserROE;

                                decimal Discount = Convert.ToDecimal(0);
                                Discount = (Discount / APIROE) * UserROE;
                                AllNightTotalRate = AllNightOfferTotalRate + Discount;
                                var DayWiseRatesLst = new List<EDateWiseRoomRate>();


                                for (int k = 0; k < Sreq.Nights; k++)
                                {
                                    var OneDayRate = RoomRatePer;
                                    var DicRates1sd = APIMarkupSection.AddMarkupByRule(APIConstant.RateHawkAPIID, Convert.ToDecimal(OneDayRate), MarkupClassObj, APICurrencyName, 1, Sreq.TotalNoRoom);
                                    DayWiseRatesLst.Add(new EDateWiseRoomRate() { Date = Sreq.CheckInDate_.AddDays(k).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), Rate = Convert.ToDecimal((Convert.ToDouble(DicRates1sd[MarkupSectionObj.AR]))) + ((totalNonIncludedTax / Sreq.Nights) / Sreq.TotalNoRoom) });


                                }





                                var DicRates1 = APIMarkupSection.AddMarkupByRule(APIConstant.RateHawkAPIID, Convert.ToDecimal(AllNightOfferTotalRate), MarkupClassObj, APICurrencyName, Sreq.Nights, Sreq.TotalNoRoom);
                                AllNightOfferTotalRate = (Convert.ToDecimal(DicRates1[MarkupSectionObj.AR]));
                                DefaultAllNightRate = AllNightOfferTotalRate;

                                var DicAllNightTotalRate = APIMarkupSection.AddMarkupByRule(APIConstant.RateHawkAPIID, Convert.ToDecimal(AllNightTotalRate), MarkupClassObj, APICurrencyName, Sreq.Nights, Sreq.TotalNoRoom);
                                AllNightTotalRate = (Convert.ToDecimal(DicAllNightTotalRate[APIMarkupSection.AR]));

                                EHotelDetail ObjHotelList = new EHotelDetail();
                                List<EHotelImageGallary> LstHotelImageGallary = new List<EHotelImageGallary>();
                                List<EAminities> LstHotelAminities = new List<EAminities>();
                                // ConvertionRate = APIROE;

                                var RateCalculation = new Dictionary<string, object>();



                                //            }

                                var EAminitiesLst = new List<EAminities>();



                                var Amenities = hotel.rates[i].amenities_data.SelectMany(f => f.ToString()).ToList();

                                foreach (var fac in Amenities)
                                {
                                    var EAminitiesobj = new EAminities()
                                    {
                                        AminitieName = fac.ToString(),
                                        AminitieCategory = string.Empty,
                                        topAminities = 0,
                                        fcid = 0,
                                        img = string.Empty
                                    };
                                    EAminitiesLst.Add(EAminitiesobj);
                                }




                                //var EHotelRoomDetailobj = new EHotelRoomDetail();

                                EHotelRoomDetailobj.CheckInDate = Convert.ToDateTime(Sreq.CheckInDate_);
                                EHotelRoomDetailobj.CheckOutDate = Convert.ToDateTime(Sreq.CheckOutDate_);


                                EHotelRoomDetailobj.TotalTax = ((Convert.ToDecimal(DicAllNightTotalRate[APIMarkupSection.TXV])));
                                EHotelRoomDetailobj.IsTaxType = Convert.ToBoolean(DicAllNightTotalRate[APIMarkupSection.TXO]); Discount = Convert.ToDecimal(AllNightTotalRate) - Convert.ToDecimal(DefaultAllNightRate);
                                RateCalculation.Add(ContractedHotelConstant.Base_Room_Rate, Convert.ToDecimal(DicAllNightTotalRate[APIMarkupSection.AR]) - EHotelRoomDetailobj.TotalTax);//AP Cost
                                decimal DefaultTotalRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount);


                                EHotelRoomDetailobj.BHRDID = BHRDID;
                                EHotelRoomDetailobj.ServiceAPIID = APIConstant.RateHawkAPIID;
                                EHotelRoomDetailobj.RoomBasicClassType = hotel.rates[i].room_name;
                                EHotelRoomDetailobj.RoomIndex = RoomIndex_;
                                EHotelRoomDetailobj.RoomNo = Sreq.TotalNoRoom;
                                EHotelRoomDetailobj.HotelID = Sreq.HotelID;
                                EHotelRoomDetailobj.RoomType = hotel.rates[i].room_name;
                                EHotelRoomDetailobj.RoomTypeId = hotel.rates[i].room_name;
                                EHotelRoomDetailobj.RoomMeals = hotel.rates[i].meal;
                                EHotelRoomDetailobj.RoomMealsID = "0";
                                EHotelRoomDetailobj.RoomOccupancyDetails = Sreq.RoomOccupancyDetails;
                                EHotelRoomDetailobj.IsOffer = false;
                                EHotelRoomDetailobj.OfferName = "";
                                EHotelRoomDetailobj.TotalRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount);
                                EHotelRoomDetailobj.AllNightTotalRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount);
                                EHotelRoomDetailobj.Currency = APICurrencyName;


                                string SupplierName = APISupplierName;
                                int SupplierMasterID = 0;
                                int.TryParse(Convert.ToString(APISupplierID), out SupplierMasterID);

                                ERoomDefaultInfo RoomDefaultInfoObj = new ERoomDefaultInfo();
                                RoomDefaultInfoObj.EHotelMarkupInfoLst = new List<EHotelMarkupInfo>();
                                RoomDefaultInfoObj.Purchase_CurrencyCode = Convert.ToString(APICurrencyName);
                                RoomDefaultInfoObj.Purchase_CurrencyID = 0;
                                RoomDefaultInfoObj.Purchase_CurrencyROE_DIV = Convert.ToDecimal("1");
                                RoomDefaultInfoObj.Purchase_CurrencyROE_MLP = Convert.ToDecimal("1");
                                RoomDefaultInfoObj.Purchase_FinalAllNightRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount) + Discount;
                                RoomDefaultInfoObj.Supplier_AllNightOfferRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount);
                                RoomDefaultInfoObj.Supplier_AllNightRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount) + Discount;
                                RoomDefaultInfoObj.Supplier_Currency = Convert.ToString(APICurrencyName);
                                RoomDefaultInfoObj.System_AllNightOfferRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount) + Discount;
                                RoomDefaultInfoObj.System_AllNightRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount) + Discount;
                                RoomDefaultInfoObj.System_CurrencyCode = Convert.ToString(APICurrencyName);
                                RoomDefaultInfoObj.System_CurrencyID = 1;
                                RoomDefaultInfoObj.System_CurrencyROE_DIV = 1;
                                RoomDefaultInfoObj.System_CurrencyROE_MLP = 1;
                                RoomDefaultInfoObj.System_OneNightRate = RoomRatePer;
                                RoomDefaultInfoObj.System_TotalMarkup = 0;
                                RoomDefaultInfoObj.System_TotalTax = 0;
                                RoomDefaultInfoObj.DicAllNightRate = DicAllNightTotalRate;
                                RoomDefaultInfoObj.DicAllOfferTotalRate = DicRates1;

                                EHotelRoomDetailobj.RoomDefaultInfo = RoomDefaultInfoObj;

                                ESupplierRoomDefaultInfo SupplierRoomDefaultInfoObj = new ESupplierRoomDefaultInfo();
                                SupplierRoomDefaultInfoObj.Supplier_AllNightOfferRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount);
                                SupplierRoomDefaultInfoObj.Supplier_AllNightRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount) + Discount;
                                SupplierRoomDefaultInfoObj.Supplier_Currency = APICurrencyName;
                                SupplierRoomDefaultInfoObj.Supplier_ID = SupplierMasterID;
                                SupplierRoomDefaultInfoObj.Supplier_MinNightRate = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount);
                                SupplierRoomDefaultInfoObj.Supplier_Name = SupplierName;
                                SupplierRoomDefaultInfoObj.SupplierCode = APIName;

                                EHotelRoomDetailobj.SupplierRoomDefaultInfo = SupplierRoomDefaultInfoObj;
                                EHotelRoomDetailobj.Currency = Sreq.CurrencyCode;
                                EHotelRoomDetailobj.Supplier = SupplierName;
                                EHotelRoomDetailobj.IsSupplier = true;
                                EHotelRoomDetailobj.SupplierName = SupplierName;

                                EHotelRoomDetailobj.Errata = String.Empty;
                                EHotelRoomDetailobj.IsErrata = false;


                                //added for non included tax
                                EHotelRoomDetailobj.Errata = nonincludedtaxes;
                                EHotelRoomDetailobj.IsErrata = EHotelRoomDetailobj.Errata.Length > 0;
                                EHotelRoomDetailobj.TotalTax = totalNonIncludedTax;
                                EHotelRoomDetailobj.IsTaxType = EHotelRoomDetailobj.TotalTax > 0;
                                //added for non included tax

                                //metapolicy_info

                                OldEHRDetails_ExtraInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(OldEHRDetails.ExtraInfo);

                                if (OldEHRDetails_ExtraInfo.TryGetValue("metapolicy_extra_info", out string metapolicy_extra_info_value))
                                {
                                    EHotelRoomDetailobj.Errata += "<br/><b><u>Extra Info:</u></b><br/>" + metapolicy_extra_info_value;
                                }
                                if (OldEHRDetails_ExtraInfo.TryGetValue("metapolicy_struct", out string metapolicy_struct))
                                {
                                    EHotelRoomDetailobj.Errata += "<br/>" + metapolicy_struct;
                                }




                                Dictionary<string, string> RTHWKAPIExtraInfo = new Dictionary<string, string>();

                                EHotelRoomDetailobj.IsHotelPackage = Convert.ToInt32(0);
                                RTHWKAPIExtraInfo.Add("RIndex", String.Empty);
                                RTHWKAPIExtraInfo.Add("BHRDID", BHRDID);
                                RTHWKAPIExtraInfo.Add("book_hash", hotel.rates[i].book_hash);
                                RTHWKAPIExtraInfo.Add("match_hash", hotel.rates[i].match_hash);

                                RTHWKAPIExtraInfo.Add("HotelPackage", Convert.ToString(Convert.ToBoolean(EHotelRoomDetailobj.IsHotelPackage)));
                                RTHWKAPIExtraInfo.Add("metapolicy_struct", metapolicy_struct);
                                RTHWKAPIExtraInfo.Add("metapolicy_extra_info", metapolicy_extra_info_value);

                                EHotelRoomDetailobj.ExtraInfo = JsonConvert.SerializeObject(RTHWKAPIExtraInfo);


                                EHotelRoomDetailobj.AvilableRoom = Sreq.TotalNoRoom;
                                EHotelRoomDetailobj.Issharedbedding = false;
                                EHotelRoomDetailobj.IsExtraBed = false;
                                EHotelRoomDetailobj.IsMealPlanUpgrad = false;


                                EViewCalculation ViewCalculation = new EViewCalculation()
                                {
                                    Currency = Sreq.CurrencyCode,
                                    DiscountRate = (-Discount),
                                    HotelName = Convert.ToString(string.Empty),
                                    IsOffer = EHotelRoomDetailobj.IsOffer,
                                    OfferStr = EHotelRoomDetailobj.OfferName,
                                    OtherCharges = 0,
                                    ROE = 1,
                                    Rates = DayWiseRatesLst

                                };

                                string LancellationPolicyStr = string.Empty;

                                var DicGrossPrice = APIMarkupSection.AddMarkupByRule(APIConstant.RateHawkAPIID, Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].show_amount), MarkupClassObj, APICurrencyName, Sreq.Nights, Sreq.TotalNoRoom);
                                var GrossPrice = ((Convert.ToDecimal(DicGrossPrice[MarkupSectionObj.AR])) / APIROE) * UserROE;



                                DateTime LastCancellationDate = DateTime.Now;
                                DateTime datefinal = new DateTime();

                                DateTime dt = new DateTime();
                                DateTime dt1 = new DateTime();
                                string dt2 = string.Empty;
                                string finaldatee = string.Empty;

                                DateTime datecancel = new DateTime();

                                datecancel = Sreq.CheckInDate_;

                                if (hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.free_cancellation_before != null)
                                {

                                    EHotelRoomDetailobj.IsFreeCostCancellationDate = hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.free_cancellation_before > DateTime.Now.Date;
                                }
                                else
                                {
                                    EHotelRoomDetailobj.IsFreeCostCancellationDate = false;
                                }


                                DateTime fromdate = DateTime.Now;
                                DateTime todate = DateTime.Now;

                                List<DateTime> CancelDate = new List<DateTime>();

                                if (hotel.rates[i].payment_options.payment_types[0].cancellation_penalties != null || hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies.Count > 0)
                                {
                                    for (int cp = 0; cp < hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies.Count; cp++)
                                    {
                                        if (hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].start_at != null)
                                        {

                                            fromdate = Convert.ToDateTime(hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].start_at);
                                            todate = Convert.ToDateTime(hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].end_at);
                                            //if (hotel.roomDetails[i].cancellationPolicyType != "NRF")
                                            if (fromdate != null)
                                            {
                                                if (!string.IsNullOrEmpty(hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].amount_show.ToString()))
                                                {

                                                    decimal amount = Convert.ToDecimal(hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].amount_show);
                                                    amount = (amount / APIROE) * UserROE;
                                                    amount = Convert.ToDecimal(APIMarkupSection.AddPolicyMarkupByRule(APIConstant.RateHawkAPIID, amount, MarkupClassObj, APICurrencyName, 1, 1));
                                                    amount = Math.Ceiling(amount);
                                                    //if()



                                                    //LancellationPolicyStr += "From  <b>" + fromdate.AddDays(APIConstant.FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture) + "To <b>" + todate.AddDays(APIConstant.FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture) + "</b>  Cancellation Penalty will be <b>" + Sreq.CurrencyCode + " " + amount.ToString("0.00") + "</b>  <br/>";

                                                    if (hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.policies[cp].end_at == null)
                                                    {
                                                        LancellationPolicyStr += "From  <b>" + fromdate.AddDays(APIConstant.FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture) + "</b>  Cancellation Penalty will be <b>" + Sreq.CurrencyCode + " " + amount.ToString("0.00") + "</b>  <br/>";
                                                    }
                                                    else
                                                    {
                                                        LancellationPolicyStr += "From  <b>" + fromdate.AddDays(APIConstant.FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture) + "To <b>" + todate.AddDays(APIConstant.FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture) + "</b>  Cancellation Penalty will be <b>" + Sreq.CurrencyCode + " " + amount.ToString("0.00") + "</b>  <br/>";
                                                    }

                                                }


                                            }
                                        }
                                        CancelDate.Add(fromdate);
                                        CancelDate.Add(todate);

                                    }
                                }

                                //string freecanceldate = CancelDate.Min().ToString("dd MMM yyyy");
                                string freecanceldate = hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.free_cancellation_before != null ? DateTime.ParseExact(
                                    hotel.rates[i].payment_options.payment_types[0].cancellation_penalties.free_cancellation_before.ToString(),
                                    "M/d/yyyy h:mm:ss tt",
                                    CultureInfo.InvariantCulture
                                ).ToString("dd MM yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact(
                                   DateTime.Now.Date.ToString(),
                                    "M/d/yyyy h:mm:ss tt",
                                    CultureInfo.InvariantCulture
                                ).ToString("dd MM yyyy", CultureInfo.InvariantCulture);


                                if (EHotelRoomDetailobj.IsFreeCostCancellationDate)
                                {

                                    //EHotelRoomDetailobj.FreeCostCancellationDate = Convert.ToDateTime(freecanceldate).AddDays(APIConstant.FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);

                                    DateTime parsedFreeCancelDate = DateTime.ParseExact(freecanceldate, "dd MM yyyy", CultureInfo.InvariantCulture);

                                    EHotelRoomDetailobj.FreeCostCancellationDate = parsedFreeCancelDate.AddDays(APIConstant.FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);

                                }
                                else
                                {
                                    EHotelRoomDetailobj.FreeCostCancellationDate = DateTime.Now.AddDays(APIConstant.FreeCancellationBufferPeriod).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
                                }
                                //EHotelRoomDetailobj.FreeCostCancellationDate = Convert.ToDateTime(EHotelRoomDetailobj.FreeCostCancellationDate).AddDays(-1).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);


                                DateTime today = DateTime.Now;
                                if (today > Convert.ToDateTime(EHotelRoomDetailobj.FreeCostCancellationDate))
                                {
                                    EHotelRoomDetailobj.IsFreeCostCancellationDate = false;

                                }
                                else
                                {
                                    EHotelRoomDetailobj.IsFreeCostCancellationDate = true;
                                }

                                EHotelRoomDetailobj.IsCanclePolicy = LancellationPolicyStr.Length != 0;
                                EHotelRoomDetailobj.CanclePolicy = LancellationPolicyStr;

                                EHotelRoomDetailobj.ViewCalculation = ViewCalculation;
                                EHotelRoomDetailobj.AllNightTotalRate = AllNightTotalRate;
                                EHotelRoomDetailobj.AllNigntOfferRate = AllNightOfferTotalRate;
                                EHotelRoomDetailobj.TotalRate = RoomRatePer;


                                //added for nonincluded tax
                                EHotelRoomDetailobj.AllNigntOfferRate = EHotelRoomDetailobj.AllNigntOfferRate + EHotelRoomDetailobj.TotalTax;
                                EHotelRoomDetailobj.AllNightTotalRate = EHotelRoomDetailobj.AllNightTotalRate + EHotelRoomDetailobj.TotalTax;
                                EHotelRoomDetailobj.TotalRate = RoomRatePer + ((EHotelRoomDetailobj.TotalTax / Sreq.Nights) / Sreq.TotalNoRoom);
                                //added for nonincluded tax


                                EHotelRoomDetailobj.ServiceAPIID = Convert.ToInt32(APISupplierID);
                                EHotelRoomDetailobj.BHRDID = BHRDID;

                                EHotelRoomDetailobj.EMealPlanUpgradeInfoObj = new EMealPlanUpgradeInfo();
                                EHotelRoomDetailobj.GMXMealPlanUpgradeInfo = new GMXMealPlanUpgradeInfo();
                                EHotelRoomDetailobj.GMXMealPlanUpgradeInfo.MealPlanDesc = EHotelRoomDetailobj.RoomMeals;
                                EHotelRoomDetailobj.GMXMealPlanUpgradeInfo.APCurrency = string.Empty;
                                EHotelRoomDetailobj.GMXMealPlanUpgradeInfo.ARCurrency = string.Empty;
                                EHotelRoomDetailobj.OfferInclusion = string.Empty;

                                //RateCalculation
                                RateCalculation.Add(ContractedHotelConstant.Service_Charges, 0);
                                RateCalculation.Add(ContractedHotelConstant.Service_Charges_Value, "0");
                                RateCalculation.Add(ContractedHotelConstant.Service_Charges_CalType, "1");
                                RateCalculation.Add(ContractedHotelConstant.Extra_Fees, 0);
                                RateCalculation.Add(ContractedHotelConstant.Extra_Fees_Value, 0);
                                RateCalculation.Add(ContractedHotelConstant.Extra_Fees_CalType, "1");
                                RateCalculation.Add(ContractedHotelConstant.Extra_Fees_Desc, "Munipality Fees");
                                RateCalculation.Add(ContractedHotelConstant.Input_Tax, 0);
                                RateCalculation.Add(ContractedHotelConstant.Output_Tax, EHotelRoomDetailobj.TotalTax);
                                RateCalculation.Add(ContractedHotelConstant.Base_Room_Rate_OfferDiscount, Discount);
                                RateCalculation.Add(ContractedHotelConstant.Base_Room_Rate_OfferName, Convert.ToString(EHotelRoomDetailobj.OfferName));
                                EHotelRoomDetailobj.RateCalculation = RateCalculation;


                                List<EHotelRoomImages> HotelRoomImages = new List<EHotelRoomImages>();

                                EHotelRoomDetailobj.HotelRoomImages = HotelRoomImages;//need to add

                                List<EHotelRoomAminities> HotelRoomAmenities = new List<EHotelRoomAminities>();

                                EHotelRoomDetailobj.HotelRoomAminities = HotelRoomAmenities;//need to add

                            }
                        }

                    }
                }







            }
            catch (Exception ex)
            {

            }
            finally
            {
                //File.WriteAllText(FolderPath + "/AfterValuationResponse" + Guid.NewGuid().ToString().Replace('-', '_') + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.CreateSpecificCulture("en-US")) + ".txt", Convert.ToString(EHotelRoomDetailobj));

            }

            return EHotelRoomDetailobj;
        }

        #endregion

    }


    public class RateHawkClass
    {
        public RateHawkClass() { }

        #region Region Data class

        public class regiondumpclass
        {
            public class Center
            {
                public double longitude { get; set; }
                public double latitude { get; set; }
            }

            public class CountryName
            {
                public string ar { get; set; }
                public string bg { get; set; }
                public string cs { get; set; }
                public string da { get; set; }
                public string de { get; set; }
                public string el { get; set; }
                public string en { get; set; }
                public string es { get; set; }
                public string fi { get; set; }
                public string fr { get; set; }
                public string he { get; set; }
                public string hu { get; set; }
                public string it { get; set; }
                public string ja { get; set; }
                public string kk { get; set; }
                public string ko { get; set; }
                public string nl { get; set; }
                public string no { get; set; }
                public string pl { get; set; }
                public string pt { get; set; }
                public string pt_PT { get; set; }
                public string ro { get; set; }
                public string ru { get; set; }
                public string sq { get; set; }
                public string sr { get; set; }
                public string sv { get; set; }
                public string th { get; set; }
                public string tr { get; set; }
                public string uk { get; set; }
                public string vi { get; set; }
                public string zh_CN { get; set; }
                public string zh_TW { get; set; }
            }

            public class Name
            {
                public string ar { get; set; }
                public string bg { get; set; }
                public string cs { get; set; }
                public string da { get; set; }
                public string de { get; set; }
                public string el { get; set; }
                public string en { get; set; }
                public string es { get; set; }
                public string fi { get; set; }
                public string fr { get; set; }
                public object he { get; set; }
                public string hu { get; set; }
                public string it { get; set; }
                public string ja { get; set; }
                public object kk { get; set; }
                public string ko { get; set; }
                public string nl { get; set; }
                public string no { get; set; }
                public string pl { get; set; }
                public object pt { get; set; }
                public string pt_PT { get; set; }
                public string ro { get; set; }
                public string ru { get; set; }
                public object sq { get; set; }
                public string sr { get; set; }
                public string sv { get; set; }
                public string th { get; set; }
                public string tr { get; set; }
                public string uk { get; set; }
                public string vi { get; set; }
                public string zh_CN { get; set; }
                public string zh_TW { get; set; }
            }

            public class Root
            {
                [BsonId]
                [BsonRepresentation(BsonType.ObjectId)]
                public string _id { get; set; } // Map the MongoDB _id field
                public CountryName country_name { get; set; }
                public string country_code { get; set; }
                public Center center { get; set; }
                public List<int> hids { get; set; }
                public List<string> hotels { get; set; }
                public string iata { get; set; }
                public int id { get; set; }
                public string type { get; set; }
                public Name name { get; set; }
            }
        }



        #endregion

        #region Hotel Data class

        public class HotelDumpclass
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class AmenityGroup
            {
                public List<string> amenities { get; set; }
                public List<string> non_free_amenities { get; set; }
                public string group_name { get; set; }
            }

            public class DescriptionStruct
            {
                public List<string> paragraphs { get; set; }
                public string title { get; set; }
            }

            public class Electricity
            {
                public List<int> frequency { get; set; }
                public List<int> voltage { get; set; }
                public List<string> sockets { get; set; }
            }

            public class Facts
            {
                public object floors_number { get; set; }
                public int? rooms_number { get; set; }
                public object year_built { get; set; }
                public object year_renovated { get; set; }
                public Electricity electricity { get; set; }
            }

            public class ImagesExt
            {
                public string url { get; set; }
                public string category_slug { get; set; }
            }

            public class KeysPickup
            {
                public string type { get; set; }
                public object phone { get; set; }
                public object email { get; set; }
                public object apartment_office_address { get; set; }
                public object apartment_extra_information { get; set; }
                public bool is_contactless { get; set; }
            }

            public class MetapolicyStruct
            {
                public List<object> internet { get; set; }
                public List<object> meal { get; set; }
                public List<object> children_meal { get; set; }
                public List<object> extra_bed { get; set; }
                public List<object> cot { get; set; }
                public List<object> pets { get; set; }
                public List<object> shuttle { get; set; }
                public List<object> parking { get; set; }
                public List<object> children { get; set; }
                public Visa visa { get; set; }
                public List<object> deposit { get; set; }
                public NoShow no_show { get; set; }
                public List<object> add_fee { get; set; }
                public List<object> check_in_check_out { get; set; }
            }

            public class NameStruct
            {
                public string bathroom { get; set; }
                public object bedding_type { get; set; }
                public string main_name { get; set; }
            }

            public class NoShow
            {
                public string availability { get; set; }
                public object time { get; set; }
                public string day_period { get; set; }
            }

            public class Region
            {
                public int id { get; set; }
                public string country_code { get; set; }
                public object iata { get; set; }
                public string name { get; set; }
                public string type { get; set; }
                public string type_v2 { get; set; }
            }

            public class RgExt
            {
                public int @class { get; set; }
                public int quality { get; set; }
                public int sex { get; set; }
                public int bathroom { get; set; }
                public int bedding { get; set; }
                public int family { get; set; }
                public int capacity { get; set; }
                public int club { get; set; }
                public int bedrooms { get; set; }
                public int balcony { get; set; }
                public int floor { get; set; }
                public int view { get; set; }
            }

            public class RoomGroup
            {
                public int room_group_id { get; set; }
                public List<object> images { get; set; }
                public string name { get; set; }
                public List<string> room_amenities { get; set; }
                public RgExt rg_ext { get; set; }
                public NameStruct name_struct { get; set; }
            }

            public class Root
            {
                [BsonId]
                [BsonRepresentation(BsonType.ObjectId)]
                public string _id { get; set; } // Map the MongoDB _id field

                public string address { get; set; }
                public List<AmenityGroup> amenity_groups { get; set; }
                public string check_in_time { get; set; }
                public string check_out_time { get; set; }
                public List<DescriptionStruct> description_struct { get; set; }
                public string id { get; set; }
                public int hid { get; set; }
                public List<string> images { get; set; }
                public List<ImagesExt> images_ext { get; set; }
                public string kind { get; set; }
                public double latitude { get; set; }
                public double longitude { get; set; }
                public string name { get; set; }
                public object phone { get; set; }
                public List<object> policy_struct { get; set; }
                public object postal_code { get; set; }
                public List<RoomGroup> room_groups { get; set; }
                public Region region { get; set; }
                public int star_rating { get; set; }
                public object email { get; set; }
                public List<object> serp_filters { get; set; }
                public bool deleted { get; set; }
                public bool is_closed { get; set; }
                public bool is_gender_specification_required { get; set; }
                public MetapolicyStruct metapolicy_struct { get; set; }
                public object metapolicy_extra_info { get; set; }
                public object star_certificate { get; set; }
                public Facts facts { get; set; }
                public List<object> payment_methods { get; set; }
                public string hotel_chain { get; set; }
                public object front_desk_time_start { get; set; }
                public object front_desk_time_end { get; set; }
                public KeysPickup keys_pickup { get; set; }
            }

            public class Visa
            {
                public string visa_support { get; set; }
            }


        }

        #endregion

        #region HotelDataSearch class

        public class RateHawkHotelDataSearch
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class AmenityGroup
            {
                public List<string> amenities { get; set; }
                public string group_name { get; set; }
                public List<string> non_free_amenities { get; set; }
            }

            public class Data
            {
                public string address { get; set; }
                public List<AmenityGroup> amenity_groups { get; set; }
                public string check_in_time { get; set; }
                public string check_out_time { get; set; }
                public List<DescriptionStruct> description_struct { get; set; }
                public string email { get; set; }
                public Facts facts { get; set; }
                public string front_desk_time_end { get; set; }
                public string front_desk_time_start { get; set; }
                public int hid { get; set; }
                public string hotel_chain { get; set; }
                public string id { get; set; }
                public List<string> images { get; set; }
                public List<ImagesExt> images_ext { get; set; }
                public bool is_closed { get; set; }
                public bool is_gender_specification_required { get; set; }
                public KeysPickup keys_pickup { get; set; }
                public string kind { get; set; }
                public double latitude { get; set; }
                public double longitude { get; set; }
                public string metapolicy_extra_info { get; set; }
                public MetapolicyStruct metapolicy_struct { get; set; }
                public string name { get; set; }
                public List<string> payment_methods { get; set; }
                public string phone { get; set; }
                public List<PolicyStruct> policy_struct { get; set; }
                public string postal_code { get; set; }
                public Region region { get; set; }
                public List<RoomGroup> room_groups { get; set; }
                public List<string> serp_filters { get; set; }
                public object star_certificate { get; set; }
                public int star_rating { get; set; }
            }

            public class DescriptionStruct
            {
                public List<string> paragraphs { get; set; }
                public string title { get; set; }
            }

            public class Electricity
            {
                public List<int> frequency { get; set; }
                public List<string> sockets { get; set; }
                public List<int> voltage { get; set; }
            }

            public class Facts
            {
                public Electricity electricity { get; set; }
                public object floors_number { get; set; }
                public int? rooms_number { get; set; }
                public object year_built { get; set; }
                public object year_renovated { get; set; }
            }

            public class ImagesExt
            {
                public string category_slug { get; set; }
                public string url { get; set; }
            }

            public class KeysPickup
            {
                public string apartment_extra_information { get; set; }
                public string apartment_office_address { get; set; }
                public string email { get; set; }
                public bool is_contactless { get; set; }
                public string phone { get; set; }
                public string type { get; set; }
            }

            public class MetapolicyStruct
            {
                public List<object> add_fee { get; set; }
                public List<object> check_in_check_out { get; set; }
                public List<ChildPolicy> children { get; set; }
                public List<ChildrenMealPolicy> children_meal { get; set; }
                public List<CotPolicy> cot { get; set; }
                public List<DepositPolicy> deposit { get; set; }
                public List<ExtraBedPolicy> extra_bed { get; set; }
                public List<InternetPolicy> internet { get; set; }
                public List<MealPolicy> meal { get; set; }
                public NoShow no_show { get; set; }
                public List<ParkingPolicy> parking { get; set; }
                public List<PetsPolicy> pets { get; set; }
                public List<ShuttlePolicy> shuttle { get; set; }
                public Visa visa { get; set; }
            }
            public class ChildPolicy
            {
                public int age_start { get; set; }
                public int age_end { get; set; }
                public string currency { get; set; }
                public string extra_bed { get; set; }
                public string price { get; set; }
            }

            public class ChildrenMealPolicy
            {
                public int age_start { get; set; }
                public int age_end { get; set; }
                public string currency { get; set; }
                public string inclusion { get; set; }
                public string meal_type { get; set; }
                public string price { get; set; }
            }

            public class CotPolicy
            {
                public int amount { get; set; }
                public string currency { get; set; }
                public string inclusion { get; set; }
                public string price { get; set; }
                public string price_unit { get; set; }
            }

            public class DepositPolicy
            {
                public string availability { get; set; }
                public string currency { get; set; }
                public string deposit_type { get; set; }
                public string payment_type { get; set; }
                public string price { get; set; }
                public string price_unit { get; set; }
                public string pricing_method { get; set; }
            }

            public class ExtraBedPolicy
            {
                public int amount { get; set; }
                public string currency { get; set; }
                public string inclusion { get; set; }
                public string price { get; set; }
                public string price_unit { get; set; }
            }

            public class InternetPolicy
            {
                public string currency { get; set; }
                public string inclusion { get; set; }
                public string internet_type { get; set; }
                public string price { get; set; }
                public string price_unit { get; set; }
                public string work_area { get; set; }
            }

            public class MealPolicy
            {
                public string currency { get; set; }
                public string inclusion { get; set; }
                public string meal_type { get; set; }
                public string price { get; set; }
            }

            public class ParkingPolicy
            {
                public string currency { get; set; }
                public string inclusion { get; set; }
                public string price { get; set; }
                public string price_unit { get; set; }
                public string territory_type { get; set; }
            }

            public class PetsPolicy
            {
                public string currency { get; set; }
                public string inclusion { get; set; }
                public string pets_type { get; set; }
                public string price { get; set; }
                public string price_unit { get; set; }
            }

            public class ShuttlePolicy
            {
                public string currency { get; set; }
                public string destination_type { get; set; }
                public string inclusion { get; set; }
                public string price { get; set; }
                public string shuttle_type { get; set; }
            }

            public class NameStruct
            {
                public string bathroom { get; set; }
                public string bedding_type { get; set; }
                public string main_name { get; set; }
            }

            public class NoShow
            {
                public string availability { get; set; }
                public string day_period { get; set; }
                public string time { get; set; }
            }

            public class PolicyStruct
            {
                public List<string> paragraphs { get; set; }
                public string title { get; set; }
            }

            public class Region
            {
                public string country_code { get; set; }
                public string iata { get; set; }
                public int id { get; set; }
                public string name { get; set; }
                public string type { get; set; }
            }

            public class RgExt
            {
                public int balcony { get; set; }
                public int bathroom { get; set; }
                public int bedding { get; set; }
                public int bedrooms { get; set; }
                public int capacity { get; set; }
                public int @class { get; set; }
                public int club { get; set; }
                public int family { get; set; }
                public int floor { get; set; }
                public int quality { get; set; }
                public int sex { get; set; }
                public int view { get; set; }
            }

            public class RoomGroup
            {
                public List<string> images { get; set; }
                public List<ImagesExt> images_ext { get; set; }
                public string name { get; set; }
                public NameStruct name_struct { get; set; }
                public RgExt rg_ext { get; set; }
                public List<string> room_amenities { get; set; }
                public int room_group_id { get; set; }
            }

            public class Root
            {
                public Data data { get; set; }
                public object debug { get; set; }
                public object error { get; set; }
                public string status { get; set; }
            }

            public class Visa
            {
                public string visa_support { get; set; }
            }


        }

        #endregion

        #region search class

        public class RegionSearchClass
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class CancellationPenalties
            {
                public List<Policy> policies { get; set; }
                public DateTime? free_cancellation_before { get; set; }
            }

            public class Charge
            {
                public string amount_gross { get; set; }
                public string amount_net { get; set; }
                public string amount_commission { get; set; }
            }

            public class CommissionInfo
            {
                public Show show { get; set; }
                public Charge charge { get; set; }
            }

            public class Data
            {
                public List<Hotel> hotels { get; set; }
                public int total_hotels { get; set; }
            }

            public class Debug
            {
                public Request request { get; set; }
                public int key_id { get; set; }
                public object validation_error { get; set; }
            }

            public class Deposit
            {
                public string amount { get; set; }
                public string currency_code { get; set; }
                public bool is_refundable { get; set; }
            }

            public class Guest
            {
                public int adults { get; set; }
                public List<object> children { get; set; }
            }

            public class Hotel
            {
                public string id { get; set; }
                public List<Rate> rates { get; set; }
                public object bar_price_data { get; set; }
            }

            public class NoShow
            {
                public string amount { get; set; }
                public string currency_code { get; set; }
                public string from_time { get; set; }
            }

            public class PaymentOptions
            {
                public List<PaymentType> payment_types { get; set; }
            }

            public class PaymentType
            {
                public string amount { get; set; }
                public string show_amount { get; set; }
                public string currency_code { get; set; }
                public string show_currency_code { get; set; }
                public object by { get; set; }
                public bool is_need_credit_card_data { get; set; }
                public bool is_need_cvc { get; set; }
                public string type { get; set; }
                public VatData vat_data { get; set; }
                public TaxData tax_data { get; set; }
                public Perks perks { get; set; }
                public CommissionInfo commission_info { get; set; }
                public CancellationPenalties cancellation_penalties { get; set; }
                public RecommendedPrice recommended_price { get; set; }
            }

            public class Perks
            {
            }

            public class Policy
            {
                public DateTime? start_at { get; set; }
                public DateTime? end_at { get; set; }
                public string amount_charge { get; set; }
                public string amount_show { get; set; }
                public CommissionInfo commission_info { get; set; }
            }

            public class Rate
            {
                public string match_hash { get; set; }
                public List<string> daily_prices { get; set; }
                public string meal { get; set; }
                public PaymentOptions payment_options { get; set; }
                public object bar_rate_price_data { get; set; }
                public RgExt rg_ext { get; set; }
                public string room_name { get; set; }
                public object room_name_info { get; set; }
                public List<string> serp_filters { get; set; }
                public object sell_price_limits { get; set; }
                public int? allotment { get; set; }
                public List<string> amenities_data { get; set; }
                public bool any_residency { get; set; }
                public Deposit deposit { get; set; }
                public NoShow no_show { get; set; }
                public RoomDataTrans room_data_trans { get; set; }
            }

            public class RecommendedPrice
            {
                public string amount { get; set; }
                public string show_amount { get; set; }
                public string currency_code { get; set; }
                public string show_currency_code { get; set; }
            }

            public class Request
            {
                public string checkin { get; set; }
                public string checkout { get; set; }
                public string residency { get; set; }
                public string language { get; set; }
                public List<Guest> guests { get; set; }
                public int region_id { get; set; }
                public string currency { get; set; }
            }

            public class RgExt
            {
                public int @class { get; set; }
                public int quality { get; set; }
                public int sex { get; set; }
                public int bathroom { get; set; }
                public int bedding { get; set; }
                public int family { get; set; }
                public int capacity { get; set; }
                public int club { get; set; }
            }

            public class RoomDataTrans
            {
                public string main_room_type { get; set; }
                public string main_name { get; set; }
                public object bathroom { get; set; }
                public string bedding_type { get; set; }
                public string misc_room_type { get; set; }
            }

            public class Root
            {
                public Data data { get; set; }
                public Debug debug { get; set; }
                public string status { get; set; }
                public object error { get; set; }
            }

            public class Show
            {
                public string amount_gross { get; set; }
                public string amount_net { get; set; }
                public string amount_commission { get; set; }
            }

            public class TaxData
            {
            }

            public class VatData
            {
                public bool included { get; set; }
                public string value { get; set; }
            }


        }

        public class GeoSearchClass
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class CancellationPenalties
            {
                public List<Policy> policies { get; set; }
                public DateTime free_cancellation_before { get; set; }
            }

            public class Charge
            {
                public string amount_gross { get; set; }
                public string amount_net { get; set; }
                public string amount_commission { get; set; }
            }

            public class CommissionInfo
            {
                public Show show { get; set; }
                public Charge charge { get; set; }
            }

            public class Data
            {
                public List<Hotel> hotels { get; set; }
                public int total_hotels { get; set; }
            }

            public class Debug
            {
                public Request request { get; set; }
                public int key_id { get; set; }
                public object validation_error { get; set; }
            }

            public class Deposit
            {
                public string amount { get; set; }
                public string currency_code { get; set; }
                public bool is_refundable { get; set; }
            }

            public class Guest
            {
                public int adults { get; set; }
                public List<object> children { get; set; }
            }

            public class Hotel
            {
                public string id { get; set; }
                public List<Rate> rates { get; set; }
                public object bar_price_data { get; set; }
            }

            public class NoShow
            {
                public string amount { get; set; }
                public string currency_code { get; set; }
                public string from_time { get; set; }
            }

            public class PaymentOptions
            {
                public List<PaymentType> payment_types { get; set; }
            }

            public class PaymentType
            {
                public string amount { get; set; }
                public string show_amount { get; set; }
                public string currency_code { get; set; }
                public string show_currency_code { get; set; }
                public object by { get; set; }
                public bool is_need_credit_card_data { get; set; }
                public bool is_need_cvc { get; set; }
                public string type { get; set; }
                public VatData vat_data { get; set; }
                public TaxData tax_data { get; set; }
                public Perks perks { get; set; }
                public CommissionInfo commission_info { get; set; }
                public CancellationPenalties cancellation_penalties { get; set; }
                public RecommendedPrice recommended_price { get; set; }
            }

            public class Perks
            {
            }

            public class Policy
            {
                public DateTime? start_at { get; set; }
                public DateTime? end_at { get; set; }
                public string amount_charge { get; set; }
                public string amount_show { get; set; }
                public CommissionInfo commission_info { get; set; }
            }

            public class Rate
            {
                public string match_hash { get; set; }
                public List<string> daily_prices { get; set; }
                public string meal { get; set; }
                public PaymentOptions payment_options { get; set; }
                public object bar_rate_price_data { get; set; }
                public RgExt rg_ext { get; set; }
                public string room_name { get; set; }
                public object room_name_info { get; set; }
                public List<string> serp_filters { get; set; }
                public object sell_price_limits { get; set; }
                public int? allotment { get; set; }
                public List<string> amenities_data { get; set; }
                public bool any_residency { get; set; }
                public Deposit deposit { get; set; }
                public NoShow no_show { get; set; }
                public RoomDataTrans room_data_trans { get; set; }
            }

            public class RecommendedPrice
            {
                public string amount { get; set; }
                public string currency_code { get; set; }
                public string show_amount { get; set; }
                public string show_currency_code { get; set; }
            }

            public class Request
            {
                public string checkin { get; set; }
                public string checkout { get; set; }
                public string residency { get; set; }
                public string language { get; set; }
                public List<Guest> guests { get; set; }
                public double longitude { get; set; }
                public double latitude { get; set; }
                public int radius { get; set; }
                public string currency { get; set; }
            }

            public class RgExt
            {
                public int @class { get; set; }
                public int quality { get; set; }
                public int sex { get; set; }
                public int bathroom { get; set; }
                public int bedding { get; set; }
                public int family { get; set; }
                public int capacity { get; set; }
                public int club { get; set; }
            }

            public class RoomDataTrans
            {
                public string main_room_type { get; set; }
                public string main_name { get; set; }
                public object bathroom { get; set; }
                public string bedding_type { get; set; }
                public string misc_room_type { get; set; }
            }

            public class Root
            {
                public Data data { get; set; }
                public Debug debug { get; set; }
                public string status { get; set; }
                public object error { get; set; }
            }

            public class Show
            {
                public string amount_gross { get; set; }
                public string amount_net { get; set; }
                public string amount_commission { get; set; }
            }

            public class TaxData
            {
            }

            public class VatData
            {
                public bool included { get; set; }
                public string value { get; set; }
            }


        }


        public class HotelSearchClass
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class CancellationPenalties
            {
                public List<Policy> policies { get; set; }
                public DateTime free_cancellation_before { get; set; }
            }

            public class Charge
            {
                public string amount_gross { get; set; }
                public string amount_net { get; set; }
                public string amount_commission { get; set; }
            }

            public class CommissionInfo
            {
                public Show show { get; set; }
                public Charge charge { get; set; }
            }

            public class Data
            {
                public List<Hotel> hotels { get; set; }
                public int total_hotels { get; set; }
            }

            public class Debug
            {
                public Request request { get; set; }
                public int key_id { get; set; }
                public object validation_error { get; set; }
            }

            public class Deposit
            {
                public string amount { get; set; }
                public string currency_code { get; set; }
                public bool is_refundable { get; set; }
            }

            public class Guest
            {
                public int adults { get; set; }
                public List<object> children { get; set; }
            }

            public class Hotel
            {
                public string id { get; set; }
                public List<Rate> rates { get; set; }
                public object bar_price_data { get; set; }
            }

            public class NoShow
            {
                public string amount { get; set; }
                public string currency_code { get; set; }
                public string from_time { get; set; }
            }

            public class PaymentOptions
            {
                public List<PaymentType> payment_types { get; set; }
            }

            public class PaymentType
            {
                public string amount { get; set; }
                public string show_amount { get; set; }
                public string currency_code { get; set; }
                public string show_currency_code { get; set; }
                public object by { get; set; }
                public bool is_need_credit_card_data { get; set; }
                public bool is_need_cvc { get; set; }
                public string type { get; set; }
                public VatData vat_data { get; set; }
                public TaxData tax_data { get; set; }
                public Perks perks { get; set; }
                public CommissionInfo commission_info { get; set; }
                public CancellationPenalties cancellation_penalties { get; set; }
                public RecommendedPrice recommended_price { get; set; }
            }

            public class Perks
            {
            }

            public class Policy
            {
                public DateTime? start_at { get; set; }
                public DateTime? end_at { get; set; }
                public string amount_charge { get; set; }
                public string amount_show { get; set; }
                public CommissionInfo commission_info { get; set; }
            }

            public class Rate
            {
                public string match_hash { get; set; }
                public List<string> daily_prices { get; set; }
                public string meal { get; set; }
                public PaymentOptions payment_options { get; set; }
                public object bar_rate_price_data { get; set; }
                public RgExt rg_ext { get; set; }
                public string room_name { get; set; }
                public object room_name_info { get; set; }
                public List<string> serp_filters { get; set; }
                public object sell_price_limits { get; set; }
                public int? allotment { get; set; }
                public List<string> amenities_data { get; set; }
                public bool any_residency { get; set; }
                public Deposit deposit { get; set; }
                public NoShow no_show { get; set; }
                public RoomDataTrans room_data_trans { get; set; }
            }

            public class RecommendedPrice
            {
                public string amount { get; set; }
                public string show_amount { get; set; }
                public string currency_code { get; set; }
                public string show_currency_code { get; set; }
            }

            public class Request
            {
                public string checkin { get; set; }
                public string checkout { get; set; }
                public string residency { get; set; }
                public string language { get; set; }
                public List<Guest> guests { get; set; }
                public List<string> ids { get; set; }
                public string currency { get; set; }
            }

            public class RgExt
            {
                public int @class { get; set; }
                public int quality { get; set; }
                public int sex { get; set; }
                public int bathroom { get; set; }
                public int bedding { get; set; }
                public int family { get; set; }
                public int capacity { get; set; }
                public int club { get; set; }
            }

            public class RoomDataTrans
            {
                public string main_room_type { get; set; }
                public string main_name { get; set; }
                public object bathroom { get; set; }
                public string bedding_type { get; set; }
                public string misc_room_type { get; set; }
            }

            public class Root
            {
                public Data data { get; set; }
                public Debug debug { get; set; }
                public string status { get; set; }
                public object error { get; set; }
            }

            public class Show
            {
                public string amount_gross { get; set; }
                public string amount_net { get; set; }
                public string amount_commission { get; set; }
            }

            public class TaxData
            {
            }

            public class VatData
            {
                public bool included { get; set; }
                public string value { get; set; }
            }


        }

        #endregion

        #region details class

        public class RateHawkHotelDetail
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class CancellationPenalties
            {
                public List<Policy> policies { get; set; }
                public DateTime? free_cancellation_before { get; set; }
            }

            public class Charge
            {
                public string amount_gross { get; set; }
                public string amount_net { get; set; }
                public string amount_commission { get; set; }
            }

            public class CommissionInfo
            {
                public Show show { get; set; }
                public Charge charge { get; set; }
            }

            public class Data
            {
                public List<Hotel> hotels { get; set; }
            }

            public class Debug
            {
                public Request request { get; set; }
                public int key_id { get; set; }
                public object validation_error { get; set; }
            }

            public class Guest
            {
                public int adults { get; set; }
                public List<object> children { get; set; }
            }

            public class Hotel
            {
                public string id { get; set; }
                public int hid { get; set; }
                public List<Rate> rates { get; set; }
                public object bar_price_data { get; set; }
            }

            public class MealData
            {
                public string value { get; set; }
                public bool has_breakfast { get; set; }
                public bool no_child_meal { get; set; }
            }

            public class PaymentOptions
            {
                public List<PaymentType> payment_types { get; set; }
            }

            public class PaymentType
            {
                public string amount { get; set; }
                public string show_amount { get; set; }
                public string currency_code { get; set; }
                public string show_currency_code { get; set; }
                public object by { get; set; }
                public bool is_need_credit_card_data { get; set; }
                public bool is_need_cvc { get; set; }
                public string type { get; set; }
                public VatData vat_data { get; set; }
                public TaxData tax_data { get; set; }
                public Perks perks { get; set; }
                public CommissionInfo commission_info { get; set; }
                public CancellationPenalties cancellation_penalties { get; set; }
                public object recommended_price { get; set; }
            }

            public class Perks
            {
                public List<EarlyCheckin> early_checkin { get; set; }
                public List<LateCheckout> late_checkout { get; set; }
            }


            public class EarlyCheckin
            {
                public string charge_price { get; set; }
                public string show_price { get; set; }
                public CommissionInfo commission_info { get; set; }
                public string time { get; set; }
                public bool is_requested { get; set; }
            }

            public class LateCheckout
            {
                public string charge_price { get; set; }
                public string show_price { get; set; }
                public CommissionInfo commission_info { get; set; }
                public string time { get; set; }
                public bool is_requested { get; set; }
            }
            public class Policy
            {
                public DateTime? start_at { get; set; }
                public DateTime? end_at { get; set; }
                public string amount_charge { get; set; }
                public string amount_show { get; set; }
                public CommissionInfo commission_info { get; set; }
            }

            public class Rate
            {
                public string book_hash { get; set; }
                public string match_hash { get; set; }
                public List<string> daily_prices { get; set; }
                public string meal { get; set; }
                public MealData meal_data { get; set; }
                public PaymentOptions payment_options { get; set; }
                public object bar_rate_price_data { get; set; }
                public RgExt rg_ext { get; set; }
                public string room_name { get; set; }
                public object room_name_info { get; set; }
                public List<string> serp_filters { get; set; }
                public object sell_price_limits { get; set; }
                public int allotment { get; set; }
                public List<string> amenities_data { get; set; }
                public bool any_residency { get; set; }
                public object deposit { get; set; }
                public object no_show { get; set; }
                public RoomDataTrans room_data_trans { get; set; }
                public object legal_info { get; set; }
            }

            public class Request
            {
                public string checkin { get; set; }
                public string checkout { get; set; }
                public string residency { get; set; }
                public string language { get; set; }
                public List<Guest> guests { get; set; }
                public string id { get; set; }
                public string currency { get; set; }
                public Upsells upsells { get; set; }

            }


            public class Upsells
            {
                public EarlyCheckin early_checkin { get; set; }
                public LateCheckout late_checkout { get; set; }
                public bool only_eclc { get; set; }
                public bool multiple_eclc { get; set; }
            }

            public class RgExt
            {
                public int @class { get; set; }
                public int quality { get; set; }
                public int sex { get; set; }
                public int bathroom { get; set; }
                public int bedding { get; set; }
                public int family { get; set; }
                public int capacity { get; set; }
                public int club { get; set; }
                public int bedrooms { get; set; }
                public int balcony { get; set; }
                public int view { get; set; }
                public int floor { get; set; }
            }

            public class RoomDataTrans
            {
                public string main_room_type { get; set; }
                public string main_name { get; set; }
                public object bathroom { get; set; }
                public string bedding_type { get; set; }
                public string misc_room_type { get; set; }
            }

            public class Root
            {
                public Data data { get; set; }
                public Debug debug { get; set; }
                public string status { get; set; }
                public object error { get; set; }
            }

            public class Show
            {
                public string amount_gross { get; set; }
                public string amount_net { get; set; }
                public string amount_commission { get; set; }
            }

            public class TaxData
            {
                public List<Taxis> taxes { get; set; }
            }

            public class Taxis
            {
                public string name { get; set; }
                public bool included_by_supplier { get; set; }
                public string amount { get; set; }
                public string currency_code { get; set; }
            }

            public class VatData
            {
                public bool included { get; set; }
                public bool applied { get; set; }
                public string amount { get; set; }
                public string currency_code { get; set; }
                public string value { get; set; }
            }


        }

        #endregion

        #region Prebook class

        public class RateHawkPrebook
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class CancellationPenalties
            {
                public List<Policy> policies { get; set; }
                public DateTime free_cancellation_before { get; set; }
            }

            public class Changes
            {
                public bool price_changed { get; set; }
            }

            public class Charge
            {
                public string amount_gross { get; set; }
                public string amount_net { get; set; }
                public string amount_commission { get; set; }
            }

            public class CommissionInfo
            {
                public Show show { get; set; }
                public Charge charge { get; set; }
            }

            public class Data
            {
                public List<Hotel> hotels { get; set; }
                public Changes changes { get; set; }
            }

            public class Debug
            {
                public Request request { get; set; }
                public int key_id { get; set; }
                public object validation_error { get; set; }
            }

            public class Hotel
            {
                public string id { get; set; }
                public int hid { get; set; }
                public List<Rate> rates { get; set; }
                public object bar_price_data { get; set; }
            }

            public class MealData
            {
                public string value { get; set; }
                public bool has_breakfast { get; set; }
                public bool no_child_meal { get; set; }
            }

            public class PaymentOptions
            {
                public List<PaymentType> payment_types { get; set; }
            }

            public class PaymentType
            {
                public string amount { get; set; }
                public string show_amount { get; set; }
                public string currency_code { get; set; }
                public string show_currency_code { get; set; }
                public object by { get; set; }
                public bool is_need_credit_card_data { get; set; }
                public bool is_need_cvc { get; set; }
                public string type { get; set; }
                public VatData vat_data { get; set; }
                public TaxData tax_data { get; set; }
                public Perks perks { get; set; }
                public CommissionInfo commission_info { get; set; }
                public CancellationPenalties cancellation_penalties { get; set; }
                public object recommended_price { get; set; }
            }

            public class Perks
            {
            }

            public class Policy
            {
                public DateTime? start_at { get; set; }
                public DateTime? end_at { get; set; }
                public string amount_charge { get; set; }
                public string amount_show { get; set; }
                public CommissionInfo commission_info { get; set; }
            }

            public class Rate
            {
                public string book_hash { get; set; }
                public string match_hash { get; set; }
                public List<string> daily_prices { get; set; }
                public string meal { get; set; }
                public MealData meal_data { get; set; }
                public PaymentOptions payment_options { get; set; }
                public object bar_rate_price_data { get; set; }
                public RgExt rg_ext { get; set; }
                public string room_name { get; set; }
                public object room_name_info { get; set; }
                public List<string> serp_filters { get; set; }
                public object sell_price_limits { get; set; }
                public int allotment { get; set; }
                public List<string> amenities_data { get; set; }
                public bool any_residency { get; set; }
                public object deposit { get; set; }
                public object no_show { get; set; }
                public RoomDataTrans room_data_trans { get; set; }
                public object legal_info { get; set; }
            }

            public class Request
            {
                public string hash { get; set; }
                public int price_increase_percent { get; set; }
            }

            public class RgExt
            {
                public int @class { get; set; }
                public int quality { get; set; }
                public int sex { get; set; }
                public int bathroom { get; set; }
                public int bedding { get; set; }
                public int family { get; set; }
                public int capacity { get; set; }
                public int club { get; set; }
                public int bedrooms { get; set; }
                public int balcony { get; set; }
                public int view { get; set; }
                public int floor { get; set; }
            }

            public class RoomDataTrans
            {
                public string main_room_type { get; set; }
                public string main_name { get; set; }
                public object bathroom { get; set; }
                public object bedding_type { get; set; }
                public object misc_room_type { get; set; }
            }

            public class Root
            {
                public Data data { get; set; }
                public Debug debug { get; set; }
                public string status { get; set; }
                public object error { get; set; }
            }

            public class Show
            {
                public string amount_gross { get; set; }
                public string amount_net { get; set; }
                public string amount_commission { get; set; }
            }

            public class TaxData
            {
                public List<Taxis> taxes { get; set; }
            }

            public class Taxis
            {
                public string name { get; set; }
                public bool included_by_supplier { get; set; }
                public string amount { get; set; }
                public string currency_code { get; set; }
            }

            public class VatData
            {
                public bool included { get; set; }
                public bool applied { get; set; }
                public string amount { get; set; }
                public string currency_code { get; set; }
                public string value { get; set; }
            }


        }

        #endregion

        #region Book class

        public class RateHawkOrderBookingForm
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class Data
            {
                public bool is_gender_specification_required { get; set; }
                public int item_id { get; set; }
                public int order_id { get; set; }
                public string partner_order_id { get; set; }
                public List<PaymentType> payment_types { get; set; }
                //public List<upselldata> upsell_data { get; set; }
                public List<object> upsell_data { get; set; }
            }

            public class PaymentType
            {
                public string amount { get; set; }
                public string currency_code { get; set; }
                public bool is_need_credit_card_data { get; set; }
                public bool is_need_cvc { get; set; }
                public object recommended_price { get; set; }
                public string type { get; set; }
            }


            public class upselldata
            {
                public string name { get; set; }
                public string uid { get; set; }
            }

            public class Root
            {
                public Data data { get; set; }
                public object debug { get; set; }
                public object error { get; set; }
                public string status { get; set; }
            }


        }

        #endregion

        #region OrderBookingFinish


        public class RateHawkOrderBookingFinishReq
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class Guest
            {
                public string first_name { get; set; }
                public string last_name { get; set; }

                public bool is_child { get; set; }
                public int? age { get; set; }
                public string gender { get; set; }


            }

            public class Partner
            {
                public string partner_order_id { get; set; }
                public string comment { get; set; }
                public string amount_sell_b2b2c { get; set; }
            }

            public class PaymentType
            {
                public string type { get; set; }
                public string amount { get; set; }
                public string currency_code { get; set; }
            }

            public class Room
            {
                public List<Guest> guests { get; set; }
            }


            public class upselldata
            {
                public string name { get; set; }
                public string uid { get; set; }
            }

            public class Root
            {
                public User user { get; set; }
                public SupplierData supplier_data { get; set; }
                public Partner partner { get; set; }
                public string language { get; set; }
                public List<Room> rooms { get; set; }
                //public List<upselldata> upsell_data { get; set; }
                public List<object> upsell_data { get; set; }
                public PaymentType payment_type { get; set; }
            }

            public class SupplierData
            {
                public string first_name_original { get; set; }
                public string last_name_original { get; set; }
                public string phone { get; set; }
                public string email { get; set; }
            }

            public class User
            {
                public string email { get; set; }
                public string comment { get; set; }
                public string phone { get; set; }
            }


        }

        public class RateHawkOrderBookingFinish
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class Root
            {
                public object data { get; set; }
                public object debug { get; set; }
                public object error { get; set; }
                public string status { get; set; }
            }

        }

        #endregion

        #region OrderBookingFinishStatus

        public class RateHawkOrderBookingFinishStatus
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class Data
            {
                public object data_3ds { get; set; }
                public string partner_order_id { get; set; }
                public int percent { get; set; }
                public object prepayment { get; set; }
            }

            public class Root
            {
                public Data data { get; set; }
                public object debug { get; set; }
                public object error { get; set; }
                public string status { get; set; }
            }
        }

        #endregion OrderBookingFinishStatus

        #region cancel class
        public class RateHawkOrderCancel
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class AmountPayable
            {
                public string amount { get; set; }
                public object amount_info { get; set; }
                public string currency_code { get; set; }
            }

            public class AmountRefunded
            {
                public string amount { get; set; }
                public object amount_info { get; set; }
                public string currency_code { get; set; }
            }

            public class AmountSell
            {
                public string amount { get; set; }
                public object amount_info { get; set; }
                public string currency_code { get; set; }
            }

            public class Data
            {
                public AmountPayable amount_payable { get; set; }
                public AmountRefunded amount_refunded { get; set; }
                public AmountSell amount_sell { get; set; }
            }

            public class Root
            {
                public Data data { get; set; }
                public object debug { get; set; }
                public object error { get; set; }
                public string status { get; set; }
            }


        }

        #endregion

    }
}
