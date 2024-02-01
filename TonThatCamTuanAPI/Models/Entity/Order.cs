using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TonThatCamTuanAPI.Models.Entity;

[Table("Order")]
public partial class Order
{
    [Key]
    public string Id { get; set; } = null!;

    [StringLength(450)]
    public string? ProductId { get; set; }

    public int? ProductQuantity { get; set; }

    [StringLength(150)]
    public string? UserName { get; set; }

    [StringLength(10)]
    public string? Sodienthoai { get; set; }

    [StringLength(150)]
    public string? Diachi { get; set; }

    [StringLength(100)]
    public string? Ngaytao { get; set; }

    [StringLength(450)]
    public string? OrderId { get; set; }
}
