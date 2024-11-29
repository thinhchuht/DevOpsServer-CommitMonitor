namespace CommitPushNoti.Services.Classes
{
    public class CollectionService(IBaseDbServices baseDbServices) : ICollectionService
    {
        public async Task<ResponseModel> AddCollection(string id, string name, string url)
        {
            var collection = await baseDbServices.GetByIdAsync<Collection>(id);
            if (collection == null) return await baseDbServices.AddAsync(new Collection(id, name, url));
            return ResponseModel.GetSuccessResponse("Already exist collection");
        }
    }
}
