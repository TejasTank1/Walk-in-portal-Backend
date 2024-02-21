using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class PersonalInfo
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string Resume { get; set; } = null!;

    public string DisplayPicture { get; set; } = null!;

    public string PortfolioUrl { get; set; } = null!;

    public string RefferedEmployeeName { get; set; } = null!;

    public sbyte SendJobUpdate { get; set; }

    public int Id { get; set; }

    public virtual UserReg? IdNavigation { get; set; } = null!;
}
