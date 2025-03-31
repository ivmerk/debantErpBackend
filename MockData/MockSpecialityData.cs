using Dapper;
using DebantErp.DAL.Models;
using Npgsql;

public class MockSpecialityData
{
    private readonly string _connectionString;

    public MockSpecialityData(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InsertAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var transaction = await connection.BeginTransactionAsync();

        var specialityCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM specialities"
        );
        if (specialityCount > 0)
            return;

        var specialities = new List<dynamic>
        {
            new { Name = "Швея", IsActual = true },
            new { Name = "Ткач", IsActual = true },
            new { Name = "Закройщик", IsActual = true },
            new { Name = "Технолог швейного производства", IsActual = true },
            new { Name = "Оператор швейного оборудования", IsActual = true },
        };

        try
        {
            var specialityIds = new List<int>();

            foreach (var speciality in specialities)
            {
                string insertSpecialitySql =
                    @"
                    INSERT INTO specialities (name, is_actual) 
                    VALUES (@Name, @IsActual) 
                    ON CONFLICT (name) DO NOTHING 
                    RETURNING id";

                var id = await connection.ExecuteScalarAsync<int?>(
                    insertSpecialitySql,
                    new { Name = speciality.Name, IsActual = speciality.IsActual },
                    transaction
                );
                if (id.HasValue)
                {
                    specialityIds.Add(id.Value);
                }
            }

            if (!specialityIds.Any())
            {
                string getExistingSpecialitiesSql = "SELECT id FROM specialities";
                specialityIds = (
                    await connection.QueryAsync<int>(
                        getExistingSpecialitiesSql,
                        transaction: transaction
                    )
                ).ToList();
            }
            // Получаем 10 случайных сотрудников
            string getEmployeesSql = "SELECT id FROM employees ORDER BY RANDOM() LIMIT 10";
            var employeeIds = (
                await connection.QueryAsync<int>(getEmployeesSql, transaction: transaction)
            ).ToList();

            if (!employeeIds.Any())
            {
                throw new Exception("No employees found. Please insert employees first.");
            }

            // Создаем 10 записей в employee_specialities
            var random = new Random();
            var employeeSpecialities = new List<dynamic>();

            foreach (var employeeId in employeeIds)
            {
                var specialityId = specialityIds[random.Next(specialityIds.Count)];
                employeeSpecialities.Add(
                    new
                    {
                        EmployeeId = employeeId,
                        SpecialityId = specialityId,
                        DateFrom = DateTime.UtcNow.Date,
                        IsActual = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    }
                );
            }

            string insertEmployeeSpecialitiesSql =
                @"
                INSERT INTO employee_specialities (employee_id, speciality_id, date_from, is_actual, created_at, updated_at) 
                VALUES (@EmployeeId, @SpecialityId, @DateFrom, @IsActual, @CreatedAt, @UpdatedAt)";

            await connection.ExecuteAsync(
                insertEmployeeSpecialitiesSql,
                employeeSpecialities,
                transaction
            );

            await transaction.CommitAsync();
            Console.WriteLine("Mock data inserted successfully.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error inserting mock data: {ex.Message}");
        }
    }
}
