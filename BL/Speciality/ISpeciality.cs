using DebantErp.Dtos;
using DebantErp.Rdos;

namespace DebantErp.BL.Speciality
{
    public interface ISpeciality
    {
        public Task<List<SpecialityRdo>> GetSpecialities();
        public Task<SpecialityRdo> GetSpeciality(int id);
        public Task<int> CreateSpeciality(CreateUpdateSpecialityDto dto);
        public Task<int> UpdateSpeciality(int id, CreateUpdateSpecialityDto dto);
        public Task<int> DeleteSpeciality(int id);
    }
}
