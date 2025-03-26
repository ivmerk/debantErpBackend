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
            //var productMock = new MockProductData(_connectionString);

            await userMock.InsertAsync();
            // await productMock.InsertAsync();

            Console.WriteLine("Мок-данные добавлены!");
        }
    }
}
