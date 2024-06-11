namespace EFInterceptor.Data
{
    /// <summary>
    /// Base Item
    /// </summary>
    public class BaseItem
    {
        /// <summary>
        /// Creation Date
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Last Update
        /// </summary>
        public DateTime? LastUpdate { get; set; }

        /// <summary>
        /// Delete Date
        /// </summary>
        public DateTime? DeleteDate { get; set; }
    }
}
