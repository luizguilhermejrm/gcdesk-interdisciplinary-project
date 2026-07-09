using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>Represents a company department.</summary>
public class Department
{
    /// <summary>Unique department identifier.</summary>
    public int DepartmentId { get; set; }

    /// <summary>Department name / sector name.</summary>
    public string DepSector { get; set; }

    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public string DeletedAt { get; set; }
}
