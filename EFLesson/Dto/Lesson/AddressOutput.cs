using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EFLesson.Dto.Lesson
{
    /// <summary>
    /// Address Output
    /// </summary>
    public class AddressOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Street
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; } = string.Empty;
    }
}
