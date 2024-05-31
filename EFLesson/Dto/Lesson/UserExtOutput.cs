namespace EFLesson.Dto.Lesson
{
    /// <summary>
    /// User Extension
    /// </summary>
    public class UserExtOutput : UserOutput
    {
        /// <summary>
        /// Addresses
        /// </summary>
        public List<AddressOutput> Addresses { get; set; } = [];
    }
}
