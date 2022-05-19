using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using tobeApi.Models;
using tobeApi.Utils;
using MySql.Data.MySqlClient;
using FluentResults;

namespace tobeApi.Data.Repositories.Students
{
    public class StudentRepository : IStudentRepository
    {
        private IDataAccess _contextDb;

        public StudentRepository(IDataAccess contextDb)
        {
            this._contextDb = contextDb;
        }

        public Student Create(Student model)
        {
            const string sql = @"INSERT INTO `tobe_db`.`student`
		                                (name, 
                                        cpf, 
                                        email, 
                                        state, 
                                        level, 
                                        date_of_birth, 
                                        city, 
                                        institution_id, 
                                        scholling_id) 
                                 VALUES (@name,
                                        @cpf,
                                        @email,
                                        @state, 
                                        @level, 
                                        @date_of_birth, 
                                        @city, 
                                        @institution_id, 
                                        @scholling_id
                                        )";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("name", model.Name),
                new MySqlParameter("cpf", model.Cpf),
                new MySqlParameter("email", model.Email),
                new MySqlParameter("state", model.State),
                new MySqlParameter("level", model.Level),
                new MySqlParameter("date_of_birth", model.DateOfBirth),
                new MySqlParameter("city", model.City),
                new MySqlParameter("institution_id", model.InstitutionsID),
                new MySqlParameter("scholling_id", model.SchoolingID),
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
            const string sql = @"DELETE FROM `tobe_db`.`student`
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

        public Student Get(long id)
        {
            const string sql = @"SELECT 
		                                a.id as idAluno, 
                                        a.name, 
                                        a.cpf, 
                                        a.email, 
                                        a.state, 
                                        a.level, 
                                        a.date_of_birth, 
                                        a.city, 
                                        a.institution_id, 
                                        a.scholling_id, 
                                        a.active, 
                                        i.name as desc_instituicao,
                                        e.description as desc_escolaridade
                                        FROM tobe_db.student a
                                        INNER JOIN tobe_db.Institutions i on a.institution_id = i.id
                                        INNER JOIN tobe_db.Schollings e on a.scholling_id = e.id
                                 WHERE a.id = @id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("id", id)
            };
            var row = _contextDb.GetRow(sql, paramList);
            if (row == null)
            {
                return null;
            }
            var student = Map(row);
            return student;
        }

        public List<Student> GetAll()
        {
            const string sql = @"SELECT 
		                                a.id as idAluno, 
                                        a.name, 
                                        a.cpf, 
                                        a.email, 
                                        a.state, 
                                        a.level, 
                                        a.date_of_birth, 
                                        a.city, 
                                        a.institution_id, 
                                        a.scholling_id, 
                                        a.active, 
                                        i.name as desc_instituicao,
                                        e.description as desc_escolaridade
                                        FROM tobe_db.student a
                                        INNER JOIN tobe_db.Institutions i on a.institution_id = i.id
                                        INNER JOIN tobe_db.Schollings e on a.scholling_id = e.id";
            var table = _contextDb.GetTable(sql);
            var studentList = (from DataRow row in table.Rows select Map(row)).ToList();
            return studentList;
        }

        public Result ToggleActive(long id)
        {
            Student student = Get(id);
            if (student == null)
            {
                return Result.Fail("User not exists");
            }
            var active = student.Active == 0 ? 1 : 0;
            const string sql = @"UPDATE `tobe_db`.`student`
                                        SET active = @active
                                 WHERE (id = @id);";

            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("active", active),
                new MySqlParameter("id", id)
            };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return Result.Fail("Error in update the field active");
            }
            return Result.Ok();
        }

        public Student Update(Student model, long id)
        {
            const string sql = @"UPDATE `tobe_db`.`student`
                                        SET
		                                name = @name, 
                                        cpf = @cpf, 
                                        email = @email, 
                                        state = @state, 
                                        level = @level, 
                                        date_of_birth = @date_of_birth, 
                                        city = @city, 
                                        institution_id = @institution_id, 
                                        scholling_id = @scholling_id
                                 WHERE id = @id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("name", model.Name),
                new MySqlParameter("cpf", model.Cpf),
                new MySqlParameter("email", model.Email),
                new MySqlParameter("state", model.State),
                new MySqlParameter("level", model.Level),
                new MySqlParameter("date_of_birth", model.DateOfBirth),
                new MySqlParameter("city", model.City),
                new MySqlParameter("institution_id", model.InstitutionsID),
                new MySqlParameter("scholling_id", model.SchoolingID),
                new MySqlParameter("id", id),
            };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return null;
            }
            return Get(id);
        }
        private Student Map(DataRow row)
        {
            var institution = new Institution()
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "institution_id"),
                Name = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "desc_instituicao"),
            };
            var scholling = new Scholling()
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "scholling_id"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "desc_escolaridade"),
            };
            var student = new Student
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "idAluno"),
                Name = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "name"),
                Cpf = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "cpf"),
                Email = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "email"),
                State = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "state"),
                Level = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "level"),
                DateOfBirth = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "date_of_birth"),
                City = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "city"),
                InstitutionsID = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "institution_id"),
                SchoolingID = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "scholling_id"),
                Active = MapperDataRowToObjectUtil.CreateItemFromRow<int>(row, "active"),
                Institution = institution,
                scholling = scholling,
            };
            return student;
        }
    }
}
