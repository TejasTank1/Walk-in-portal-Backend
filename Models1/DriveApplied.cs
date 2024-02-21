using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class DriveApplied
{
    public string? Resume { get; set; }

    public int Id { get; set; }

    public int SlotsId { get; set; }

    public int DriveId { get; set; }

    public DateOnly? DtCreated { get; set; }

    public DateOnly? DtUpdated { get; set; }

    public virtual DriveAvailableTime? DriveAvailableTime { get; set; } = null!;

    public virtual UserReg? IdNavigation { get; set; } = null!;

    public virtual ICollection<JobRole>? Roles { get; set; } = null;
}
