using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models1;

public partial class Stream
{
    [Key]
    public int Streamid { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EdiucationInfo> EdiucationInfos { get; set; } = new List<EdiucationInfo>();
}
