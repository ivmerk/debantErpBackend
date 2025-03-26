using Dapper;
using DebantErp.DAL.Models;
using Npgsql;

public class MockEmployeeData
{
    private readonly string _connectionString;

    public MockEmployeeData(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InsertAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);

        var mockEmployeeDetails = new List<EmployeeDetailsModel>
        {
          new EmployeeDetailsModel
          {
            FirstName = "Ivan",
            LastName = "Merkulov",
            Phone = "1234567890",
            Role = "Admin",
            Email = "s2P7D@example.com",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
          }
        }


        var mockEmployees = new List<EmployeeModel>
        {
            new EmployeeModel
            {
                FirstName = "Ivan",
                LastName = "Merkulov",
                Phone = "1234567890",
                Role = "Admin",
                Email = "s2P7D@example.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        };


        foreach (var employee in mockEmployees)
          if (employee != null)
          {
            string sql =
              "INSERT INTO employees ( first_name, last_name, phone, role, email, created_at, updated_at) VALUES (@firstName, @lastName, @phone, @role, @email, @createdAt, @updatedAt)";
            await connection.ExecuteAsync(sql, employee);
          }
    }
}
