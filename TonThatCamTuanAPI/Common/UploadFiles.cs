namespace TonThatCamTuanAPI.Common
{
    public class UploadFiles
    {// static la khoi tao mot lan luc tao project la khoi tao luon
        public static string wwwroot = Directory.GetCurrentDirectory() + "\\wwwroot";
        
        public static string SaveImage(IFormFile image) //image.length la do lon cua tam hinh
        {
            if (image != null && image.Length > 0)
            {
                string id = Guid.NewGuid().ToString();
                string urlPath = "";
                /*string wwwroot = Directory.GetCurrentDirectory() + "\\wwwroot";*/ //Directory.GetCurrentDirectory() vo cai cho ma cai project duoc luu; \\ = \ =>them cai \wwwroot de vo cai file wwwroot
                string filePath = Path.Combine(wwwroot, "images", id + "-" + image.FileName); //ghep lai thanh mot cai chuoi; filePath la dich den khi minh tao hinh bo no vo dau
                using (var fileStream = new FileStream(filePath, FileMode.Create)) //fileMode.Create + filepath la tao mot hinh rỗng o trong wwwroot roi may cai ben trong ham la de copy thong tin image vo cai image wwwroot
                {
                    image.CopyTo(fileStream);
                    urlPath = Path.Combine("\\images", id + "-" + image.FileName);//urlpath la duong dan lien ket tren website
                }
                return urlPath;
            }
            return "\\images\\noimage.jpg";

        }

        public static bool RemoveImage(string url)
        {
            //string wwwroot = Directory.GetCurrentDirectory() + "\\wwwroot";
            string filePath = wwwroot + url; //Path.Combine(wwwroot, url) ko ghep vo dc nen phai xai cach truyen thong
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

    }
}
