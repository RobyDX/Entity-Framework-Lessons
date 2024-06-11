
namespace EFInterceptor.Dto
{
    /// <summary>
    /// Log Output
    /// </summary>
    public class LogOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Input
        /// </summary>
        public string Input { get; set; } = string.Empty;
        
        /// <summary>
        /// Detail
        /// </summary>
        public string? Detail { get;  set; }
    }
}
