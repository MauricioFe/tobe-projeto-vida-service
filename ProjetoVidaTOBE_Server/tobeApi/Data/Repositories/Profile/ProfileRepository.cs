using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using tobeApi.Models;
using tobeApi.Utils;
using MySql.Data.MySqlClient;
using FluentResults;

namespace tobeApi.Data.Repositories.Profile
{
    public class ProfileRepository : IProfileRepository
    {
        private IDataAccess _contextDb;

        public ProfileRepository(IDataAccess contextDb)
        {
            this._contextDb = contextDb;
        }

        public Profiles Create(Profiles model)
        {
            const string sql = @"INSERT INTO `tobe_db`.`profiles`
		                                (description) 
                                 VALUES (@description)";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("description", model.Description),
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
            const string sql = @"DELETE FROM `tobe_db`.`profiles`
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

        public Profiles Get(long id)
        {
            const string sql = @"SELECT id,
		                                description
                                        FROM tobe_db.profiles
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
            var profiles = Map(row);
            return profiles;
        }

        public List<Profiles> GetAll()
        {
            const string sql = @"SELECT 
		                                 id,
		                                description
                                        FROM tobe_db.profiles;";
            var table = _contextDb.GetTable(sql);
            var profilesList = (from DataRow row in table.Rows select Map(row)).ToList();
            return profilesList;
        }

        public Profiles Update(Profiles model, long id)
        {
            const string sql = @"UPDATE `tobe_db`.`profiles`
                                        SET
		                                description = @description
                                 WHERE id = @id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("description", model.Description),
                new MySqlParameter("id", id),
            };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return null;
            }
            return Get(id);
        }
        private Profiles Map(DataRow row)
        {
            var profiles = new Profiles
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "description"),
            };
            return profiles;
        }
    }
}
