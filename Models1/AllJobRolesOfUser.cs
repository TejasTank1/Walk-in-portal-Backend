using System;
using System.Collections.Generic;

namespace Backend.Models1;

public partial class AllJobRolesOfUser
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UserReg>? Ids { get; set; } = new List<UserReg>();
}
