using System.Collections.Generic;

namespace RESTfulHotel.Models
{
    public class RootObject
        {
            public Hotel hotel { get; set; }
            public List<HotelRate> hotelRates { get; set; }
        }
    
        public class Hotel
        {
            public int classification { get; set; }
            public int hotelID { get; set; }
            public string name { get; set; }
            public double reviewscore { get; set; }
        }
    
        public class HotelRate
        {
            public int adults { get; set; }
            public int los { get; set; }
            public Price price { get; set; }
            public string rateDescription { get; set; }
            public string rateID { get; set; }
            public string rateName { get; set; }
            public List<RateTag> rateTags { get; set; }
            public string targetDay { get; set; }
        }
    
        public class Price
        {
            public string currency { get; set; }
            public double numericFloat { get; set; }
            public int numericInteger { get; set; }
        }
    
        public class RateTag
        {
            public string name { get; set; }
            public bool shape { get; set; }
        }
}