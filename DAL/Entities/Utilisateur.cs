using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Utilisateur
{
    public int IdUser { get; set; }

    public string Email { get; set; }

    public string Motdepasse { get; set; }

    public int? IdRole { get; set; }

    public string Matricule { get; set; }

    public virtual Role IdRoleNavigation { get; set; }
}
