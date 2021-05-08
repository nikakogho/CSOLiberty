using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSOLiberty.Models
{
    [Table("Sellers")]
    public class Seller
    {
        [Key, Column("seller_id"), Required]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 1), Column("seller_fname"), Required]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 1), Column("seller_lname"), Required]
        public string LastName { get; set; }

        [Column("seller_boss_id")]
        public int? BossID { get; set; }
        public virtual Seller Boss { get; set; }

        public virtual ICollection<Seller> Employees { get; set; } = new List<Seller>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public bool IsOwnBoss => BossID == ID;
    }
}
