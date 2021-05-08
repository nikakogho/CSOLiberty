using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSOLiberty.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key, Column("order_id"), Required]
        public int ID { get; set; }
        
        [Column("order_client_id")]
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }

        [Column("order_seller_id")]
        public int SellerID { get; set; }
        public virtual Seller Seller { get; set; }

        [Column("order_parent_id")]
        public int? ParentID { get; set; }
        public virtual Order Parent { get; set; }

        public virtual ICollection<Order> Children { get; set; } = new List<Order>();

        [Column("order_amount"), DataType(DataType.Currency), Range(0.01, double.MaxValue), Required]
        public double Amount { get; set; }
        
        [Column("order_date", TypeName = "date"), Required]
        public DateTime Date { get; set; }

        public bool IsMain => ParentID == null;
        public bool HasKids => Children.Count > 0;
        public DateTime LastChildDate => HasKids ? Children.Max(o => o.Date) : Date;
        public DateTime LastDate => LastChildDate > Date ? LastChildDate : Date;

        public double TotalAmount => Amount + (HasKids ? Children.Sum(o => o.Amount) : 0);
    }
}
