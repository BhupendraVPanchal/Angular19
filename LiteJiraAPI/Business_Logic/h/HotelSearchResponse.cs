using System;
using System.Collections.Generic;


public class HotelSearchResponse
{
    public HotelSearchResponse()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public HotelSearchStatus ResponseCode { get; set; }
    public List<HotelList> ResponseData { get; set; }
    public string ContryCode { get; set; }
    public string ContryCode2 { get; set; }
    public string ResponseMessage { get; set; }
    public string tblSearchTraceID { get; set; }
    public HotelSearchRequest Resquest { get; set; }
    
}

