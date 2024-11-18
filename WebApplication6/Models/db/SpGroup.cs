using System;
using System.Collections.Generic;

namespace WebApplication6.Models.db;

public partial class SpGroup
{
    public int Id { get; set; }

    public string GroupName { get; set; } = null!;

    public string CreateBy { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public virtual ICollection<SpGroupPer> SpGroupPers { get; set; } = new List<SpGroupPer>();

    public virtual ICollection<SpGroupUser> SpGroupUsers { get; set; } = new List<SpGroupUser>();
}
