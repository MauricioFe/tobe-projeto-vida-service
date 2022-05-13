using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using tobeApi.Models;
using tobeApi.Utils;
using MySql.Data.MySqlClient;
using FluentResults;

namespace tobeApi.Data.Repositories.CalendarEvents
{
    public class CalendarEventRepository : ICalendarEventRepository
    {
        private IDataAccess _contextDb;

        public CalendarEventRepository(IDataAccess contextDb)
        {
            this._contextDb = contextDb;
        }

        public CalendarEvent Create(CalendarEvent model)
        {
            const string sql = @"INSERT INTO `tobe_db`.`eventos_calendario
		                                (
                                            titulo,
                                            data,
                                            hora,
                                            alunos_id
                                        ) 
                                 VALUES (
                                            @titulo,
                                            @data,
                                            @hora,
                                            @alunos_id                                        
                                        )";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("titulo", model.Title),
                new MySqlParameter("data", model.Date),
                new MySqlParameter("hora", model.Time),
                new MySqlParameter("alunos_id", model.StudentId),
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
            const string sql = @"DELETE FROM `tobe_db`.`eventos_calendario`
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

        public CalendarEvent Get(long id)
        {
            const string sql = @"SELECT id,
		                                titulo,
                                        data,
                                        hora,
                                        alunos_id
                                        FROM tobe_db.eventos_calendario 
                                 WHERE id = @id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("id", id)
            };
            var row = _contextDb.GetRow(sql, paramList);
            var calendarEvents = Map(row);
            return calendarEvents;
        }

        public List<CalendarEvent> GetAll()
        {
            const string sql = @"SELECT 
		                                id,
		                                titulo,
                                        data,
                                        hora,
                                        alunos_id
                                        FROM tobe_db.eventos_calendario;";
            var table = _contextDb.GetTable(sql);
            var calendarEventsList = (from DataRow row in table.Rows select Map(row)).ToList();
            return calendarEventsList;
        }

        public List<CalendarEvent> GetCalendarEventsByStudent(long studentId)
        {
            const string sql = @"SELECT 
		                                id,
		                                titulo,
                                        data,
                                        hora,
                                        alunos_id,
                                        nome,
                                        email,
                                        cpf,
                                        estado,
                                        nivel,
                                        data_nascimento,
                                        cidade,
                                        instituicoes_id,
                                        escolaridades_id,
                                        ativo
                                        FROM tobe_db.eventos_calendario ec
                                        INNER JOIN tobe_db.alunos a ON
                                        ec.alunos_id = a.id
                                WHERE alunos_id = @alunos_id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("alunos_id", studentId)
            };
            var table = _contextDb.GetTable(sql, paramList);
            var calendarEventsList = (from DataRow row in table.Rows select MapWithStudent(row)).ToList();
            return calendarEventsList;
        }

        public CalendarEvent Update(CalendarEvent model, long id)
        {
            const string sql = @"UPDATE `tobe_db`.`eventos_calendario`
                                        SET
		                                titulo = @titulo, 
                                        data = @data, 
                                        hora = @hora, 
                                        alunos_id = @alunos_id 
                                 WHERE (id = @id);";

            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("titulo", model.Title),
                new MySqlParameter("data", model.Date),
                new MySqlParameter("hora", model.Time),
                new MySqlParameter("alunos_id", model.StudentId),
                new MySqlParameter("id", id)
            };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return null;
            }
            return Get(id);
        }
        private CalendarEvent Map(DataRow row)
        {
            try
            {
                var calendarEvents = new CalendarEvent
                {
                    Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                    Title = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "titulo"),
                    Date = MapperDataRowToObjectUtil.CreateItemFromRow<DateTime>(row, "data"),
                    Time = MapperDataRowToObjectUtil.CreateItemFromRow<TimeSpan>(row, "hora"),
                    StudentId = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "alunos_id")
                };
                return calendarEvents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error at map the object CalendarEvents \n{ex.Message}");
                return null;
            }
        }

        private CalendarEvent MapWithStudent(DataRow row)
        {
            try
            {
                var student = new Student();
                student.Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id");
                student.Name = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "nome");
                student.Email = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "email");
                student.State = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "estado");
                student.Level = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "nivel");
                student.DateOfBirth = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "data_nascimento");
                student.InstitutionsID = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "instituicoes_id");
                student.SchoolingID = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "escolaridades_id");
                student.Active = MapperDataRowToObjectUtil.CreateItemFromRow<int>(row, "ativo");
                var calendarEvents = new CalendarEvent
                {
                    Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                    Title = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "titulo"),
                    Date = MapperDataRowToObjectUtil.CreateItemFromRow<DateTime>(row, "data"),
                    Time = MapperDataRowToObjectUtil.CreateItemFromRow<TimeSpan>(row, "hora"),
                    StudentId = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "alunos_id"),
                    Student = student
                };
                return calendarEvents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error at map the object CalendarEvents \n{ex.Message}");
                return null;
            }
        }
    }
}
