using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EFLesson.Data
{
    /// <summary>
    /// Course
    /// </summary>
    [Table("Course")]
    public class Course
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        [Required, MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Students
        /// </summary>
        public ICollection<Student> Students { get; set; } = null!;
    }
}
