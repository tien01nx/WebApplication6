using System;
using System.Collections.Generic;

namespace WebApplication6.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Role1 { get; set; } = null!;

    public string? Createby { get; set; }

    public DateTime? Createat { get; set; }
}
