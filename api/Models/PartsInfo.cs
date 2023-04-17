using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartsInfo.Models;

public partial class PartsInfo
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? UpdateBy { get; set; }

    public bool? IsDelete { get; set; }

    public int? ParentId { get; set; }

    public string? ParentCode { get; set; }

    public virtual ICollection<PartsInfo> InverseParent { get; } = new List<PartsInfo>();

    public virtual PartsInfo? Parent { get; set; }
}
