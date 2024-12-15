using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Role
{
    public int IdRole { get; set; }

    public int IdProfil { get; set; }

    public string TypeRole { get; set; }

    public virtual Profil IdProfilNavigation { get; set; }

    public virtual ICollection<ListRole> ListRoles { get; set; } = new List<ListRole>();

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
