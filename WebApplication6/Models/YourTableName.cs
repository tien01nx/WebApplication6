using System;
using System.Collections.Generic;

namespace WebApplication6.Models;

public partial class YourTableName
{
    public int Id { get; set; }

    public string? ManagementId { get; set; }

    public int? Status { get; set; }

    public string? Model { get; set; }

    public string? PartNo { get; set; }

    public string? PartName { get; set; }

    public string? DrawingRev { get; set; }

    public string? No { get; set; }

    public string? Content { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? IssuedBy { get; set; }

    public DateTime? IssuedDate { get; set; }

    public string? CheckedBy { get; set; }

    public DateTime? CheckedDate { get; set; }

    public string? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public byte[]? File { get; set; }
}
