using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class HotelDetailResponse
    {
        public HotelDetailResponse()
        { }
        public HotelDetailRequest Request { get; set; }
        public HotelDetailStatus ResponseCode { get; set; }
        public HotelList ResponseData { get; set; }
        public string ResponseMessage { get; set; }
        public string tblHotelDetailTraceID { get; set; }
        public string HDRID { get; set; }
    }

