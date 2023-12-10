using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TonThatCamTuanAPI.Models.Entity;

[Table("TaiKhoan")]
public partial class TaiKhoan : IdentityUser
{
    public string Role { get; set; }
}
