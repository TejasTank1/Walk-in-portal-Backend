using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class UserReg
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<DriveApplied>? DriveApplieds { get; set; } = new List<DriveApplied>();

    public virtual ICollection<EdiucationInfo>? EdiucationInfos { get; set; } = new List<EdiucationInfo>();

    public virtual PersonalInfo? PersonalInfo { get; set; }

    public virtual ProfessionalQualificationInfo? ProfessionalQualificationInfo { get; set; }

    public virtual ICollection<AllJobRolesOfUser>? Roles { get; set; } = new List<AllJobRolesOfUser>();
}
