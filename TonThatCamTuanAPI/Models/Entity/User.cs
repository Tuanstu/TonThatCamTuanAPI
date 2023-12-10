using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TonThatCamTuanAPI.Models.Entity;

[Table("User")]
public partial class User
{
    [Key]
    [StringLength(100)]
    public string Id { get; set; } = null!;

    [StringLength(150)]
    public string? UserName { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(50)]
    public string? Adress { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }
}
