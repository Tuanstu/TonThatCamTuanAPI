using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TonThatCamTuanAPI.Models.Entity;

[Table("Admin")]
public partial class Admin
{
    [Key]
    [StringLength(100)]
    public string Id { get; set; } = null!;

    [StringLength(10)]
    public string? AdminName { get; set; }
}
