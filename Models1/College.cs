using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class College
{
    public int Collegeid { get; set; }

    public string Name { get; set; } = null!;

    public string CollegeLocaton { get; set; } = null!;

    public virtual ICollection<EdiucationInfo>? EdiucationInfos { get; set; } = null;
}
