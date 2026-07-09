using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>Represents a system user (analyst or collaborator).</summary>
public class User
{
    /// <summary>Unique user identifier.</summary>
    public int UserId { get; set; }

    /// <summary>Full name of the user.</summary>
    public string Name { get; set; }

    /// <summary>Job position / role title.</summary>
    public string Position { get; set; }

    /// <summary>Account status (1 = active, 0 = inactive).</summary>
    public int StatusUser { get; set; }

    /// <summary>Analyst classification (e.g. "Colaborador").</summary>
    public string TypeAnalist { get; set; }

    /// <summary>Email address used for login and notifications.</summary>
    public string Email { get; set; }

    /// <summary>SHA-512 hashed password.</summary>
    public string Password { get; set; }

    /// <summary>Password confirmation (used during registration, not persisted).</summary>
    public string RetypePassword { get; set; }

    /// <summary>Access level (0 = analyst, 1 = collaborator).</summary>
    public int TypeAccess { get; set; }

    /// <summary>First login flag (1 = must change password on next login).</summary>
    public int FirstLogin { get; set; }

    /// <summary>Foreign key to the user's department.</summary>
    public int DepartId { get; set; }

    /// <summary>Profile image filename.</summary>
    public string Image { get; set; }

    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public string DeletedAt { get; set; }
    public string LastLogin { get; set; }
}
