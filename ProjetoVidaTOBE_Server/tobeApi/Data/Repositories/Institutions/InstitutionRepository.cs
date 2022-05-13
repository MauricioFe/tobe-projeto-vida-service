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
            const string sql = @"INSERT INTO `tobe_db`.`instituicoes`
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
            const string sql = @"DELETE FROM `tobe_db`.`instituicoes`
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
		                                descricao
                                        FROM tobe_db.instituicoes 
                                 WHERE id = @id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("id", id)
            };
            var row = _contextDb.GetRow(sql, paramList);
            var instituicoes = Map(row);
            return instituicoes;
        }

        public List<Institution> GetAll()
        {
            const string sql = @"SELECT 
		                                 id,
		                                descricao
                                        FROM tobe_db.instituicoes;";
            var table = _contextDb.GetTable(sql);
            var instituicoesList = (from DataRow row in table.Rows select Map(row)).ToList();
            return instituicoesList;
        }

        public Institution Update(Institution model, long id)
        {
            const string sql = @"UPDATE `tobe_db`.`instituicoes`
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
        private Institution Map(DataRow row)
        {
            var instituicoes = new Institution
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "descricao"),
            };
            return instituicoes;
        }
    }
}
