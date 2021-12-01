using System;

namespace Domain
{
    public class Bike
    {
        public Guid Id { get; set; }
        public string CustomerName {get; set;}
        public DateTime? CheckoutTime { get; set; }
        public DateTime? CheckinTime { get; set; }
        public int TotalTimeSpent { get; set; }
        public DateTime DateModified { get; set; }
        
    }
}