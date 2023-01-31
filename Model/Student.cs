using System.ComponentModel.DataAnnotations;

namespace WebApplication_Student.Model
{
    public class Student
    {
        [Key]
       // public int Id { get; set; }
        public long RollNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public long ContactNo { get; set; }
        public int HouseNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
    }
}
