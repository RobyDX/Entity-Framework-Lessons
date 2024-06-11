using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EFInterceptor.Data
{
    /// <summary>
    /// Category
    /// </summary>
    [Table("CATEGORY")]
    public class Category : BaseItem
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
    }
}
