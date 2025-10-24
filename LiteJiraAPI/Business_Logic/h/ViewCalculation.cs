public class ViewCalculation
{
    public string HotelName { get; set; }
    public decimal OtherCharges { get; set; }
    public bool IsOffer { get; set; }
    public string OfferStr { get; set; }
    public List<DateWiseRoomRate> Rates { get; set; }
    public string Currency { get; set; }
    public decimal ROE { get; set; }
    public decimal DiscountRate { get; set; }

}
public class DateWiseRoomRate
{
    public string Date { get; set; }
    public decimal Rate { get; set; }
}



