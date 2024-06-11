namespace EFInterceptor.Dto
{
    /// <summary>
    /// Base Output
    /// </summary>
    public abstract class BaseOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// LastUpdate
        /// </summary>
        public DateTime? LastUpdate { get; set; }

        /// <summary>
        /// CreationDate
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Delete Date
        /// </summary>
        public DateTime? DeleteDate { get; internal set; }
    }
}
