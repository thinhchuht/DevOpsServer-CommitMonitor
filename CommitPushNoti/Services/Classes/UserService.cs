namespace CommitPushNoti.Services.Classes
{
    public class UserService(IBaseDbServices baseDbServices, DevopsContext devopsContext) : IUserService
    {
        public async Task<ResponseModel> AddUser(User newUser)
        {
            var user = await baseDbServices.GetByIdAsync<User>(newUser.Email);
            if (user == null)
            {
                return await baseDbServices.AddAsync(newUser);
            }
            return ResponseModel.GetSuccessResponse("Already exist User");
        }

        public async Task<List<User>> GetAllUser() => await devopsContext.Users.Include(u => u.PullRequests).Include(u => u.CommitDetails).ToListAsync();

        public async Task<List<UserReporter>> GetUserReport(List<string> selectedEmails, DateTime startDate, DateTime endDate)
        {
            Console.WriteLine(startDate.ToString());
            Console.WriteLine(endDate.ToString());
            var userReport = await devopsContext.Users
                .Where(u => selectedEmails.Contains(u.Email))
                .Select(u => new UserReporter(
                    u.Name,
                    u.Email,
                    u.PullRequests.Count(pr => pr.CreatedDate >= startDate && pr.CreatedDate <= endDate),
                    u.CommitDetails.Count(cd => cd.CreateDate >= startDate && cd.CreateDate <= endDate),
                    u.CommitDetails
                        .Where(cd => cd.CreateDate >= startDate && cd.CreateDate <= endDate) 
                        .Sum(cd => cd.LineChange) 
                ))
                .ToListAsync();

            return userReport;
        }
    }
}
