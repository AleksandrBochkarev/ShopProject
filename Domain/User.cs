
using System.Collections.Generic;

namespace Domain
{
    public  class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public ICollection<UserOrders>? Orders { get; set; }
    }
}
