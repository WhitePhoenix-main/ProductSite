using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsSite.Pages.Wishlist.Data
{
    [Table("Wishlist")]
    public class WishlistRecord
    {
        [Key]
        [Column(TypeName = "nvarchar(150)")]
        public string UserId { get; set; }

        [Key]
        [Column(TypeName = "nvarchar(150)")]
        public string ProductId { get; set; }
    }
}