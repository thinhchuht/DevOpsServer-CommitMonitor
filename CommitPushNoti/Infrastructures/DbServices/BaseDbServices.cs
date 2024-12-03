namespace CommitPushNoti.Infrastructures.DbServices
{
    public class BaseDbServices(DevopsContext devopsContext, ILogger<BaseDbServices> logger) : IBaseDbServices
    {
        public async Task<List<T>> GetAllAsync<T>() where T : class => await devopsContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync<T>(dynamic id) where T : class =>  await devopsContext.Set<T>().FindAsync(id);

        public async Task<ResponseModel> AddAsync<T>(T entity) where T : class
        {
            try
            {
                await devopsContext.Set<T>().AddAsync(entity);
                await devopsContext.SaveChangesAsync();
                return ResponseModel.GetSuccessResponse("Add successed");
            }
            catch (Exception ex)
            {
                logger.LogError(message: ex.ToString());
                return ResponseModel.GetFailureResponse($"Something went wrong while adding ${typeof(T).Name}");
            }
        }

        public async Task<ResponseModel> UpdateAsync<T>(T entity) where T : class
        {
            try
            {
                devopsContext.Set<T>().Update(entity);
                await devopsContext.SaveChangesAsync();
                return ResponseModel.GetSuccessResponse("Update successed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return ResponseModel.GetFailureResponse($"Something went wrong while updating ${typeof(T).Name}");
            }
        }

        public async Task<ResponseModel> DeleteAsync<T>(T entity) where T : class
        {
            try
            {
                devopsContext.Set<T>().Remove(entity);
                await devopsContext.SaveChangesAsync();
                return ResponseModel.GetSuccessResponse("Delete successed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return ResponseModel.GetFailureResponse($"Something went wrong while Deleting ${typeof(T).Name}");
            }

        }

        public async Task<T> GetByPropertyAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await devopsContext.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<int> CountAsync<T>() where T : class
        {
            return await devopsContext.Set<T>().CountAsync();
        }

    }
}
