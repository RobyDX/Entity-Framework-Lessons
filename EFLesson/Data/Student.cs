using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EFLesson.Data
{
    /// <summary>
    /// Student
    /// </summary>
    [Table("Student")]
    [Index(nameof(Code), IsUnique = true, Name = "IDX_Code")]
    public class Student : User
    {
        /// <summary>
        /// Student Code
        /// </summary>
        [Required, MaxLength(6)]
        public string Code { get; set; } = string.Empty;


        /// <summary>
        /// Courses
        /// </summary>
        public ICollection<Course> Courses { get; set; } = null!;
    }
}
