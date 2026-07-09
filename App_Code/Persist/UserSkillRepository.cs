using System;
using System.Collections.Generic;
using System.Data;

public class UserSkillRepository : IRepository<UserSkill>
{
    private static UserSkill Map(IDataReader r)
    {
        return new UserSkill
        {
            SkillId = Convert.ToInt32(r["skill_id"]),
            UserId = Convert.ToInt32(r["user_id"]),
            SkillName = r["skill_name"].ToString(),
            CreatedAt = r["created_at"] == DBNull.Value ? null : r["created_at"].ToString(),
            UpdatedAt = r["updated_at"] == DBNull.Value ? null : r["updated_at"].ToString(),
            DeletedAt = r["deleted_at"] == DBNull.Value ? null : r["deleted_at"].ToString()
        };
    }

    public UserSkill GetById(int id)
    {
        return DataHelper.ExecuteReader(
            "SELECT * FROM user_skill WHERE skill_id=?id",
            r => Map(r),
            Mapped.Parameter("?id", id))[0];
    }

    public List<UserSkill> GetAll()
    {
        return DataHelper.ExecuteReader("SELECT * FROM user_skill WHERE deleted_at IS NULL", r => Map(r));
    }

    public static List<UserSkill> GetByUserId(int userId)
    {
        return DataHelper.ExecuteReader(
            "SELECT * FROM user_skill WHERE user_id=?id AND deleted_at IS NULL",
            r => Map(r),
            Mapped.Parameter("?id", userId));
    }

    int IRepository<UserSkill>.Insert(UserSkill entity)
    {
        return Insert(entity.UserId, entity.SkillName);
    }

    public static int Insert(int userId, string skillName)
    {
        string now = Function.Now();
        return DataHelper.ExecuteNonQuery(
            "INSERT INTO user_skill (user_id, skill_name, created_at, updated_at) VALUES (?uid, ?skill, ?now, ?now)",
            Mapped.Parameter("?uid", userId),
            Mapped.Parameter("?skill", skillName),
            Mapped.Parameter("?now", now));
    }

    bool IRepository<UserSkill>.Update(UserSkill entity)
    {
        return false;
    }

    bool IRepository<UserSkill>.Delete(int id)
    {
        return DataHelper.ExecuteNonQuery(
            "UPDATE user_skill SET deleted_at=?now WHERE skill_id=?id",
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", id)) > 0;
    }

    public static int DeleteByUser(int userId)
    {
        string now = Function.Now();
        return DataHelper.ExecuteNonQuery(
            "UPDATE user_skill SET deleted_at=?now WHERE user_id=?id",
            Mapped.Parameter("?now", now),
            Mapped.Parameter("?id", userId));
    }
}
