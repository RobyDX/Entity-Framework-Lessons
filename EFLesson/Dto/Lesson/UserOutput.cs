
namespace EFLesson.Dto.Lesson
{
    /// <summary>
    /// User Output
    /// </summary>
    public class UserOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

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
    }
}
