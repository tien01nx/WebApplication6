using System;
using System.Collections.Generic;

namespace WebApplication6.Models.db;

public partial class SpGroupPer
{
    public int Id { get; set; }

    public string GroupName { get; set; } = null!;

    public string Permission { get; set; } = null!;

    public string Form { get; set; } = null!;

    public string Action { get; set; } = null!;

    public virtual SpGroup GroupNameNavigation { get; set; } = null!;

    public virtual SpPermission PermissionNavigation { get; set; } = null!;
}
