using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;
using TonThatCamTuanAPI.Common;
using TonThatCamTuanAPI.Models.Entity;
using TonThatCamTuanAPI.Models.Order;
using TonThatCamTuanAPI.Models.Product;

namespace TonThatCamTuanAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly TonThatCamTuanDeAnContext _context;
        public OrderController(TonThatCamTuanDeAnContext context)
        {
            this._context = context;
        }
        [HttpPost("themvaogiohang")]
        public IActionResult ThemVaoGioHang([FromForm] InputOrder input)
        {

            
            if (ModelState.IsValid)
            {
                Order order = new Order();
                order.Id = input.Id;
                order.OrderId = input.OrderId;
                order.ProductId = input.ProductId;
                order.ProductQuantity = int.Parse(input.ProductQuantity);
                order.UserName = input.UserName;
                order.Sodienthoai = input.Sodienthoai;
                order.Diachi = input.Diachi;
                order.Ngaytao = input.Ngaytao;
                _context.Orders.Add(order);
                _context.SaveChanges();
                return Ok(order);
            }
            return BadRequest();

        }
        
        [HttpGet("laydanhsachdonhang")]
        public IActionResult DanhSachDonHang() //nay moi them khuc tim kiem 
        {

            var items = _context.Orders.ToList();
            return Ok(items);

        }
        [HttpGet("laydanhsachdonhang2")]
        public List<Order> DanhSachDonHang2() // Kieu nay thi dinh dang cho no mot kieu nhat dinh la mot cai list
        {
            var items = _context.Orders.ToList();
            return items;
        }
    }
}
