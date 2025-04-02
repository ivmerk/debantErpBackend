using DebantErp.DAL.Models;

namespace DebantErp.DAL.Interfaces
{
    public interface ISpecialityDAL
    {
        Task<List<SpecialityModel>> GetSpecialities();
        Task<SpecialityModel> GetSpeciality(int id);
        Task<int> CreateSpeciality(SpecialityModel model);
        Task<int> DeleteSpeciality(int id);
    }
}
