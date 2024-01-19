using Microsoft.Identity.Client;

namespace TonThatCamTuanAPI.Models.Product
{
    public class OutputImage
    {
        public string? UrlImage { get; set; }
        public int? Position { get; set; } = 1;

    //    OutputImage() { }
    //    private static readonly object lockobj = new object();  
    //private static OutputImage instance = null;
    //    public static OutputImage Instance
    //    {
    //        get
    //        {
    //            lock (lockobj)
    //                {
    //                    if (instance == null)
    //                    {
    //                        instance = new OutputImage();
    //                    }
    //                    return instance;
    //                }
    //        }

    //    }
    }
}
