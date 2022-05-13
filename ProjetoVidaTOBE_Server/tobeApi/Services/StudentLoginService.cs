using FluentResults;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using tobeApi.Data.Dtos.Students;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class StudentLoginService
    {
        private string GenerateRandomPassword()
        {
            Random randNum = new Random(2012);
            return $"{randNum.Next()}!eF";
        }

        private StudentUserDto GenerateUserLogin(Student student)
        {
            StudentUserDto studentUser = new StudentUserDto();
            studentUser.FullName = student.Name;
            studentUser.Email = student.Email;
            studentUser.Role = "Aluno";
            studentUser.Password = GenerateRandomPassword();
            studentUser.RePassword = studentUser.Password;
            return studentUser;
        }
        public async Task<Result> CreateStudentLoginUser(Student student)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5051");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var serializedUser= JsonConvert.SerializeObject(GenerateUserLogin(student));
            HttpContent content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/register-user", content);
            if (response.IsSuccessStatusCode)
            {
                return Result.Ok();
            }
            return Result.Fail("Error in create user to student");
        }
    }
}
