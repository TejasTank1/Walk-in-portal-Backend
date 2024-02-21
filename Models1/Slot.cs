using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class Slot
{
    public int Id { get; set; }

    public string SlotName { get; set; } = null!;

    public virtual ICollection<DriveAvailableTime> DriveAvailableTimes { get; set; } = new List<DriveAvailableTime>();
}
