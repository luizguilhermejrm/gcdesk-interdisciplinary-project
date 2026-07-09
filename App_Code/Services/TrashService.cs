using System;
using System.Data;

public class TrashService
{
    public DataSet GetDeletedTickets() => LixeiraRepository.GetDeletedTickets();
    public DataSet GetDeletedUsers() => LixeiraRepository.GetDeletedUsers();
    public DataSet GetDeletedDepartments() => LixeiraRepository.GetDeletedDepartments();
    public DataSet GetDeletedEquipment() => LixeiraRepository.GetDeletedEquipment();
    public DataSet GetDeletedServices() => LixeiraRepository.GetDeletedServices();
    public DataSet GetDeletedNotifications() => LixeiraRepository.GetDeletedNotifications();

    public int Restore(string table, string pkName, int id)
    {
        return LixeiraRepository.Restore(table, pkName, id);
    }
}
