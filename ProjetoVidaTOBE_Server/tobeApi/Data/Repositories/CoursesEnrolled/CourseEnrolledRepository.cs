using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using tobeApi.Models;
using tobeApi.Utils;
using MySql.Data.MySqlClient;
using FluentResults;

namespace tobeApi.Data.Repositories.CoursesEnrolled
{
    public class CourseEnrolledRepository : ICourseEnrolledRepository
    {
        private IDataAccess _contextDb;

        public CourseEnrolledRepository(IDataAccess contextDb)
        {
            this._contextDb = contextDb;
        }

        public Result Create(CourseEnrolled model)
        {
            const string sql = @"INSERT INTO `tobe_db`.`courses_enrolleds` 
                                (
                                  student_id, 
                                  courses_id
                                ) VALUES 
                                (
                                  @student_id, 
                                  @courses_id
                                );";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("student_id", model.StudentId),
                new MySqlParameter("courses_id", model.CourseId)
            };
            long lastInsertedId = _contextDb.ExecuteCommand(sql, false, paramList);
            if (lastInsertedId == 0)
            {
                return Result.Fail("Error");
            }
            return Result.Ok();
        }
        public List<CourseEnrolled> Get(long idStudent)
        {
            const string sql = @"SELECT  
	                                    ce.student_id, 
                                        ce.courses_id, 
                                        ce.status,
                                        c.name, 
                                        c.description as c_description, 
                                        c.workload, 
                                        c.launch_date, 
                                        c.update_date, 
                                        c.active as c_active,
                                        s.name as s_name, 
                                        s.cpf, 
                                        s.email, 
                                        s.state, 
                                        s.level, 
                                        s.date_of_birth, 
                                        s.city, 
                                        s.institution_id, 
                                        s.scholling_id, 
                                        s.active as s_active,
                                        sc.description as sc_description,
                                        i.name as i_name
                                    FROM tobe_db.courses_enrolleds ce
                                    INNER JOIN Courses c on c.id = ce.courses_id
                                    INNER JOIN Student s on s.id = ce.student_id
                                    INNER JOIN schollings sc on s.scholling_id = sc.id
                                    INNER JOIN Institutions i on s.institution_id = i.id
                                 WHERE ce.student_id = @student_id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("student_id", idStudent)
            };
            var table = _contextDb.GetTable(sql, paramList);
            var courseList = (from DataRow row in table.Rows select Map(row)).ToList();
            return courseList;
        }

        public List<CourseEnrolled> GetAll()
        {
            const string sql = @"SELECT  
	                                    ce.student_id, 
                                        ce.courses_id, 
                                        ce.status,
                                        c.name, 
                                        c.description as c_description, 
                                        c.workload, 
                                        c.launch_date, 
                                        c.update_date, 
                                        c.active as c_active,
                                        s.name as s_name, 
                                        s.cpf, 
                                        s.email, 
                                        s.state, 
                                        s.level, 
                                        s.date_of_birth, 
                                        s.city, 
                                        s.institution_id, 
                                        s.scholling_id, 
                                        s.active as s_active,
                                        sc.description as sc_description,
                                        i.name as i_name
                                    FROM tobe_db.courses_enrolleds ce
                                    INNER JOIN Courses c on c.id = ce.courses_id
                                    INNER JOIN Student s on s.id = ce.student_id
                                    INNER JOIN schollings sc on s.scholling_id = sc.id
                                    INNER JOIN Institutions i on s.institution_id = i.id";
            var table = _contextDb.GetTable(sql);
            var courseList = (from DataRow row in table.Rows select Map(row)).ToList();
            return courseList;
        }

        public CourseEnrolled GetByIds(long studentId, long courseId)
        {

            const string sql = @"SELECT  
	                                    ce.student_id, 
                                        ce.courses_id, 
                                        ce.status,
                                        c.name, 
                                        c.description as c_description, 
                                        c.workload, 
                                        c.launch_date, 
                                        c.update_date, 
                                        c.active as c_active,
                                        s.name as s_name, 
                                        s.cpf, 
                                        s.email, 
                                        s.state, 
                                        s.level, 
                                        s.date_of_birth, 
                                        s.city, 
                                        s.institution_id, 
                                        s.scholling_id, 
                                        s.active as s_active,
                                        sc.description as sc_description,
                                        i.name as i_name
                                    FROM tobe_db.courses_enrolleds ce
                                    INNER JOIN Courses c on c.id = ce.courses_id
                                    INNER JOIN Student s on s.id = ce.student_id
                                    INNER JOIN schollings sc on s.scholling_id = sc.id
                                    INNER JOIN Institutions i on s.institution_id = i.id
                            WHERE ce.student_id = @student_id AND ce.courses_id = @courses_id;";
            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("student_id", studentId),
                new MySqlParameter("courses_id", courseId)
            };
            var row = _contextDb.GetRow(sql, paramList);
            if (row == null)
            {
                return null;
            }
            var course = Map(row);
            return course;
        }

        public Result UpdateStatus(long studentId, long courseId)
        {
            CourseEnrolled course = GetByIds(studentId, courseId);
            if (course == null)
            {
                return Result.Fail("CourseEnrolled not exists");
            }
            var status = "0";
            status = CourseEnrolledRepositoryUtils.getNewStatus(course, status);
            const string sql = @"UPDATE `tobe_db`.`courses_enrolleds`
                                        SET status = @status
                                  WHERE student_id = @student_id AND courses_id = @courses_id;";

            var paramList = new MySqlParameter[]
            {
                new MySqlParameter("status", status),
                new MySqlParameter("student_id", studentId),
                new MySqlParameter("courses_id", courseId)
            };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return Result.Fail("Error in update the field active");
            }
            return Result.Ok();
        }



        public Result UnrolledCourse(long studentId, long CourseId)
        {
            CourseEnrolled course = GetByIds(studentId, CourseId);
            if (course == null)
            {
                return Result.Fail("CourseEnrolled not exists");
            }
            const string sql = @"DELETE FROM `tobe_db`.`courses_enrolleds`
                                 WHERE student_id= @student_id and courses_id = @courses_id;";
            var paramList = new MySqlParameter[]
            {

                new MySqlParameter("student_id", studentId),
                new MySqlParameter("courses_id", CourseId),
            };
            long affectRows = _contextDb.ExecuteCommand(sql, false, paramList);
            if (affectRows == 0)
            {
                return Result.Fail("Error in delete");
            }
            return Result.Ok();

        }
        private CourseEnrolled Map(DataRow row)
        {
            var course = new Course
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "courses_id"),
                Name = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "name"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "c_description"),
                Workload = MapperDataRowToObjectUtil.CreateItemFromRow<double>(row, "workload"),
                LaunchDate = MapperDataRowToObjectUtil.CreateItemFromRow<DateTime>(row, "launch_date"),
                UpdateDate = MapperDataRowToObjectUtil.CreateItemFromRow<DateTime>(row, "update_date"),
            };

            var institution = new Institution()
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "institution_id"),
                Name = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "i_name"),
            };
            var scholling = new Scholling()
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "scholling_id"),
                Description = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "sc_description"),
            };
            var student = new Student
            {
                Id = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "student_id"),
                Name = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "s_name"),
                Cpf = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "cpf"),
                Email = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "email"),
                State = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "state"),
                Level = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "level"),
                DateOfBirth = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "date_of_birth"),
                City = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "city"),
                InstitutionsID = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "institution_id"),
                SchoolingID = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "scholling_id"),
                Active = MapperDataRowToObjectUtil.CreateItemFromRow<int>(row, "s_active"),
                Institution = institution,
                scholling = scholling,
            };

            var courseEnrolled = new CourseEnrolled
            {
                StudentId = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "student_id"),
                CourseId = MapperDataRowToObjectUtil.CreateItemFromRow<long>(row, "courses_id"),
                Status = MapperDataRowToObjectUtil.CreateItemFromRow<string>(row, "status"),
                Student = student,
                Course = course
            };
            return courseEnrolled;
        }
    }
}
