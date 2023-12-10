using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TonThatCamTuanAPI.Models.Authentication;
using TonThatCamTuanAPI.Models.Entity;
using TonThatCamTuanDeAn.Areas.Admin.Models.TaiKhoan;

namespace TonThatCamTuanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _configuration;//Khai bao de lay may cai key roi issuer trong appsetting.json
        private readonly TonThatCamTuanDeAnContext _context;
        public AuthenticationController(IConfiguration configuration,TonThatCamTuanDeAnContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost("auth")]
        public async Task<IActionResult> XacThuc([FromForm] InputDangNhap input) //duyet token dua tren cai signature key
        {
            //de tao token
            var item = await _context.TaiKhoans.FirstOrDefaultAsync(c => c.Email == input.Email
            //&& c.UserName == input.Username
            && c.PasswordHash == input.Password);
            if (item == null) return Unauthorized();
            var token = GenerateJWT(item);
            return Ok(new OutputToken
            {
                Token = token,
                RefreshToken = null,
                InvokeToken = null,
                Times = null
            });
        }

        private string GenerateJWT(TaiKhoan taikhoan)
        {
            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])); //lay cai key trong appsetting
            var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256Signature); //chon cai thuat toan ma hoa cho cai key
            //var role = _context.Roles.FirstOrDefault(c => c.Id == taikhoan.Id);
            var claims = new List<Claim>() //Jwt gom ba thanh phan: header, claim, secret key
            {
                new Claim(ClaimTypes.Sid, taikhoan.Id), //tra ve id 
                new Claim(ClaimTypes.Name, taikhoan.UserName), //tra ve ten nguoi dung
                new Claim(ClaimTypes.Role, taikhoan.Role), 
            };
            //Nay khi bo vo debugger cua jwt thi se hien ra nhung thong tin nay
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"], //nguoi duyet cai token
                audience: _configuration["JWT:Issuer"], //token danh cho nguoi nao
                claims,
                notBefore: new DateTimeOffset(DateTime.Now).DateTime, //thoi gian bat dau tao
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(10)).DateTime,
                //expires: new DateTimeOffset(DateTime.Now.AddMinutes(10)).DateTime, //thoi gian het han theo phut
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token); //return ve chuoi token
        }
    }
}
