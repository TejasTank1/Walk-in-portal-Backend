using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class EdiucationInfo
{
    public int Percentage { get; set; }

    public int PassingYear { get; set; }

    public int Id { get; set; }

    public int Collegeid { get; set; }

    public int Streamid { get; set; }

    public int QualificationId { get; set; }

    public virtual College? College { get; set; } = null!;

    public virtual UserReg? IdNavigation { get; set; } = null!;

    public virtual Qualification? Qualification { get; set; } = null!;

    public virtual Stream? Stream { get; set; } = null!;
}
