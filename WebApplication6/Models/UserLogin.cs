using System;
using System.Collections.Generic;

namespace WebApplication6.Models;

public partial class UserLogin
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Grade { get; set; }

    public string? Shift { get; set; }

    public string? Group { get; set; }

    public string? Picture { get; set; }
}
