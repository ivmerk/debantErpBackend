using DebantErp.DAL.Models;

namespace DebantErp.DAL;
public class SessionDAL : ISessionDAL
{
    public async Task<List<SessionModel>> Get()
    {
        var result = await DbHelper.QueryAsync<SessionModel>(
            "SELECT * FROM DbSession"
        );
        return result.ToList();
    }

    public async Task<SessionModel> Get(int id)
    {
        var result = await DbHelper.QueryAsync<SessionModel>(
            "SELECT * FROM DbSession WHERE UserId = @id",
            new { id }
        );
        return result.FirstOrDefault() ?? new SessionModel();
    }

    public async Task<SessionModel> Get(Guid sessionId)
    {
        var result = await DbHelper.QueryAsync<SessionModel>(
            "SELECT * FROM DbSession WHERE DbSessionId = @sessionId",
            new { sessionId }
        );
        return result.FirstOrDefault() ?? new SessionModel();
    }

    public async Task<int> Create(SessionModel model)
    {
        string sql = "INSERT INTO DbSession (DbSessionId, SessionData, Created, LastAccessed, UserId) VALUES (@DbSessionId, @SessionContent, @Created, @LastAccessed, @UserId)";
        return await DbHelper.ExecuteAsync(sql, model);
    }

    public async Task<bool> IsExist(int id)
    {
        string sql = "SELECT EXISTS (SELECT 1 FROM DbSession WHERE UserId = @id)";
        return await DbHelper.ExecuteScalarAsync<bool>(sql, new { id });
    }

    public async Task<bool> IsExist(Guid sessionId)
    {
        string sql = "SELECT EXISTS (SELECT 1 FROM DbSession WHERE DbSessionId = @sessionId)";
        return await DbHelper.ExecuteScalarAsync<bool>(sql, new { sessionId });
    }

    public async Task<int> Update(SessionModel model)
    {
        string sql = "UPDATE DbSession SET SessionData = @SessionContent, LastAccessed = @LastAccessed, UserId = @UserId WHERE DbSessionId = @DbSessionId";
        return await DbHelper.ExecuteAsync(sql, model);
    }

    public async Task<int> UpdateLastAccessed(Guid sessionId)
    {
        string sql = "UPDATE DbSession SET LastAccessed = @LastAccessed WHERE DbSessionId = @sessionId";
        return await DbHelper.ExecuteAsync(sql, new { sessionId, LastAccessed = DateTime.UtcNow });
    }

    public async Task<int> Delete(int id)
    {
        string sql = "DELETE FROM DbSession WHERE UserId = @id";
        return await DbHelper.ExecuteAsync(sql, new { id });
    }

    public async Task<int> Delete(Guid sessionId)
    {
        string sql = "DELETE FROM DbSession WHERE DbSessionId = @sessionId";
        return await DbHelper.ExecuteAsync(sql, new { sessionId });
    }
}