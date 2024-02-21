using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models1;

public partial class ProfessionalQualificationInfo
{
    public int YearsOfExperience { get; set; }

    public int? CurrentCtc { get; set; }

    public int? ExpectedCtc { get; set; }

    public sbyte? NoticePeriod { get; set; }

    public string? NoticePeriodDuration { get; set; }

    public sbyte? AppliedPrevious12Months { get; set; }

    public string? RoleForApplied { get; set; }

    public string ApplicantType { get; set; } = null!;

    [Key]
    public int Id { get; set; }

    public virtual UserReg? IdNavigation { get; set; } = null;

    public virtual ICollection<TechnologyTable>? Teches { get; set; } = new List<TechnologyTable>();

    public virtual ICollection<TechnologyTable>? TechesNavigation { get; set; } = new List<TechnologyTable>();
}
