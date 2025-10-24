using System;



public enum HotelSearchRoleRight
{
    ViewOnly = 0,
    All = 1

}
public enum HotelSearchStatus
{
    successful = 1,
    error = 0
}
public enum HotelDetailStatus
{
    successful = 1,
    error = 0
}
public enum QuoteResponseStatus
{
    successful = 1,
    error = 0
}
public enum BookingStatus
{
    None=-1,
    successful = 1,
    error = 0,
    BookingCancel=3,
    ErrorInCancellation=4
}

public enum BookingPaxTitle
{
    Mr = 1,
    Mrs = 2,
    Miss = 3,
    Mstr = 4
}



public static class HotelProvider
{

    public  const string TboApi = "TboApi";
    public  const string Travcoapi = "Travcoapi";
    public  const string Local = "Local";


}

public static class SearchSection
{

    public  const string FromB2b = "B2B";
    public  const string FromB2c = "B2C";
}


