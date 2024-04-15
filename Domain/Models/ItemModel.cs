using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class ItemModel
    {

        public int Id { get; set; }

        // we put limits on the strings because it takes up less memory (it is max by default)
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "Item Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1,000")]
        [DataType(DataType.Currency, ErrorMessage = "That's not money!")]
        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18,2)")] // This is the default for SQL Server
        [Display(Name = "How much it'll cost ya")]
        public decimal? Price { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageURL { get; set; }


        //create tracking
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        // delete tracking
        public bool isDeleted { get; set; } = false;




    }
}
