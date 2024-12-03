namespace CommitPushNoti.Services.Interfaces
{
    public interface ICollectionService
    {
        Task<ResponseModel> AddCollection(string id, string name, string url);
    }
}
