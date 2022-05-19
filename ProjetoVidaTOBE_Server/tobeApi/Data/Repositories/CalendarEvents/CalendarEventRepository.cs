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
            const string sql = @"INSERT INTO `tobe_db`.`calendar_events`
		                                (
                                            title,
                                            date,
                                            time,
                                            student_id
                                        ) 
                                 VALUES (
                                            @title,
                                            @date,
                                            @time,
                                            @student_id                                        
                                        )";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("title", model.Title),
                new MySqlParameter("date", model.Date),
                new MySqlParameter("time", model.Time),
                new MySqlParameter("student_id", model.StudentId),
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
            const string sql = @"DELETE FROM `tobe_db`.`calendar_events`
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
		                                title,
                                        date,
                                        time,
                                        student_id
                                        FROM tobe_db.calendar_events 
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
            var calendarEvents = Map(row);
            return calendarEvents;
        }

        public List<CalendarEvent> GetAll()
        {
            const string sql = @"SELECT 
		                                id,
		                                title,
                                        date,
                                        time,
                                        student_id
                                        FROM tobe_db.calendar_events;";
            var table = _contextDb.GetTable(sql);
            var calendarEventsList = (from DataRow row in table.Rows select Map(row)).ToList();
            return calendarEventsList;
        }

        public List<CalendarEvent> GetCalendarEventsByStudent(long studentId)
        {
            const string sql = @"SELECT 
		                                ec.id,
		                                ec.title,
                                        ec.date,
                                        ec.time,
                                        ec.student_id,
                                        a.name, 
                                        a.cpf, 
                                        a.email, 
                                        a.state, 
                                        a.level, 
                                        a.date_of_birth, 
                                        a.city, 
                                        a.institution_id, 
                                        a.scholling_id, 
                                        a.active
                                        FROM tobe_db.calendar_events ec
                                        INNER JOIN tobe_db.student a ON
                                        ec.student_id = a.id
                                WHERE student_id = @student_id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("student_id", studentId)
            };
            var table = _contextDb.GetTable(sql, paramList);
            var calendarEventsList = (from DataRow row in table.Rows select MapWithStudent(row)).ToList();
            return calendarEventsList;
        }

        public CalendarEvent Update(CalendarEvent model, long id)
        {
            const string sql = @"UPDATE `tobe_db`.`calendar_events`
                                        SET
		                                title = @title, 
                                        date = @date, 
                                        time = @time, 
                                        student_id = @student_id 
                                 WHERE id = @id;";

            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("title", model.Title),
                new MySqlParameter("date", model.Date),
                new MySqlParameter("time", model.Time),
                new MySqlParameter("student_id", model.StudentId),
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
                    Title = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "title"),
                    Date = MapperDataRowToObjectUtil.CreateItemFromRow<DateTime>(row, "date"),
                    Time = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "time"),
                    StudentId = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "student_id")
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
                var student = new Student
                {
                    Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "student_id"),
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
                };
                var calendarEvents = new CalendarEvent
                {
                    Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "id"),
                    Title = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "title"),
                    Date = MapperDataRowToObjectUtil.CreateItemFromRow<DateTime>(row, "date"),
                    Time = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "time"),
                    StudentId = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "student_id"),
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
