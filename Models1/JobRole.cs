using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class JobRole
{
    public int RoleId { get; set; }

    public string? Name { get; set; }

    public int? Package { get; set; }

    public string? Requirement { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<DriveApplied> DriveApplieds { get; set; } = new List<DriveApplied>();

    public virtual ICollection<WalkInDrife> Drives { get; set; } = new List<WalkInDrife>();
}
