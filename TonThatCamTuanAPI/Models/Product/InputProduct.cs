using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace TonThatCamTuanAPI.Models.Product
{
    public class InputProduct
    {

        
        public string? ProductName { get; set; }
        [Column(TypeName = "money")]
        public string? Price { get; set; }
        //public IFormFile image { get; set; }
       
        public string? Detail { get; set; }
        

        public string? Material { get; set; }
        
        public string? Size { get; set; } 
        public IFormFileCollection? Images { get; set; }
    }
}
