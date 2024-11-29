using CommitPushNoti.Commons;
using System.Linq.Expressions;

namespace CommitPushNoti.Infrastructures.DbServices
{
    public interface IBaseDbServices
    {
        Task<List<T>> GetAllAsync<T>() where T : class;
        Task<T> GetByIdAsync<T>(dynamic id) where T : class;
        Task<T> GetByPropertyAsync<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<ResponseModel> AddAsync<T>(T entity) where T : class;
        Task<ResponseModel> UpdateAsync<T>(T entity) where T : class;
        Task<ResponseModel> DeleteAsync<T>(T entity) where T : class;
        Task<int> CountAsync<T>() where T : class;
    }
}
