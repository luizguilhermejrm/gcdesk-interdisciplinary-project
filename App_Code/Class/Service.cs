using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>Represents a service/solution associated with a department and equipment.</summary>
public class Service
{
    /// <summary>Unique service identifier.</summary>
    public int ServiceId { get; set; }

    /// <summary>Solution description.</summary>
    public string Solution { get; set; }

    /// <summary>Foreign key to the department that offers this service.</summary>
    public int DepId { get; set; }

    /// <summary>Foreign key to the equipment related to this service.</summary>
    public int EquipId { get; set; }

    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public string DeletedAt { get; set; }
}
