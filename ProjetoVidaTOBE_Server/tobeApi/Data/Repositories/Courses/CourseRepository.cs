using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using tobeApi.Models;
using tobeApi.Utils;
using MySql.Data.MySqlClient;
using FluentResults;

namespace tobeApi.Data.Repositories.Courses
{
    public class CourseRepository : ICourseRepository
    {
        private IDataAccess _contextDb;

        public CourseRepository(IDataAccess contextDb)
        {
            this._contextDb = contextDb;
        }

        public Course Create(Course model)
        {
            const string sql = @"INSERT INTO `tobe_db`.`courses`
		                                (
                                        name, 
                                        description, 
                                        workload, 
                                        launch_date, 
                                        update_date) 
                                 VALUES (
                                        @name, 
                                        @description, 
                                        @workload, 
                                        @launch_date, 
                                        @update_date
                                        )";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("name", model.Name),
                new MySqlParameter("description", model.Description),
                new MySqlParameter("workload", model.Workload),
                new MySqlParameter("launch_date", model.LaunchDate),
                new MySqlParameter("update_date", model.UpdateDate)
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
            const string sql = @"DELETE FROM `tobe_db`.`courses`
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

        public Course Get(long id)
        {
            const string sql = @"SELECT 
		                               	id, 
                                        name, 
                                        description, 
                                        workload, 
                                        launch_date, 
                                        update_date, 
                                        active
                                        FROM tobe_db.courses
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
            var course = Map(row);
            return course;
        }

        public List<Course> GetAll()
        {
            const string sql = @"SELECT 
		                               	id, 
                                        name, 
                                        description, 
                                        workload, 
                                        launch_date, 
                                        update_date, 
                                        active
                                        FROM tobe_db.courses";
            var table = _contextDb.GetTable(sql);
            var courseList = (from DataRow row in table.Rows select Map(row)).ToList();
            return courseList;
        }

        public List<Course> GetAllCoursesLandPage()
        {
            const string sql = @"SELECT 
		                               	id, 
                                        name, 
                                        description
                                        FROM tobe_db.courses
                                        Where Active = 1";
            var table = _contextDb.GetTable(sql);
            var courseList = (from DataRow row in table.Rows select Map(row)).ToList();
            return courseList;
        }

        public Result ToggleActive(long id)
        {
            Course course = Get(id);
            if (course == null)
            {
                return Result.Fail("Course not exists");
            }
            var active = course.Active == 0 ? 1 : 0;
            const string sql = @"UPDATE `tobe_db`.`courses`
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

        public Course Update(Course model, long id)
        {
            const string sql = @"UPDATE `tobe_db`.`courses`
                                        SET
		                                name = @name, 
                                        description = @description, 
                                        workload = @workload, 
                                        update_date = @update_date
                                 WHERE id = @id;";
            var paramList = new MySqlParameter[]
                      {
                new MySqlParameter("name", model.Name),
                new MySqlParameter("description", model.Description),
                new MySqlParameter("workload", model.Workload),
                new MySqlParameter("launch_date", model.LaunchDate),
                new MySqlParameter("update_date", model.UpdateDate),
                new MySqlParameter("id", id)
                      };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return null;
            }
            return Get(id);
        }
        private Course Map(DataRow row)
        {
            var course = new Course
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "Id"),
                Name = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "name"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "description"),
                Workload = MapperDataRowToObjectUtil.CreateItemFromRow<double>(row, "workload"),
                LaunchDate = MapperDataRowToObjectUtil.CreateItemFromRow<DateTime>(row, "launch_date"),
                UpdateDate = MapperDataRowToObjectUtil.CreateItemFromRow<DateTime>(row, "update_date"),
            };
            return course;
        }
    }
}
