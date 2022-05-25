using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using tobeApi.Models;
using tobeApi.Utils;
using MySql.Data.MySqlClient;
using FluentResults;

namespace tobeApi.Data.Repositories.ExerciseTypes
{
    public class ExerciseTypeRepository : IExerciseTypeRepository
    {
        private IDataAccess _contextDb;

        public ExerciseTypeRepository(IDataAccess contextDb)
        {
            this._contextDb = contextDb;
        }

        public ExerciseType Create(ExerciseType model)
        {
            const string sql = @"INSERT INTO `tobe_db`.`exercise_type`
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
            const string sql = @"DELETE FROM `tobe_db`.`exercise_type`
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

        public ExerciseType Get(long id)
        {
            const string sql = @"SELECT id,
		                                description
                                        FROM tobe_db.exercise_type
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
            var exerciseType = Map(row);
            return exerciseType;
        }

        public List<ExerciseType> GetAll()
        {
            const string sql = @"SELECT 
		                                 id,
		                                description
                                        FROM tobe_db.exercise_type;";
            var table = _contextDb.GetTable(sql);
            var exerciseTypeList = (from DataRow row in table.Rows select Map(row)).ToList();
            return exerciseTypeList;
        }

        public ExerciseType Update(ExerciseType model, long id)
        {
            const string sql = @"UPDATE `tobe_db`.`exercise_type`
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
        private ExerciseType Map(DataRow row)
        {
            var exerciseType = new ExerciseType
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "description"),
            };
            return exerciseType;
        }
    }
}
