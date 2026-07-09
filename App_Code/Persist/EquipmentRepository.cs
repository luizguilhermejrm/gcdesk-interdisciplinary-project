using System;
using System.Collections.Generic;
using System.Data;

/// <summary>Data access layer for the Equipment entity.</summary>
public class EquipmentRepository : IRepository<Equipment>
{
    /// <summary>Retrieves equipment by its primary key.</summary>
    public Equipment GetById(int id)
    {
        return DataHelper.ExecuteReader("SELECT * FROM equipment WHERE equip_id = ?id",
            r => Map(r), Mapped.Parameter("?id", id))[0];
    }

    /// <summary>Returns all active (non-deleted) equipment.</summary>
    public List<Equipment> GetAll()
    {
        return DataHelper.ExecuteReader("SELECT * FROM equipment WHERE deleted_at IS NULL", r => Map(r));
    }

    /// <summary>Inserts new equipment.</summary>
    public int Insert(Equipment entity)
    {
        string now = Function.Now();
        return DataHelper.ExecuteNonQuery(
            "INSERT INTO equipment (equip_description, equip_number, created_at, updated_at) VALUES (?desc, ?num, ?now, ?now)",
            Mapped.Parameter("?desc", entity.Description),
            Mapped.Parameter("?num", entity.EquipNumber),
            Mapped.Parameter("?now", now));
    }

    /// <summary>Updates existing equipment.</summary>
    public bool Update(Equipment entity)
    {
        return DataHelper.ExecuteNonQuery(
            "UPDATE equipment SET equip_description = ?desc, equip_number = ?num, updated_at=?now WHERE equip_id = ?id",
            Mapped.Parameter("?desc", entity.Description),
            Mapped.Parameter("?num", entity.EquipNumber),
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", entity.EquipmentId)) > 0;
    }

    /// <summary>Soft-deletes equipment by its primary key.</summary>
    public bool Delete(int id)
    {
        return DataHelper.ExecuteNonQuery("UPDATE equipment SET deleted_at=?now WHERE equip_id = ?id",
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", id)) > 0;
    }

    /// <summary>Maps the current IDataReader row to an Equipment object.</summary>
    private static Equipment Map(IDataReader r)
    {
        return new Equipment
        {
            EquipmentId = Convert.ToInt32(r["equip_id"]),
            Description = r["equip_description"].ToString(),
            EquipNumber = Convert.ToInt32(r["equip_number"]),
            CreatedAt = r["created_at"] == DBNull.Value ? null : r["created_at"].ToString(),
            UpdatedAt = r["updated_at"] == DBNull.Value ? null : r["updated_at"].ToString(),
            DeletedAt = r["deleted_at"] == DBNull.Value ? null : r["deleted_at"].ToString()
        };
    }
}
