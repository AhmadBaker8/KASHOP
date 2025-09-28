using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{

    public enum OrderStatus
    {
        Pending=1,
        Approved=3,
        Shipped=4,
        Delivered=5,
        Cancelled=2
    }
    public enum PaymentMethodEnum
    {
        Cash =1,
        Visa = 2
    }
    public class Order
    {

        //order properties
        public int Id { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }

        public decimal TotalAmount { get; set; }


        //payment properties

        public PaymentMethodEnum PaymentMethod { get; set; }

        public string? PaymentId { get; set; }

        //carrier properties

        public string? CarrierName { get; set; }

        public string? TrackingNumber { get; set; }

        //relationships

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public List<OrderItems> OrderItems { get; set; }

        }
}
