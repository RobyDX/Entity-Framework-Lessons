using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace EFLesson.Data
{
    /// <summary>
    /// Exam
    /// </summary>
    [Table("Exam")]
    [PrimaryKey(nameof(StudentId), nameof(CourseId))]
    public class Exam
    {
        /// <summary>
        /// Student Id
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Course Id
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Exam Date
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Vote
        /// </summary>
        public int? Vote { get; set; }

        /// <summary>
        /// Student
        /// </summary>
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = null!;

        /// <summary>
        /// Course
        /// </summary>
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = null!;
    }
}
