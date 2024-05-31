
namespace EFLesson.Dto.Lesson
{
    /// <summary>
    /// User Input
    /// </summary>
    public class UserInput
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Surname
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Birth Date
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}
