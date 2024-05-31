using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFLesson.Data
{
    /// <summary>
    /// User
    /// </summary>
    [Table("User")]
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Surname
        /// </summary>
        [Required, MaxLength(50)]
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Birth date
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Addresses
        /// </summary>
        public ICollection<Address> Addresses { get; set; } = null!;
    }
}
