namespace TaskManagementAPI.Models.ApiResponseModel
{
    public class ApiSuccessResponse<T>
    {
        public bool IsSuccess { get; set; } = true;
        public T? Data { get; set; }
    }
}
