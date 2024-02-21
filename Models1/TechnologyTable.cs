using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class TechnologyTable
{
    public int TechId { get; set; }

    public string TechName { get; set; } = null!;

    public virtual ICollection<ProfessionalQualificationInfo>? Ids { get; set; } = new List<ProfessionalQualificationInfo>();

    public virtual ICollection<ProfessionalQualificationInfo>? IdsNavigation { get; set; } = new List<ProfessionalQualificationInfo>();
}
