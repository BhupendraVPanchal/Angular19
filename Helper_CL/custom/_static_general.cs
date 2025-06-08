using Helper_CL.dal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Helper_CL.custom
{
    public static class _static_general
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static ControllerContext? context;

        public static string EncryptedText(string strToEncrypt)
        {
            _enc_dec enc = new _enc_dec();
            try
            {
                return enc.GetEncryptedText("shree247", strToEncrypt);
            }
            finally
            {
                // enc = null;
            }

        }

        public static string DecryptedText(string strToDecrypt)
        {
            _enc_dec dec = new _enc_dec();
            try
            {
                return dec.GetDecryptedText("shree247", strToDecrypt);
            }
            finally
            {
                // dec = null;
            }
        }

        public static void set_session(ControllerContext context, string key, string value)
        {
            context.HttpContext.Session.SetString(key, value);
        }

        public static string? get_session(ControllerContext context, string key)
        {
            return context.HttpContext.Session.GetString(key);
        }

        public static string GetSystemIp()
        {
            string ip = "";
            ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            return ip;
        }

        public static string GetHttpHeaders()
        {
            string ip = "";
            ip = context.HttpContext.Request.Headers["User-Agent"].ToString();
            return ip;
        }

        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".jfif", "image/jfif"}
            };
        }

        #region serialization
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //Serialized object
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream, Encoding.UTF8);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            var doc = new XmlDocument();
            doc.LoadXml(str);

            return doc.LastChild.InnerXml;
        }
        #endregion


        public static JObject TojObj<T>(T model)
        {
            PropertyInfo[] properties = model.GetType().GetProperties();
            JObject jObj = new JObject();
            foreach (PropertyInfo property in properties)
            {
                jObj[property.Name] = property.GetValue(model)?.ToString();
            }
            properties = null;
            return jObj;
        }

        public static string serialize_help<T>(DataTable dt)
        {
            if (dt.ToListof<T>().ToArray().Length > 0)
            {
                return JsonConvert.SerializeObject(dt.ToListof<T>().ToArray());
            }
            else
            {
                return null;
            }
        }


        public static string format_currency(decimal? number, string currency_short_code)
        {
            try
            {
                //List<CultureInfo> culture_info = culture_info_by_currency_shortcode(currency_short_code).ToList();
                CultureInfo culture_info = currency_short_code == "INR" ? new CultureInfo("hi-IN") : new CultureInfo("en-US");
                return string.Format(culture_info, "{0:N0}", number);
            }
            catch
            {
                return number.ToString();
            }
        }

        public static IEnumerable<CultureInfo> culture_info_by_currency_shortcode(string currency_short_code)
        {
            if (currency_short_code == null)
            {
                throw new ArgumentNullException("Currency Short Code is not specified.");
            }

            foreach (var item in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo region_info = new RegionInfo(item.LCID);
                if (region_info.ISOCurrencySymbol == currency_short_code)
                {
                    yield return item;
                }
            }
            //return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            //    .Where(x => new RegionInfo(x.LCID).ISOCurrencySymbol == currency_short_code);
        }

        public static Dictionary<string, object> get_distance(string src, string dest, bool is_ride_trace = false) //&mode=walking
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            try
            {
                using (var wb = new WebClient())
                {
                    var response = wb.DownloadString(_site_config.GetConfigValue("g_url").Replace("##src##", src).Replace("##dest##", dest) +
                                    (is_ride_trace ? "&mode=walking" : ""));
                    try
                    {
                        obj.Add("distance", (Convert.ToDouble(JObject.Parse(response)["rows"][0]["elements"][0]["distance"]["value"]) / 1000.00).ToString());
                        obj.Add("time", Math.Round((Convert.ToDouble(JObject.Parse(response)["rows"][0]["elements"][0]["duration"]["value"]) / 60)).ToString());
                        return obj;
                    }
                    catch (Exception)
                    {
                        throw new Exception("Please enter valid address");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // obj.Clear();
            }


        }

        public static string driver_log_exception(string section_name, string exception_message, string request_input, string session, int? login_code)
        {
            string error_message = "";
            try
            {
                Dictionary<string, object> incPara = new Dictionary<string, object>();
                incPara.Add("section_name", section_name);
                incPara.Add("exception_message", exception_message);
                incPara.Add("request_input", request_input);
                incPara.Add("session", session);
                incPara.Add("login_code", login_code);
                DataTable dt = _data_access.GetDataTable("usp_driver_exception_log_add", incPara);
                incPara.Clear();
                if (dt != null && dt.Rows.Count > 0)
                {
                    error_message = dt.Rows[0]["error_message"].ToString();
                }
            }
            catch
            {
            }
            return error_message;
        }

        public static string rider_log_exception(string section_name, string exception_message, string request_input, string session, int? login_code)
        {
            string error_message = "";
            try
            {
                Dictionary<string, object> incPara = new Dictionary<string, object>();
                incPara.Add("section_name", section_name);
                incPara.Add("exception_message", exception_message);
                incPara.Add("request_input", request_input);
                incPara.Add("session", session);
                incPara.Add("login_code", login_code);
                DataTable dt = _data_access.GetDataTable("usp_rider_exception_log_add", incPara);
                incPara.Clear();
                if (dt != null && dt.Rows.Count > 0)
                {
                    error_message = dt.Rows[0]["error_message"].ToString();
                }
            }
            catch
            {
            }
            return error_message;
        }

        public static void driver_request_log(string driver_code, string request_headers, string request_body, string method, string request)
        {
            try
            {
                Dictionary<string, object> incPara = new Dictionary<string, object>();
                incPara.Add("driver_code", driver_code);
                incPara.Add("request_headers", request_headers);
                incPara.Add("request_body", request_body);
                incPara.Add("method", method);
                incPara.Add("request", request);
                _data_access.GetDataTable("dusp_driver_request_log_save", incPara);
                incPara.Clear();
            }
            catch
            {
            }
        }

        public static void rider_request_log(string rider_code, string request_headers, string request_body, string method, string request)
        {
            try
            {
                Dictionary<string, object> incPara = new Dictionary<string, object>();
                incPara.Add("rider_code", rider_code);
                incPara.Add("request_headers", request_headers);
                incPara.Add("request_body", request_body);
                incPara.Add("method", method);
                incPara.Add("request", request);
                _data_access.GetDataTable("rusp_rider_request_log_save", incPara);
                incPara.Clear();
            }
            catch
            {
            }
        }

        private static JObject convert_data_row_to_jobject(DataRow dr)
        {
            JObject data = new JObject();
            foreach (DataColumn c in dr.Table.Columns)
            {
                data[c.ColumnName] = dr[c.ColumnName] != null ? dr[c.ColumnName].ToString() : "-1";
            }
            //data["rider_code"] =  dr["rider_code"] != null ? dr["rider_code"].ToString() : "-1";
            //data["src"] =  dr["src"] != null ? dr["src"].ToString() : "-1";
            //data["dest"] =  dr["dest"] != null ? dr["dest"].ToString() : "-1";
            //data["src_lat"] = dr["src_lat"] != null ? dr["src_lat"].ToString() : "-1";
            //data["src_lng"] = dr["src_lng"] != null ? dr["src_lng"].ToString() : "-1";
            //data["eta"] =  dr["eta"] != null ? dr["eta"].ToString() : "10";
            return data;
        }

        public static Dictionary<string, object> convert_data_row_to_dictionary(DataRow dr)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            foreach (DataColumn c in dr.Table.Columns)
            {
                data.Add(c.ColumnName, dr[c.ColumnName] != null ? dr[c.ColumnName].ToString() : "-1");
            }
            data.Clear();
            return data;
        }


        public static void send_notification_driver(string token, DataRow dr, string notification, string title)
        {
            string str = notification_select(token, convert_data_row_to_jobject(dr), title, notification).ToString();
            var result = "-1";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_site_config.GetConfigValue("firebase_link"));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + _site_config.GetConfigValue("driver_firebase_server_key"));
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(str);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    Dictionary<string, object> inc_para = new Dictionary<string, object>();
                    inc_para.Add("request_input", convert_data_row_to_jobject(dr));
                    inc_para.Add("request_response", result + "|" + str);
                    inc_para.Add("driver_code", dr["driver_code"]);
                    _data_access.GetDataTable("cusp_firebase_response_log_add_edit", inc_para);
                    var res = result;
                }
                httpWebRequest.KeepAlive = false;
                // return result;
            }
            catch (Exception ex)
            {
                try
                {
                    Dictionary<string, object> inc_para = new Dictionary<string, object>();
                    inc_para.Add("request_input", convert_data_row_to_jobject(dr));
                    inc_para.Add("request_response", ex + "|" + str);
                    inc_para.Add("driver_code", dr["driver_code"]);
                    _data_access.GetDataTable("cusp_firebase_response_log_add_edit", inc_para);
                }
                catch { }
            }
            //finally
            //{
            //    str = null;
            //    result = null;
            //}
        }


        public static void send_notification_rider(string token, DataRow dr, string notification, string title)
        {
            string str = notification_select(token, convert_data_row_to_jobject(dr), title, notification).ToString();
            var result = "-1";
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(_site_config.GetConfigValue("firebase_link"));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + _site_config.GetConfigValue("rider_firebase_server_key"));
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(str);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    Dictionary<string, object> inc_para = new Dictionary<string, object>();
                    inc_para.Add("request_input", convert_data_row_to_jobject(dr));
                    inc_para.Add("request_response", result + "|" + str);
                    inc_para.Add("rider_code", dr["rider_code"]);
                    _data_access.GetDataTable("cusp_firebase_response_log_add_edit", inc_para);
                    inc_para = null;
                    var res = result;
                }
                httpWebRequest.KeepAlive = false;
                // return result;
            }
            catch (Exception ex)
            {
                try
                {
                    Dictionary<string, object> inc_para = new Dictionary<string, object>();
                    inc_para.Add("request_input", convert_data_row_to_jobject(dr));
                    inc_para.Add("request_response", ex + "|" + str);
                    inc_para.Add("rider_code", dr["rider_code"]);
                    _data_access.GetDataTable("cusp_firebase_response_log_add_edit", inc_para);
                    inc_para = null;
                }
                catch { }
            }
            finally
            {
                //str = null;
                //result = null;
            }
        }


        private static StringBuilder notification_select(string token, JObject data, string title, string msg)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                bool show_bg_notification = false;
                bool is_ios = false;
                try { show_bg_notification = (bool)data?["is_show_notification_in_bg"]; } catch { }
                try { is_ios = (bool)data?["is_ios"]; } catch { }

                sb.AppendLine("{");
                sb.AppendLine(" \"to\": \"" + token + "\",");
                sb.AppendLine(" \"mutable_content\": true,");
                sb.AppendLine(" \"content_available\": true,");
                sb.AppendLine(" \"priority\": \"high\",");
                if (show_bg_notification)
                {
                    //if (is_ios)
                    //{
                    //    sb.AppendLine(" \"notification\": {\"sound\": \"default\"},");
                    //}
                    //else 
                    //{
                    //    sb.AppendLine(" \"notification\": {\"body\": \"" + msg + "\",\"title\": \"" + title + "\"},");
                    //}
                    sb.AppendLine(" \"notification\": {\"body\": \"" + msg + "\",\"title\": \"" + title + "\"},");
                }
                else if (is_ios)
                {
                    sb.AppendLine(" \"notification\": {\"sound\": \"default\"},");
                }
                sb.AppendLine(" \"data\": {");
                sb.AppendLine("     \"body\": " + data);
                //sb.AppendLine("     \"body\": {");
                //sb.AppendLine("         \"booking_request_code\": \" " + data["booking_request_code"]?.ToString() + " \",");
                //sb.AppendLine("         \"rider_code\": \" " + data["rider_code"]?.ToString() + " \",");
                //sb.AppendLine("         \"src\": \" " + data["src"]?.ToString() + " \",");
                //sb.AppendLine("         \"dest\": \" " + data["dest"]?.ToString() + " \",");
                //sb.AppendLine("         \"src_lat\": \" " + data["src_lat"]?.ToString() + " \",");
                //sb.AppendLine("         \"src_lng\": \" " + data["src_lng"]?.ToString() + " \",");
                //sb.AppendLine("         \"eta\": \" " + data["eta"]?.ToString() + " \",");
                //sb.AppendLine("         \"NotificationType\": \"" + title + "\",");
                //sb.AppendLine("         \"MessageDateTime\": \"" + DateTime.Now + "\",");
                //sb.AppendLine("         \"Message\":\"" + msg + "\"");
                //sb.AppendLine("     }");
                sb.AppendLine(" },");
                sb.AppendLine(" \"android\": {");
                sb.AppendLine("     \"priority\": \"high\"");
                sb.AppendLine(" },");
                sb.AppendLine(" \"apns\": {");
                sb.AppendLine("     \"payload\": {");
                sb.AppendLine("         \"aps\": {");
                sb.AppendLine("             \"contentAvailable\": true");
                sb.AppendLine("         }");
                sb.AppendLine("      },");
                sb.AppendLine("      \"headers\": {");
                sb.AppendLine("         \"apns-push-type\": \"background\",");
                sb.AppendLine("         \"apns-priority\": \"5\",");
                sb.AppendLine("         \"apns-topic\": \"io.flutter.plugins.firebase.messaging\"");
                sb.AppendLine("      }");
                sb.AppendLine(" }");
                sb.AppendLine("}");
                return sb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    sb.Clear();
            //}


        }



        public static string hmac_sha256_digest(string message, string secret)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyBytes, messageBytes, bytes;
            try
            {

                keyBytes = encoding.GetBytes(secret);
                messageBytes = encoding.GetBytes(message);
                System.Security.Cryptography.HMACSHA256 cryptographer = new System.Security.Cryptography.HMACSHA256(keyBytes);
                bytes = cryptographer.ComputeHash(messageBytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //encoding = null;
                //keyBytes = null;
                //messageBytes = null;
                //bytes = null;
            }

        }

        public static void send_sms(Dictionary<string, string> data)
        {
            try
            {
                if (!data.ContainsKey("short_code"))
                {
                    throw new Exception("Sms short code not provided!");
                }
                Dictionary<string, object> para = new Dictionary<string, object>();
                para.Add("short_code", data["short_code"]);
                DataTable dt = _data_access.GetDataTable("cusp_sms_template_select", para);
                para.Clear();
                if (dt.Rows.Count > 0)
                {
                    string msg = dt.Rows[0]["sms_text"].ToString();
                    data.AsParallel().ForAll(d => msg = msg.Replace("##" + d.Key.ToLower() + "##", d.Value));
                    send_sms(data["mobile_no"], msg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void send_sms(string mobile_no, string message)
        {
            try
            {
                //if (mobile_no.Substring(0, 2) == "91")
                //{
                //    mobile_no = mobile_no.Substring(2, mobile_no.Length + 1);
                //}
                HttpClientHandler client_handler = new HttpClientHandler();

                client_handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient http_client = new HttpClient(client_handler);

                string str = _site_config.GetConfigValue("sms_url").Replace("##mobile_no##", mobile_no).Replace("##message##", message);
                HttpResponseMessage result = http_client.GetAsync(str).Result;
                result.EnsureSuccessStatusCode();
                //if (result.IsSuccessStatusCode)
                //{
                var return_string = result.Content.ReadAsStringAsync().Result;
                //var cust = JsonConvert.DeserializeObject<result>(customerJsonString);
                //}
                //else
                //{
                //    int status_code = (int)result.StatusCode;
                //    string reason = result.ReasonPhrase;
                //}
                client_handler.Dispose();
                http_client.Dispose();
                driver_log_exception("send_otp", return_string, mobile_no, null, 1);
            }
            catch (Exception ex)
            {
                driver_log_exception("send_otp", ex.Message, mobile_no, null, 1);
            }
        }


        public static void file_copy(string file_name, string src_file_path, string dest_file_path)
        {
            string str_src_file = System.IO.Path.GetFullPath(src_file_path + file_name);
            string str_dest_file = System.IO.Path.GetFullPath(dest_file_path + "\\" + file_name);
            File.Copy(str_src_file, str_dest_file, true);
        }

        public static void delete_file(string file_name, string src_file_path)
        {
            string str_file = System.IO.Path.GetFullPath(src_file_path + file_name);
            File.Delete(str_file);
        }


    }


}
