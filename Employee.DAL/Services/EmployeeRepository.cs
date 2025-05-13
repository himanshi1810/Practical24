
using Employee.DAL.Models.DTO;
using Employee.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.DAL.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbContext _context;

        public EmployeeRepository(DbContext context)
        {
            _context = context;
        }

        public void Create(AddEmployeeDTO emp)
        {
            using (var conn = _context.GetConnection())
            {
                string query = @"INSERT INTO Employee 
                                (EmployeeName, EmployeeSalary, DepartmentId, EmployeeEmail, EmployeeJoiningDate, EmployeeStatus)
                                 VALUES (@Name, @Salary, @DeptId, @Email, @JoinDate, 'Active')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@Salary", emp.EmployeeSalary);
                cmd.Parameters.AddWithValue("@DeptId", emp.DepartmentId);
                cmd.Parameters.AddWithValue("@Email", emp.EmployeeEmail);
                cmd.Parameters.AddWithValue("@JoinDate", emp.EmployeeJoiningDate);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(EmployeeModel emp)
        {
            using (var conn = _context.GetConnection())
            {
                string query = @"UPDATE Employee SET 
                                EmployeeName = @Name,
                                EmployeeSalary = @Salary,
                                DepartmentId = @DeptId,
                                EmployeeEmail = @Email
                                WHERE EmployeeId = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", emp.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@Salary", emp.EmployeeSalary);
                cmd.Parameters.AddWithValue("@DeptId", emp.DepartmentId);
                cmd.Parameters.AddWithValue("@Email", emp.EmployeeEmail);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SoftDelete(int id)
        {
            using (var conn = _context.GetConnection())
            {
                string query = "UPDATE Employee SET EmployeeStatus = 'Inactive' WHERE EmployeeId = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public EmployeeModel GetById(int id)
        {
            EmployeeModel emp = null;
            using (var conn = _context.GetConnection())
            {
                string query = "SELECT * FROM Employee WHERE EmployeeId = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    emp = new EmployeeModel
                    {
                        EmployeeId = (int)reader["EmployeeId"],
                        EmployeeName = reader["EmployeeName"].ToString(),
                        EmployeeSalary = (decimal)reader["EmployeeSalary"],
                        DepartmentId = (int)reader["DepartmentId"],
                        EmployeeEmail = reader["EmployeeEmail"].ToString(),
                        EmployeeJoiningDate = (DateTime)reader["EmployeeJoiningDate"],
                        EmployeeStatus = reader["EmployeeStatus"].ToString()
                    };
                }
            }
            return emp;
        }

        public List<EmployeeModel> GetAll()
        {
            List<EmployeeModel> list = new List<EmployeeModel>();
            using (var conn = _context.GetConnection())
            {
                string query = "SELECT * FROM Employee";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new EmployeeModel
                    {
                        EmployeeId = (int)reader["EmployeeId"],
                        EmployeeName = reader["EmployeeName"].ToString(),
                        EmployeeSalary = (decimal)reader["EmployeeSalary"],
                        DepartmentId = (int)reader["DepartmentId"],
                        EmployeeEmail = reader["EmployeeEmail"].ToString(),
                        EmployeeJoiningDate = (DateTime)reader["EmployeeJoiningDate"],
                        EmployeeStatus = reader["EmployeeStatus"].ToString()
                    });
                }
            }
            return list;
        }
    }
}
