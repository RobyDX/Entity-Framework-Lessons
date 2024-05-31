using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFLesson.Data
{
    /// <summary>
    /// Address
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Street
        /// </summary>
        [Required, MaxLength(100)]
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// City
        /// </summary>
        [Required, MaxLength(100)]
        public string City { get; set; } = string.Empty;


        /// <summary>
        /// Users
        /// </summary>
        public ICollection<User> Users { get; set; } = null!;
    }
}
