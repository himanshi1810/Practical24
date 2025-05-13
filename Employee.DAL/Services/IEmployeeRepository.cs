using Employee.DAL.Models;
using Employee.DAL.Models.DTO;

namespace Employee.DAL.Services
{
    public interface IEmployeeRepository
    {
        void Create(AddEmployeeDTO emp);
        List<EmployeeModel> GetAll();
        EmployeeModel GetById(int id);
        void SoftDelete(int id);
        void Update(EmployeeModel emp);
    }
}