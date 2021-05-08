using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSOLiberty.Models
{
    [Table("Clients")]
    public class Client
    {
        [Key, Column("client_id"), Required]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 1), Column("client_fname"), Required]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 1), Column("client_lname"), Required]
        public string LastName { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
