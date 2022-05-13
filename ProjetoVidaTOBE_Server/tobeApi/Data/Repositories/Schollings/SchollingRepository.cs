using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using tobeApi.Models;
using tobeApi.Utils;
using MySql.Data.MySqlClient;
using FluentResults;

namespace tobeApi.Data.Repositories.Schollings
{
    public class SchollingRepository : ISchollingRepository
    {
        private IDataAccess _contextDb;

        public SchollingRepository(IDataAccess contextDb)
        {
            this._contextDb = contextDb;
        }

        public Scholling Create(Scholling model)
        {
            const string sql = @"INSERT INTO `tobe_db`.`escolaridades`
		                                (descricao) 
                                 VALUES (@descricao)";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("descricao", model.Description),
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
            const string sql = @"DELETE FROM `tobe_db`.`escolaridades`
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

        public Scholling Get(long id)
        {
            const string sql = @"SELECT id,
		                                descricao
                                        FROM tobe_db.escolaridades
                                 WHERE id = @id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("id", id)
            };
            var row = _contextDb.GetRow(sql, paramList);
            var schollings = Map(row);
            return schollings;
        }

        public List<Scholling> GetAll()
        {
            const string sql = @"SELECT 
		                                 id,
		                                descricao
                                        FROM tobe_db.escolaridades;";
            var table = _contextDb.GetTable(sql);
            var schollingsList = (from DataRow row in table.Rows select Map(row)).ToList();
            return schollingsList;
        }

        public Scholling Update(Scholling model, long id)
        {
            const string sql = @"UPDATE `tobe_db`.`escolaridades`
                                        SET
		                                descricao = @descricao, 
                                 WHERE (id = @id);";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("descricao", model.Description),
                new MySqlParameter("id", id),
            };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return null;
            }
            return Get(id);
        }
        private Scholling Map(DataRow row)
        {
            var schollings = new Scholling
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "descricao"),
            };
            return schollings;
        }
    }
}
