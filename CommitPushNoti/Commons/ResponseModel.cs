namespace CommitPushNoti.Commons
{
    public class ResponseModel
    {
        public int    Code      { get; set; }
        public string Message   { get; set; }
        public bool  IsSuccess() => Code == 1;

        public static ResponseModel GetSuccessResponse(string message) => new() { Code = 1, Message = message };
        public static ResponseModel GetFailureResponse(string message) => new() { Code = -1, Message = message };
    }
}
