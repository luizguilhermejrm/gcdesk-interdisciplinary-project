using System;
using System.Collections.Generic;
using System.Data;

/// <summary>Data access layer for the Service entity.</summary>
public class ServiceRepository : IRepository<Service>
{
    /// <summary>Retrieves a service by its primary key.</summary>
    public Service GetById(int id)
    {
        return DataHelper.ExecuteReader("SELECT * FROM service WHERE service_id = ?id",
            r => Map(r), Mapped.Parameter("?id", id))[0];
    }

    /// <summary>Returns all active (non-deleted) services.</summary>
    public List<Service> GetAll()
    {
        return DataHelper.ExecuteReader("SELECT * FROM service WHERE deleted_at IS NULL", r => Map(r));
    }

    /// <summary>Inserts a new service.</summary>
    public int Insert(Service entity)
    {
        string now = Function.Now();
        return DataHelper.ExecuteNonQuery(
            "INSERT INTO service (service_solution, dep_id, equip_id, created_at, updated_at) VALUES (?sol, ?dep, ?equip, ?now, ?now)",
            Mapped.Parameter("?sol", entity.Solution),
            Mapped.Parameter("?dep", entity.DepId),
            Mapped.Parameter("?equip", entity.EquipId),
            Mapped.Parameter("?now", now));
    }

    /// <summary>Updates an existing service.</summary>
    public bool Update(Service entity)
    {
        return DataHelper.ExecuteNonQuery(
            "UPDATE service SET service_solution = ?sol, dep_id = ?dep, equip_id = ?equip, updated_at=?now WHERE service_id = ?id",
            Mapped.Parameter("?sol", entity.Solution),
            Mapped.Parameter("?dep", entity.DepId),
            Mapped.Parameter("?equip", entity.EquipId),
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", entity.ServiceId)) > 0;
    }

    /// <summary>Soft-deletes a service by its primary key.</summary>
    public bool Delete(int id)
    {
        return DataHelper.ExecuteNonQuery("UPDATE service SET deleted_at=?now WHERE service_id = ?id",
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", id)) > 0;
    }

    /// <summary>Maps the current IDataReader row to a Service object.</summary>
    private static Service Map(IDataReader r)
    {
        return new Service
        {
            ServiceId = Convert.ToInt32(r["service_id"]),
            Solution = r["service_solution"].ToString(),
            DepId = Convert.ToInt32(r["dep_id"]),
            EquipId = Convert.ToInt32(r["equip_id"]),
            CreatedAt = r["created_at"] == DBNull.Value ? null : r["created_at"].ToString(),
            UpdatedAt = r["updated_at"] == DBNull.Value ? null : r["updated_at"].ToString(),
            DeletedAt = r["deleted_at"] == DBNull.Value ? null : r["deleted_at"].ToString()
        };
    }
}
