using System;

namespace Domain
{
    public class Bike
    {
        private string _customerName;

        public Guid Id { get; set; }
        public string CustomerName { get {
           return string.IsNullOrEmpty(_customerName) ?  "Not Rented" : _customerName; 
        } set => _customerName = value; }
        public DateTime? CheckoutTime { get; set; }
        public DateTime? CheckinTime { get; set; }
        public int TotalTimeSpent { get; set; }
        public DateTime DateModified { get; set; }

    }
}