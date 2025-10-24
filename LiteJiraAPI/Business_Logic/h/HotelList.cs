using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for HotelList
/// </summary>
/// 

public class HotelList
{
    public HotelList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string HotelIndex { get; set; }
    public string HotelId { get; set; }
    public string HotelName { get; set; }
    public string ImageName { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public string Category { get; set; }
    public string CategoryId { get; set; }
    public string LocationId { get; set; }
    public string SeasonId { get; set; }
    public string HotelRate { get; set; }
    public string TotalRate { get; set; }
    public string CurrencyName { get; set; }
    public string OfferCount { get; set; }
    public string HotelAmenitie { get; set; }
    public string HotelOccupancy { get; set; }
    public string local { get; set; }
    public string Service { get; set; }
    public string hotelId_service { get; set; }
    public string h_Desc { get; set; }
    public string rate_supplier { get; set; }
    public string Address { get; set; }
    public string CheckInTime { get; set; }
    public string CheckOutTime { get; set; }
    public string Location { get; set; }
    public string TekTravel_TraceId { get; set; }
    public string ExpediaApi_SessionId { get; set; }
    public string HotelIdsString { get; set; }
    public string String_combination { get; set; }
    public List<HotelAminities> HotelAminities { get; set; }
    public List<HotelImageGallary> HotelImageGallary { get; set; }
    public List<HotelRoomDetail> HotelRoomDetail { get; set; }
    public List<HotelDetail> HotelDetail { get; set; }
    public string CanclePolicy { get; set; }
    public string LocalHotelIndex { get; set; }
    public string HotelRoomName { get; set; }
    public string Hotelmeals { get; set; }
    public string TotalNights { get; set; }
    public string CityCode { get; set; }
    public string HotelRoomHTML { get; set; }
    public string Nationality { get; set; }
    public string CityName { get; set; }
    public string CountryName { get; set; }
    public string ChainCode { get; set; }
    public string IsBookable { get; set; }


}

public class HotelAminities
{
    public string AminitieName { get; set; }
}

public class HotelDetail
{
    public string checkInTime { get; set; }
    public string checkOutTime { get; set; }
    public string propertyInformation { get; set; }
    public string areaInformation { get; set; }
    public string hotelPolicy { get; set; }
    public string roomInformation { get; set; }
    public string checkInInstructions { get; set; }
    public string locationDescription { get; set; }
    public string businessAmenitiesDescription { get; set; }
    public string roomDetailDescription { get; set; }
}

public class HotelImageGallary
{
    public string HotelId { get; set; }
    public string HotelName { get; set; }
    public string HotelImageId { get; set; }
    public string ImageText { get; set; }
    public string ImagePath { get; set; }
    public string ImageThumPath { get; set; }
    public string ImageActualPath { get; set; }
    public string ImageName { get; set; }
    public string DefaultId { get; set; }
}

public class HotelRoomDetail
{
    public string RoomNo { get; set; }
    public string RoomIndex { get; set; }
    public string RoomType { get; set; }
    public string RoomTypeId { get; set; }
    public string CurrencyName { get; set; }
    public string SurchargeCount { get; set; }
    public string OccuapncyAdult { get; set; }
    public string OccuapncyAdult2 { get; set; }
    public string OccuapncyChild { get; set; }
    public string OccuapncyChild2 { get; set; }
    public string RoomOccupancy { get; set; }
    public string IsOffer { get; set; }
    public string OfferCountName { get; set; }
    public string OfferCountForHeader { get; set; }
    public string PakagecountForHeader { get; set; }
    public string Alias { get; set; }
    public string RoomRate { get; set; }
    public string TotalRate { get; set; }
    public string AllRoomRate { get; set; }
    public string AllNightTotalRate { get; set; }
    public string AllRoomOfferRate { get; set; }
    public string AllNigntOfferRate { get; set; }
    public string SupplierRate { get; set; }
    public string hotelRoomTypeId { get; set; }
    public string PriceDetails { get; set; }
    public string RuleText { get; set; }
    public string IsCanclePolicy { get; set; }
    public string CanclePolicy { get; set; }
    public string ApiRoomRate { get; set; }
    public string ApiCurrency { get; set; }
    public string ApiAllNightTotalRate { get; set; }
    public string ExtraInfo { get; set; }
    public List<HotelRoomAminities> HotelRoomAminities { get; set; }
    public RoomDefaultInfo DefaultInfo { get; set; }
    public string FreeCostCancellationDate { get; set; }
    public string IsFreeCostCancellationDate { get; set; }
    public string IsErrata { get; set; }
    public string Errata { get; set; }
    public string BasePriceCalculation { get; set; }
    public string SelectedHotelDataID { get; set; }
    public string Sess_hdnlocal_tosn { get; set; }
    public string AvilableRoom { get; set; }
    public string LocalIdentityIndex { get; set; }
    public ViewCalculation View { get; set; }
    public string IsExtraBed { get; set; }
    public string Issharedbedding { get; set; }
}

public class HotelRoomAminities
{
    public string AminitieName { get; set; }
}

public class RoomDefaultInfo
{
    public string DefaultCurrancyName { get; set; }
    public string DefaultOneNightRate { get; set; }
    public string DefaultAllNightRate { get; set; }
    public string AgentMarkupType { get; set; }
    public string AgentMarkupValue { get; set; }
    public string ApiCurrencyRate { get; set; }
}


public class CityInfo
{
    public string CityId { get; set; }
    public string CityCode { get; set; }
    public string CityShortName { get; set; }
    public string CountryName { get; set; }
    public string CountryCode { get; set; }
}
public class ErrataInfo
{
    public string Errata { get; set; }
}