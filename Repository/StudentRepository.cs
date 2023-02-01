using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using WebApplication_Student.Data;
using WebApplication_Student.Model;
#nullable disable
namespace WebApplication_Student.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly StudentDbContext _studentDbContext;
        private readonly string ConnectionStrings;

        public StudentRepository(IConfiguration configuration, StudentDbContext studentDbContext)
        {
            _configuration = configuration;
            _studentDbContext = studentDbContext;
            ConnectionStrings = _configuration.GetValue<string>("Connection");
        }

        public IDbConnection connection
        {
            get
            {
                return new SqlConnection(ConnectionStrings);

            }
        }

        public void Add(Student student)
        {
            using (IDbConnection dbconnection = connection)
            {
                string insert = @"INSERT INTO DETAILS (RollNo,Name,FatherName,MotherName,ContactNo,HouseNo,Street,City,State,Country,PostalCode) VALUES(@RollNo,@Name,@FatherName,@MotherName,@ContactNo,@HouseNo,@Street,@City,@State,@Country,@PostalCode)";
                dbconnection.Open();
                dbconnection.Execute(insert, student);
            }
        }


        public Task<IEnumerable<StudentID>> GetAll()
        {
            using (IDbConnection dbconnection = connection)
            {
                string ALL = @"SELECT * FROM DETAILS";
                dbconnection.Open();
                return Task.FromResult(dbconnection.Query<StudentID>(ALL));
            }

        }

        public Task<Student> GetById(int id)
        {
            using (IDbConnection dbconnection = connection)
            {
                string ById = @"SELECT * FROM DETAILS WHERE ID=@ID";
                dbconnection.Open();
                return Task.FromResult(dbconnection.Query<Student>(ById, new { ID = id }).FirstOrDefault());
            }

        }



        public Task<string> Delete(int id)
        {

            using (IDbConnection dbconnection = connection)
            {
                string delete = @"DELETE  FROM DETAILS WHERE ID=@ID";
                dbconnection.Open();
                int Affacted = dbconnection.Execute(delete, new { ID = id });
                if (Affacted != 0)
                    return Task.FromResult(Affacted + " raw Affacted");
                else
                    return Task.FromResult("Not Exist");
            }

        }


        public void Update(StudentID student)
        {

            using (IDbConnection dbconnection = connection)
            {
                string update = @"UPDATE DETAILS SET NAME=@NAME,RollNo=@RollNo,FatherName=@FatherName,MotherName=@MotherName,ContactNo=@ContactNo,HouseNo=@HouseNo,Street=@Street,City=@City,State=@State,Country=@Country,PostalCode=@PostalCode WHERE ID=@ID";
                dbconnection.Open();
                dbconnection.Query(update, student);
            }
        }

        #region STORED PROCEDURE 

        public Task<int> InsertStoredProcedure(Student student)
        {
            int Affacted = 0;
            string cs = _configuration.GetValue<string>("Connection");
            using (SqlConnection connect = new SqlConnection(cs))
            {
                SqlCommand command = new SqlCommand("Insert_SP", connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RollNo", student.RollNo);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@FatherName", student.FatherName);
                command.Parameters.AddWithValue("@MotherName", student.MotherName);
                command.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                command.Parameters.AddWithValue("@HouseNo", student.HouseNo);
                command.Parameters.AddWithValue("@Street", student.Street);
                command.Parameters.AddWithValue("@City", student.City);
                command.Parameters.AddWithValue("@State", student.State);
                command.Parameters.AddWithValue("@Country", student.RollNo);
                command.Parameters.AddWithValue("@PostalCode", student.PostalCode);
                connect.Open();
                Affacted = command.ExecuteNonQuery();


            }
            return Task.FromResult(Affacted);
        }


        //public Task<IEnumerable<StudentID>> SelectAllStoredProcedure()
        //{
        //    var record = 0;
        //    string cs = _configuration.GetValue<string>("Connection");
        //    using (SqlConnection connect = new SqlConnection(cs))
        //    {
        //        SqlCommand command = new SqlCommand("Select_SP", connect);
        //        command.CommandType = System.Data.CommandType.StoredProcedure;
        //        connect.Open();
        //        command.ExecuteReader();

        //    }

        //}


        public Task<StudentID> GetById_SP(int id)
        {
            string cs = _configuration.GetValue<string>("Connection");
            using(SqlConnection connection= new SqlConnection(cs))
            {
                SqlCommand command = new SqlCommand("SelectId_SP", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id",id);
                connection.Open();
                command.ExecuteNonQuery();
            }
            var hit = _studentDbContext.Students.Where(x => x.Id == id).Select(x => new StudentID()
            {
                Id= x.Id,
                RollNo= x.RollNo,
                Name= x.Name,
                FatherName= x.FatherName,
                MotherName= x.MotherName,
                ContactNo= x.ContactNo,
                State= x.State,
                City= x.City,
                Country= x.Country,
                HouseNo= x.HouseNo,
                PostalCode= x.PostalCode,
                Street= x.Street
            }).FirstOrDefaultAsync();

            return hit;

        }

            #endregion

        
    }
}
