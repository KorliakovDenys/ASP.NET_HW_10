using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASP.NET_HW_9.Core;

namespace ASP.NET_HW_9.Models {
    public class User {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        [InverseProperty("User")]
        public ICollection<CartPosition>? CartPositions { get; set; }
    }
}