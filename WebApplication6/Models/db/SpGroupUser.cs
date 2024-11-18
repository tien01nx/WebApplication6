using System;
using System.Collections.Generic;

namespace WebApplication6.Models.db;

public partial class SpGroupUser
{
    public int Id { get; set; }

    public string GroupName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual SpGroup GroupNameNavigation { get; set; } = null!;
}
