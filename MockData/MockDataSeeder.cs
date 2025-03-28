using DebantErp.BL.Auth;

namespace DebantErp.MockData
{
    public class MockDataSeeder
    {
        private readonly string _connectionString;
        private readonly IEncrypt _encrypt;

        public MockDataSeeder(string connectionString, IEncrypt encrypt)
        {
            _connectionString = connectionString;
            _encrypt = encrypt;
        }

        public async Task SeedAsync()
        {
            var userMock = new MockUserData(_connectionString, _encrypt);
            var employeeMock = new MockEmployeeData(_connectionString);

            await userMock.InsertAsync();
            await employeeMock.InsertAsync();

            Console.WriteLine("Мок-данные добавлены!");
        }
    }
}
