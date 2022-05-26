using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using tobeApi.Models;
using tobeApi.Utils;
using MySql.Data.MySqlClient;
using FluentResults;

namespace tobeApi.Data.Repositories.Exercises
{
    public class ExerciseRepository : IExerciseRepository
    {
        private IDataAccess _contextDb;

        public ExerciseRepository(IDataAccess contextDb)
        {
            this._contextDb = contextDb;
        }

        public Exercise Create(Exercise model)
        {
            const string sql = @"INSERT INTO `tobe_db`.`exercises` 
                                    (
                                        `enumeration`, 
                                        `question`, 
                                        `suggested_answer`, 
                                        `profiles_id`, 
                                        `exercise_type_id`
                                    )
                                    VALUES (
                                        @enumeration, 
                                        @question, 
                                        @suggested_answer, 
                                        @profiles_id, 
                                        @exercise_type_id
                                    );";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("enumeration", model.Enumeration),
                new MySqlParameter("question", model.Question),
                new MySqlParameter("suggested_answer", model.SuggestedAnswer),
                new MySqlParameter("profiles_id", model.ProfilesId),
                new MySqlParameter("exercise_type_id", model.ExerciseTypeId)
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
            const string sql = @"DELETE FROM `tobe_db`.`exercises`
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

        public Exercise Get(long id)
        {
            const string sql = @"select 
                                    e.id, 
                                    e.enumeration, 
                                    e.question, 
                                    e.suggested_answer, 
                                    e.profiles_id, 
                                    e.exercise_type_id
                                    from 
                                    tobe_db.exercises e
                                    INNER JOIN tobe_db.exercise_type et on et.id = e.exercise_type_id
                                    INNER JOIN tobe_db.profiles p on p.id = e.profiles_id
                                        FROM tobe_db.exercises
                                 WHERE e.id = @id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("id", id)
            };
            var row = _contextDb.GetRow(sql, paramList);
            if (row == null)
            {
                return null;
            }
            var exercise = Map(row);
            return exercise;
        }

        public List<Exercise> GetAll()
        {
            const string sql = @"select 
                                    e.id, 
                                    e.enumeration, 
                                    e.question, 
                                    e.suggested_answer, 
                                    e.profiles_id, 
                                    e.exercise_type_id
                                    from 
                                    tobe_db.exercises e
                                    INNER JOIN tobe_db.exercise_type et on et.id = e.exercise_type_id
                                    INNER JOIN tobe_db.profiles p on p.id = e.profiles_id";
            var table = _contextDb.GetTable(sql);
            var exerciseList = (from DataRow row in table.Rows select Map(row)).ToList();
            return exerciseList;
        }

        public Exercise Update(Exercise model, long id)
        {
            const string sql = @"UPDATE `tobe_db`.`exercises`
                                        SET
		                                enumeration = @enumeration, 
                                        question = @question, 
                                        suggested_answer = @suggested_answer, 
                                        profiles_id = @profiles_id,
                                        exercise_type_id =  @exercise_type_id
                                 WHERE id = @id;";
            var paramList = new MySqlParameter[]
                      {
                new MySqlParameter("enumeration", model.Enumeration),
                new MySqlParameter("question", model.Question),
                new MySqlParameter("suggested_answer", model.SuggestedAnswer),
                new MySqlParameter("profiles_id", model.ProfilesId),
                new MySqlParameter("exercise_type_id", model.ExerciseTypeId),
                new MySqlParameter("id", id)
                      };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return null;
            }
            return Get(id);
        }
        private Exercise Map(DataRow row)
        {
            var exerciseType = new ExerciseType
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "description"),
            };
            var profiles = new Profiles
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "description"),
            };
            var exercise = new Exercise
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                Enumeration = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "enumeration"),
                Question = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "question"),
                SuggestedAnswer = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "suggestedAnswer"),
                ExerciseTypeId = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "exerciseTypeId"),
                ProfilesId = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "profilesId"),
                ExerciseType = exerciseType,
                Profiles = profiles
            };
            return exercise;
        }
    }
}
