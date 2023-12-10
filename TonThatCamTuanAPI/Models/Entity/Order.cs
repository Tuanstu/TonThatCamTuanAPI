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
    [StringLength(100)]
    public string Id { get; set; } = null!;

    public int? Quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal? Amount { get; set; }

    [StringLength(150)]
    public string? Item { get; set; }
}
