using WebApplication_Student.Model;

namespace WebApplication_Student.Repository
{
    public interface IStudentRepository
    {
        void Add(Student student);
        Task<IEnumerable<StudentID>> GetAll();
        Task<Student> GetById(int id);
        Task<string> Delete(int id);
        void Update(StudentID student);

        #region Stored Procedure Method

        Task<int> InsertStoredProcedure(Student student);

        Task<StudentID> GetById_SP(int id);

        #endregion
    }
}
