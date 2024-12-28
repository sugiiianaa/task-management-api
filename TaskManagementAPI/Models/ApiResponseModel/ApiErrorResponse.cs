namespace TaskManagementAPI.Models.ApiResponseModel
{
    public class ApiErrorResponse
    {
        public bool IsSuccess { get; set; } = false;
        public required string Message { get; set; }
    }
}
