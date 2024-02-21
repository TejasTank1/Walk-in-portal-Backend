using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class WalkInDrife
{
    public int DriveId { get; set; }

    public string DriveName { get; set; } = null!;

    public string DriveStartDate { get; set; } = null!;

    public string DriveEndDate { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? ExtraInfo { get; set; }

    public int? Preid { get; set; }

    public DateOnly? DtCreated { get; set; }

    public DateOnly? DtUpdated { get; set; }

    public virtual ICollection<DriveAvailableTime> DriveAvailableTimes { get; set; } = new List<DriveAvailableTime>();

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();

    public virtual ICollection<JobRole> Roles { get; set; } = new List<JobRole>();
}
