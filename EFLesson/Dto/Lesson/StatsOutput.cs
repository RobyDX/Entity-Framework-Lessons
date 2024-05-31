namespace EFLesson.Dto.Lesson
{
    /// <summary>
    /// Course Info
    /// </summary>
    public class StatsOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Average Vote
        /// </summary>
        public double? AverageVote { get; set; }

        /// <summary>
        /// Best Vote
        /// </summary>
        public int? BestVote { get; set; }

        /// <summary>
        /// Student Count
        /// </summary>
        public int StudentCount { get; set; }
    }
}
