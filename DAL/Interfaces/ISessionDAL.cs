using DebantErp.DAL.Models;

namespace DebantErp.DAL;
public interface ISessionDAL : IBaseDAL<SessionModel>
{
  Task<SessionModel> Get(Guid sessionId);
  Task<bool> IsExist(Guid sessionId);
  Task<int> Delete(Guid sessionId);
  Task<int> UpdateLastAccessed(Guid sessionId);
}
