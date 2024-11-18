using System;
using System.Collections.Generic;

namespace WebApplication6.Models.db;

public partial class SpPermission
{
    public int Id { get; set; }

    public string Permission { get; set; } = null!;

    public string Form { get; set; } = null!;

    public string Action { get; set; } = null!;

    public virtual ICollection<SpGroupPer> SpGroupPers { get; set; } = new List<SpGroupPer>();
}
