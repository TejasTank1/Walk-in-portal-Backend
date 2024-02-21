using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class Qualification
{
    public int QualificationId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EdiucationInfo> EdiucationInfos { get; set; } = new List<EdiucationInfo>();
}
