using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>Represents a piece of equipment that can be associated with a service.</summary>
public class Equipment
{
    /// <summary>Unique equipment identifier.</summary>
    public int EquipmentId { get; set; }

    /// <summary>Equipment description / name.</summary>
    public string Description { get; set; }

    /// <summary>Equipment inventory or asset number.</summary>
    public int EquipNumber { get; set; }

    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public string DeletedAt { get; set; }
}
