using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class Prerequisite
{
    public int PreId { get; set; }

    public string? Generalinstruction { get; set; }

    public string? Instruction { get; set; }

    public string? Minrequirement { get; set; }
}
