using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.CartAPI.Models
{
    public class CartDetails
    {
        [Key]
        public int CartDetailId { get; set; }

        public int CartHeaderId { get; set; }

        [ForeignKey(nameof(CartHeaderId))]
        public virtual CartHeader CartHeader { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        public int Count { get; set; }
    }
}
