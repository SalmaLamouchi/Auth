using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class ListRole
{
    public int IdRole { get; set; }

    public int IdMenu { get; set; }

    public bool? Etat { get; set; }

    public virtual Menu IdMenuNavigation { get; set; }

    public virtual Role IdRoleNavigation { get; set; }
}
