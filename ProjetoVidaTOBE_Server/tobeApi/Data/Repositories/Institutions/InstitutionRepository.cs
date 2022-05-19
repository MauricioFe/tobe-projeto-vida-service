using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using tobeApi.Models;
using tobeApi.Utils;
using MySql.Data.MySqlClient;
using FluentResults;

namespace tobeApi.Data.Repositories.Institutions
{
    public class InstitutionRepository : IInstitutionRepository
    {
        private IDataAccess _contextDb;

        public InstitutionRepository(IDataAccess contextDb)
        {
            this._contextDb = contextDb;
        }

        public Institution Create(Institution model)
        {
            const string sql = @"INSERT INTO `tobe_db`.`institutions`
		                                (name) 
                                 VALUES (@name)";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("name", model.Name),
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
            const string sql = @"DELETE FROM `tobe_db`.`institutions`
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

        public Institution Get(long id)
        {
            const string sql = @"SELECT id,
		                                name
                                        FROM tobe_db.institutions 
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
            var institutions = Map(row);
            return institutions;
        }

        public List<Institution> GetAll()
        {
            const string sql = @"SELECT 
		                                 id,
		                                name
                                        FROM tobe_db.institutions;";
            var table = _contextDb.GetTable(sql);
            var institutionsList = (from DataRow row in table.Rows select Map(row)).ToList();
            return institutionsList;
        }

        public Institution Update(Institution model, long id)
        {
            const string sql = @"UPDATE `tobe_db`.`institutions`
                                        SET
		                                name = @name
                                 WHERE id = @id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("name", model.Name),
                new MySqlParameter("id", id),
            };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return null;
            }
            return Get(id);
        }
        private Institution Map(DataRow row)
        {
            var institutions = new Institution
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                Name = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "name"),
            };
            return institutions;
        }
    }
}
