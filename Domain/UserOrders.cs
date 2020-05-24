

namespace Domain
{
    public class UserOrders
    {
        public int UserOrdersId { get; set; }


        public int OrderId { get; set; }

        public Order? Order { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }
    }
}
