using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class UserOrders
    {
        public int UserOrdersId { get; set; }


        public int OrderId { get; set; }

        public Order? Order { get; set; }

        [ForeignKey("UserId")]
        public int Id { get; set; }

        public User? User { get; set; }

    }
}