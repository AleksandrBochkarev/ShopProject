using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User : IdentityUser<int>
    {

        [Display(Name = "User Name")]

        public override string UserName { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public override string Email { get; set; }
        public ICollection<UserOrders>? Orders { get; set; }

    }
}
