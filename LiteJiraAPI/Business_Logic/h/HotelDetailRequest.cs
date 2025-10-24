using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class HotelDetailRequest
{
    public HotelDetailRequest()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string HotelstringIds { get; set; }
    public string HotelName { get; set; }
    public string CountryName { get; set; }
    public string CountryCode { get; set; }
    public string CountryCode2 { get; set; }
    public int CityId { get; set; }
    public string CityCode { get; set; }
    public string CityShortName { get; set; }
    public string CityFullName { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int TotalNoRoom { get; set; }
    public string AdultStr { get; set; }
    public string Childstr { get; set; }
    public string Child1str { get; set; }
    public string Child2str { get; set; }
    public string Child3Str { get; set; }
    public int Nights { get; set; }
    public string ChildAgestr { get; set; }
    public int UserID { get; set; }
    public int AgentId { get; set; }
    public string Section { get; set; }
    public HotelSearchRoleRight HotelSearchRoleRight { get; set; }
    public string Nationality { get; set; }
    public string CountryOfResident { get; set; }
    public string CorporateTravelType { get; set; }
    public int CompanyID { get; set; }
}

