namespace EFInterceptor.Dto
{
    /// <summary>
    /// Product Input
    /// </summary>
    public class ProductInput
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Price
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Category Id
        /// </summary>
        public int CategoryId { get; set; }
    }
}
