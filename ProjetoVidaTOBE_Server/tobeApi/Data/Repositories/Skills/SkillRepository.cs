using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using tobeApi.Models;
using tobeApi.Utils;
using MySql.Data.MySqlClient;
using FluentResults;

namespace tobeApi.Data.Repositories.Skills
{
    public class SkillRepository : ISkillRepository
    {
        private IDataAccess _contextDb;

        public SkillRepository(IDataAccess contextDb)
        {
            this._contextDb = contextDb;
        }

        public Skill Create(Skill model)
        {
            const string sql = @"INSERT INTO `tobe_db`.`skills`
		                                (description, profiles_id) 
                                 VALUES (@description, @profiles_id)";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("description", model.Description),
                new MySqlParameter("profiles_id", model.ProfilesId),
            };
            long lastInsertedId = _contextDb.ExecuteCommand(sql, true, paramList);
            if (lastInsertedId == 0)
            {
                return null;
            }
            return Get(lastInsertedId);
        }

        public Result Delete(long id)
        {
            const string sql = @"DELETE FROM `tobe_db`.`skills`
                                 WHERE (id = @id);";
            var paramList = new MySqlParameter[]
            {

                new MySqlParameter("id", id),
            };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return Result.Fail("Error in delete");
            }
            return Result.Ok();
        }

        public Skill Get(long id)
        {
            const string sql = @"SELECT s.id as s_id,
		                                s.description as s_description,
                                        profiles_id,
                                        p.description as p_description
                                        FROM tobe_db.skills s
                                        INNER JOIN tobe_db.profiles p 
                                        ON p.id = s.profiles_id
                                 WHERE id = @id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("id", id)
            };
            var row = _contextDb.GetRow(sql, paramList);
            if (row == null)
            {
                return null;
            }
            var skills = Map(row);
            return skills;
        }

        public List<Skill> GetAll()
        {
            const string sql = @"SELECT s.id as s_id,
		                                s.description as s_description,
                                        profiles_id,
                                        p.description as p_description
                                        FROM tobe_db.skills s
                                        INNER JOIN tobe_db.profiles p 
                                        ON p.id = s.profiles_id";
            var table = _contextDb.GetTable(sql);
            var skillsList = (from DataRow row in table.Rows select Map(row)).ToList();
            return skillsList;
        }

        public Skill Update(Skill model, long id)
        {
            const string sql = @"UPDATE `tobe_db`.`skills`
                                        SET
		                                description = @description,
                                        profiles_id = @profiles_id
                                 WHERE id = @id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("description", model.Description),
                new MySqlParameter("profiles_id", model.ProfilesId),
                new MySqlParameter("id", id),
            };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return null;
            }
            return Get(id);
        }
        private Skill Map(DataRow row)
        {
            var profile = new StudentProfile
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "profiles_id"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "p_description"),
            };

            var skills = new Skill
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "s_id"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "s_description"),
                ProfilesId = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "profiles_id"),
            };
            return skills;
        }
    }
}
