using System;
using System.Collections.Generic;
using System.Data;

/// <summary>Data access layer for the Department entity.</summary>
public class DepartmentRepository : IRepository<Department>
{
    /// <summary>Retrieves a department by its primary key.</summary>
    public Department GetById(int id)
    {
        return DataHelper.ExecuteReader("SELECT * FROM department WHERE dep_id = ?id",
            r => Map(r), Mapped.Parameter("?id", id))[0];
    }

    /// <summary>Returns all active (non-deleted) departments.</summary>
    public List<Department> GetAll()
    {
        return DataHelper.ExecuteReader("SELECT * FROM department WHERE deleted_at IS NULL", r => Map(r));
    }

    /// <summary>Inserts a new department.</summary>
    public int Insert(Department entity)
    {
        string now = Function.Now();
        return DataHelper.ExecuteNonQuery(
            "INSERT INTO department (dep_sector, created_at, updated_at) VALUES (?sector, ?now, ?now)",
            Mapped.Parameter("?sector", entity.DepSector),
            Mapped.Parameter("?now", now));
    }

    /// <summary>Updates an existing department.</summary>
    public bool Update(Department entity)
    {
        return DataHelper.ExecuteNonQuery(
            "UPDATE department SET dep_sector = ?sector, updated_at=?now WHERE dep_id = ?id",
            Mapped.Parameter("?sector", entity.DepSector),
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", entity.DepartmentId)) > 0;
    }

    /// <summary>Soft-deletes a department by its primary key.</summary>
    public bool Delete(int id)
    {
        return DataHelper.ExecuteNonQuery("UPDATE department SET deleted_at=?now WHERE dep_id = ?id",
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", id)) > 0;
    }

    /// <summary>Maps the current IDataReader row to a Department object.</summary>
    private static Department Map(IDataReader r)
    {
        return new Department
        {
            DepartmentId = Convert.ToInt32(r["dep_id"]),
            DepSector = r["dep_sector"].ToString(),
            CreatedAt = r["created_at"] == DBNull.Value ? null : r["created_at"].ToString(),
            UpdatedAt = r["updated_at"] == DBNull.Value ? null : r["updated_at"].ToString(),
            DeletedAt = r["deleted_at"] == DBNull.Value ? null : r["deleted_at"].ToString()
        };
    }
}
