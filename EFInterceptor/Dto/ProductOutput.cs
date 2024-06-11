
namespace EFInterceptor.Dto
{
    /// <summary>
    /// Product Output
    /// </summary>
    public class ProductOutput : BaseOutput
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
        /// Category
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// CategoryId
        /// </summary>
        public int CategoryId { get; set; }


    }
}
