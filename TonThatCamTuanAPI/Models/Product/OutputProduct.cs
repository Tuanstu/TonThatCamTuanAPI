using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TonThatCamTuanAPI.Models.Product
{
    public class OutputProduct
    {
        public string Id { get; set; } = null!;
        public string? ProductName { get; set; }
        [Column(TypeName = "money")]
        public long? Price { get; set; }
        [StringLength(1000)]
        public string? UrlImages { get; set; }
        [StringLength(150)]
        public string? Detail { get; set; }

        public string? Material { get; set; }
        public string? Size { get; set; }
        
    }
}
