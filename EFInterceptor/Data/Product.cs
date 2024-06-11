using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFInterceptor.Data
{
    /// <summary>
    /// Product
    /// </summary>
    [Table("PRODUCT")]
    public class Product : BaseItem
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required, MaxLength(256)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Price
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;


    }
}
