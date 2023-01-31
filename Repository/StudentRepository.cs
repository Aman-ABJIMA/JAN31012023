using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication_Student.Data;
using WebApplication_Student.Model;

namespace WebApplication_Student.Repository
{
    public class StudentRepository
    {
       
     
        private string ConnectionStrings;

        public StudentRepository()
        {
            ConnectionStrings = @"";
        
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
            using(IDbConnection dbconnection = connection)
            {
                string insert = @"INSERT INTO DETAILS (RollNo,Name,FatherName,MotherName,ContactNo,HouseNo,Street,City,State,Country,PostalCode) VALUES(@RollNo,@Name,@FatherName,@MotherName,@ContactNo,@HouseNo,@Street,@City,@State,@Country,@PostalCode)";
                dbconnection.Open();
                dbconnection.Execute(insert, student);
            }
        }


        public IEnumerable<StudentID> GetAll()
        {
            using (IDbConnection dbconnection = connection)
            {
                string ALL = @"SELECT * FROM DETAILS";
                dbconnection.Open();
                return dbconnection.Query<StudentID>(ALL);
            }

        }

        public  Student GetById(int id)
        {
            using (IDbConnection dbconnection = connection)
            {
                string ById = @"SELECT * FROM DETAILS WHERE ID=@ID";
                dbconnection.Open();
                return dbconnection.Query<Student>(ById, new { ID = id }).FirstOrDefault();
               
               
            }

        }



        public string Delete(int id)
        {
            using (IDbConnection dbconnection = connection)
            {
                string delete = @"DELETE  FROM DETAILS WHERE ID=@ID";
                dbconnection.Open();
                int Affacted = dbconnection.Execute(delete, new { ID = id });
                if (Affacted != 0)
                   return Affacted + " raw Affacted";
                else
                   return "Not Exist";
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


    }
}
