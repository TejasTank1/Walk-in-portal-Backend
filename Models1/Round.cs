using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class Round
{
    public int RoundId { get; set; }

    public string? Time { get; set; }

    public string? Type { get; set; }

    public int DriveId { get; set; }

    public string? ExtraInfo { get; set; }

    public virtual WalkInDrife Drive { get; set; } = null!;
}
