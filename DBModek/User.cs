using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CourceProject.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public string UserOrSuperUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
