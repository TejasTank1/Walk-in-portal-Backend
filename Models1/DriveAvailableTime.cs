using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class DriveAvailableTime
{
    public int SlotsId { get; set; }

    public int WalkInDrivesDriveId { get; set; }

    public virtual ICollection<DriveApplied> DriveApplieds { get; set; } = new List<DriveApplied>();

    public virtual Slot Slots { get; set; } = null!;

    public virtual WalkInDrife WalkInDrivesDrive { get; set; } = null!;
}
