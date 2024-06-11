using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFInterceptor.Data
{
    /// <summary>
    /// Log
    /// </summary>
    [Table("LOGEVENT")]
    public class LogEvent
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Input
        /// </summary>
        [Required]
        public string Input { get; set; } = string.Empty;

        /// <summary>
        /// Event Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Author of event
        /// </summary>
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Detail
        /// </summary>
        public string? Detail { get; set; }
    }
}
