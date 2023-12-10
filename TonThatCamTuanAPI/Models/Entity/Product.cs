using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TonThatCamTuanAPI.Models.Entity;

[Table("Product")]
public partial class Product
{
    [Key]
    public string Id { get; set; } = null!;

    [StringLength(150)]
    public string? ProductName { get; set; }

    [Column(TypeName = "money")]
    public long? Price { get; set; }

    [StringLength(1000)]
    public string? Image { get; set; }

    [StringLength(150)]
    public string? Detail { get; set; }

    public string? Filter { get; set; }

    public string? Material { get; set; }

    public string? Size { get; set; }
}
