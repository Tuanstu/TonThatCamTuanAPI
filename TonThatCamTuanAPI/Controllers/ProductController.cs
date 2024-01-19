using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text.Json;
using TonThatCamTuanAPI.Common;
using TonThatCamTuanAPI.Models.Entity;
using TonThatCamTuanAPI.Models.Product;

namespace TonThatCamTuanAPI.Controllers
{
    [Route("api/[controller]")] //muon them gi phai sau chu api cung dc
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ProductController : ControllerBase
    {
        private readonly TonThatCamTuanDeAnContext _context;
        public ProductController(TonThatCamTuanDeAnContext context)
        {
            this._context = context;
        }

        [AllowAnonymous] //Moi nguoi co the xai
        [HttpGet("danh-sach-san-pham")]
        public IActionResult DanhSachSanPham(string? timkiem) //nay moi them khuc tim kiem 
        {
            //var items = _context.Products.ToList();
            //return Ok(items);

            var items = _context.Products.Where(c => c.Filter.Contains((timkiem ?? "").ToLower())).Select(c => new OutputProduct() //Select de chon ra cai specific muon tra ve trong TH nay thi khong tra cai filter
            {
                Id = c.Id,
                ProductName = c.ProductName,
                Price = c.Price,
                UrlImages = c.Image,
                Detail = c.Detail,
                Material = c.Material,
                Size = c.Size,

            }).ToList();
            return Ok(items);

        }

        //Cai nay get product ma chua co tim kiem
        //[HttpGet("danh-sach-san-pham")]
        //public IActionResult DanhSachSanPham() //Kieu nay co the sai nhung ham dung sai cua chuong trinh nhu la Ok(), BadRequest(),...
        //{
        //    //var items = _context.Products.ToList();
        //    //return Ok(items);
        //    var items = _context.Products.Select(c => new OutputProduct() //Select de chon ra cai specific muon tra ve trong TH nay thi khong tra cai filter
        //    {
        //        Id = c.Id,
        //        ProductName = c.ProductName,
        //        Price = c.Price,
        //        UrlImages = c.Image,
        //        Detail = c.Detail,
        //        Material = c.Material,
        //        Size = c.Size,

        //    }).ToList();
        //    return Ok(items);
        //}
        [HttpGet("danh-sach-san-pham-2")]
        public List<Product> DanhSachSanPham2() // Kieu nay thi dinh dang cho no mot kieu nhat dinh la mot cai list
        {
            var items = _context.Products.ToList();
            return items;
        }
        //Api get 1 data

        [AllowAnonymous]
        [HttpGet("data-product/{id}")]
        public IActionResult ItemProduct(string id)
        {


            var items = _context.Products.Select(c => new OutputProduct() //Select de chon ra cai specific muon tra ve trong TH nay thi khong tra cai filter
            {
                Id = c.Id,
                ProductName = c.ProductName,
                Price = c.Price,
                UrlImages = c.Image,
                Detail = c.Detail,
                Material = c.Material,
                Size = c.Size,

            }).ToList().FirstOrDefault(c => c.Id == id);
            return Ok(items);
        }
        [HttpDelete("xoa-product/{id}")]
        public IActionResult XoaProduct(string id)
        {
            var item = _context.Products.FirstOrDefault(x => x.Id == id);

            var temp = JsonSerializer.Deserialize<List<OutputImage>>(item.Image);
            foreach (var img in temp)
            {
                UploadFiles.RemoveImage(img.UrlImage);
            }

            _context.Products.Remove(item);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("tao-product")]
        public IActionResult Them([FromForm] InputProduct input) //FromForm la nhap du lieu dang form nhung neu muon nhap bang postman thi phai copy cai json roi vo postman nhap vao form-data roi bulk edit
        {
            Product product = new Product();
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid().ToString();
                product.ProductName = input.ProductName;
                product.Detail = input.Detail;
                product.Price = int.Parse(input.Price);
                product.Material = input.Material;
                product.Size = input.Size;
                List<OutputImage> listImages = new List<OutputImage>();
                int i = 0;
                foreach (var img in input.Images)
                {
                    i++;
                    OutputImage outputImage = new OutputImage();
                    outputImage.UrlImage = UploadFiles.SaveImage(img);
                    outputImage.Position = i;
                    listImages.Add(outputImage);
                }
                product.Image = JsonSerializer.Serialize(listImages); //tai nhap nhieu hinh nen phai chuyen ve json roi se dung deserialize cho no bien ve thanh dang list ma co nhieu images
                //var temp = JsonSerializer.Deserialize<OutputImage>(product.Image);
                product.Filter = input.Detail + " " + input.ProductName.ToLower() + " " + Common.Utility.ConvertToUnsign(input.ProductName.ToLower()) + " " + input.Price;
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok(product);
            }
            return BadRequest();
        }

        [HttpPut("cap-nhat-product/{id}")]
        public IActionResult CapNhat(string id, [FromForm] InputProduct input)
        {

            var item = _context.Products.FirstOrDefault(c => c.Id == id.ToString());
            if (item != null)
            {
                item.ProductName = input.ProductName;
                item.Detail = input.Detail;
                item.Price = int.Parse(input.Price);
                item.Material = input.Material;
                item.Size = input.Size;
                item.Filter = input.Detail + " " + input.ProductName.ToLower() + " " + Common.Utility.ConvertToUnsign(input.ProductName.ToLower()) + " " + input.Price;
                if(input.Images == null)
                {
                    item.Image = item.Image;
                    _context.Update(item);
                    _context.SaveChanges();
                    return Ok();
                }
                var temp = JsonSerializer.Deserialize<List<OutputImage>>(item.Image);
                foreach (var img in temp)
                {
                    UploadFiles.RemoveImage(img.UrlImage);
                }

                List<OutputImage> listImages = new List<OutputImage>();
                int i = 0;
                foreach (var img in input.Images)
                {
                    i++;
                    OutputImage outputImage = new OutputImage();
                    outputImage.UrlImage = UploadFiles.SaveImage(img);
                    outputImage.Position = i;
                    listImages.Add(outputImage);
                }
                item.Image = JsonSerializer.Serialize(listImages);
                _context.Update(item);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();

        }
    }
}
