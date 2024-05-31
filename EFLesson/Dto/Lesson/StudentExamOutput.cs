namespace EFLesson.Dto.Lesson
{
    /// <summary>
    /// Student Output
    /// </summary>
    public class StudentExamOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique Code
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Surname
        /// </summary>
        public string Surname { get; set; } = String.Empty;

        /// <summary>
        /// Birthdate
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Street
        /// </summary>
        public int? Vote { get; set; }

        /// <summary>
        /// Exam Date
        /// </summary>
        public DateTime? ExamDate { get; set; }

        /// <summary>
        /// Passed
        /// </summary>
        public bool Passed { get; set; }
    }
}
