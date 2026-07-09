using System.Collections.Generic;
using System.Data;

/// <summary>Generic repository interface for CRUD operations.</summary>
/// <typeparam name="T">Entity type.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>Retrieves an entity by its primary key.</summary>
    T GetById(int id);

    /// <summary>Returns all entities.</summary>
    List<T> GetAll();

    /// <summary>Inserts a new entity. Returns 0 on success.</summary>
    int Insert(T entity);

    /// <summary>Updates an existing entity.</summary>
    bool Update(T entity);

    /// <summary>Deletes an entity by its primary key.</summary>
    bool Delete(int id);
}
