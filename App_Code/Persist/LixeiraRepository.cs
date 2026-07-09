using System;
using System.Data;

public static class LixeiraRepository
{
    public static DataSet GetDeletedTickets()
    {
        return DataHelper.FillDataSet(
            "SELECT tic_id, tic_description, tic_type, deleted_at FROM ticket WHERE deleted_at IS NOT NULL ORDER BY deleted_at DESC");
    }

    public static DataSet GetDeletedUsers()
    {
        return DataHelper.FillDataSet(
            "SELECT user_id, user_name, user_email, user_typeAnalyst, deleted_at FROM user WHERE deleted_at IS NOT NULL ORDER BY deleted_at DESC");
    }

    public static DataSet GetDeletedDepartments()
    {
        return DataHelper.FillDataSet(
            "SELECT dep_id, dep_sector, deleted_at FROM department WHERE deleted_at IS NOT NULL ORDER BY deleted_at DESC");
    }

    public static DataSet GetDeletedEquipment()
    {
        return DataHelper.FillDataSet(
            "SELECT equip_id, equip_description, equip_number, deleted_at FROM equipment WHERE deleted_at IS NOT NULL ORDER BY deleted_at DESC");
    }

    public static DataSet GetDeletedServices()
    {
        return DataHelper.FillDataSet(
            "SELECT service_id, service_solution, deleted_at FROM service WHERE deleted_at IS NOT NULL ORDER BY deleted_at DESC");
    }

    public static DataSet GetDeletedNotifications()
    {
        return DataHelper.FillDataSet(
            "SELECT not_id, not_title, not_description, deleted_at FROM notification WHERE deleted_at IS NOT NULL ORDER BY deleted_at DESC");
    }

    public static int Restore(string table, string pkName, int id)
    {
        return DataHelper.ExecuteNonQuery(
            $"UPDATE {table} SET deleted_at = NULL WHERE {pkName} = ?id",
            Mapped.Parameter("?id", id));
    }
}
