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
            const string sql = @"INSERT INTO `tobe_db`.`alunos`
		                                (nome, 
                                        cpf, 
                                        email, 
                                        estado, 
                                        nivel, 
                                        data_nascimento, 
                                        cidade, 
                                        instituicoes_id, 
                                        escolaridades_id) 
                                 VALUES (@nome,
                                        @cpf,
                                        @email,
                                        @estado, 
                                        @nivel, 
                                        @data_nascimento, 
                                        @cidade, 
                                        @instituicoes_id, 
                                        @escolaridades_id
                                        )";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("nome", model.Name),
                new MySqlParameter("cpf", model.Cpf),
                new MySqlParameter("email", model.Email),
                new MySqlParameter("estado", model.State),
                new MySqlParameter("nivel", model.Level),
                new MySqlParameter("data_nascimento", model.DateOfBirth),
                new MySqlParameter("cidade", model.City),
                new MySqlParameter("instituicoes_id", model.InstitutionsID),
                new MySqlParameter("escolaridades_id", model.SchoolingID),
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
            const string sql = @"DELETE FROM `tobe_db`.`alunos`
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
		                                id, 
                                        nome, 
                                        cpf, 
                                        email, 
                                        estado, 
                                        nivel, 
                                        data_nascimento, 
                                        cidade, 
                                        instituicoes_id, 
                                        escolaridades_id, 
                                        ativo 
                                        FROM tobe_db.alunos 
                                 WHERE id = @id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("id", id)
            };
            var row = _contextDb.GetRow(sql, paramList);
            var student = Map(row);
            return student;
        }

        public List<Student> GetAll()
        {
            const string sql = @"SELECT 
		                                id, 
                                        nome, 
                                        cpf, 
                                        email, 
                                        estado, 
                                        nivel, 
                                        data_nascimento, 
                                        cidade, 
                                        instituicoes_id, 
                                        escolaridades_id, 
                                        ativo 
                                        FROM tobe_db.alunos;";
            var table = _contextDb.GetTable(sql);
            var studentList = (from DataRow row in table.Rows select Map(row)).ToList();
            return studentList;
        }

        public Result ToggleActive(long id)
        {
            Student student = Get(id);
            var active = student.Active == 0 ? 1 : 0;
            const string sql = @"UPDATE `tobe_db`.`alunos`
                                        SET ativo = @ativo
                                 WHERE (id = @id);";

            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("ativo", active),
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
            const string sql = @"UPDATE `tobe_db`.`alunos`
                                        SET
		                                nome = @nome, 
                                        cpf = @cpf, 
                                        email = @email, 
                                        estado = @estado, 
                                        nivel = @nivel, 
                                        data_nascimento = @data_nascimento, 
                                        cidade = @cidade, 
                                        instituicoes_id = @instituicoes_id, 
                                        escolaridades_id = @escolaridades_id, 
                                        ativo = @ativo
                                 WHERE (id = @id);";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("nome", model.Name),
                new MySqlParameter("cpf", model.Cpf),
                new MySqlParameter("email", model.Email),
                new MySqlParameter("estado", model.State),
                new MySqlParameter("nivel", model.Level),
                new MySqlParameter("data_nascimento", model.DateOfBirth),
                new MySqlParameter("cidade", model.City),
                new MySqlParameter("instituicoes_id", model.InstitutionsID),
                new MySqlParameter("escolaridades_id", model.SchoolingID),
                new MySqlParameter("ativo", model.Active),
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
            var student = new Student
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                Name = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "nome"),
                Cpf = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "cpf"),
                Email = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "email"),
                State = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "estado"),
                Level = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "nivel"),
                DateOfBirth = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "data_nascimento"),
                City = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "cidade"),
                InstitutionsID = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "instituicoes_id"),
                SchoolingID = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "escolaridades_id"),
                Active = MapperDataRowToObjectUtil.CreateItemFromRow<int>(row, "ativo"),
            };
            return student;
        }
    }
}
