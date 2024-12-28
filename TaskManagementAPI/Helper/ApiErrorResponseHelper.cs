using TaskManagement.Application.Models.Enums;
using TaskManagementAPI.Enums;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Helper
{
    public class ApiErrorResponseHelper
    {
        public IResult SendMessage(ApplicationErrorMessage message)
        {
            switch (message)
            {
                case ApplicationErrorMessage.NotFound:
                    return Results.NotFound(new ApiErrorResponse
                    {
                        Message = ApiResponseMessageHelper.GetMessage(ApiResponseMessages.NotFound)
                    });

                case ApplicationErrorMessage.BadRequest:
                    return Results.BadRequest(new ApiErrorResponse
                    {
                        Message = ApiResponseMessageHelper.GetMessage(ApiResponseMessages.BadRequest)
                    });

                default:
                    return Results.InternalServerError(new ApiErrorResponse
                    {
                        Message = ApiResponseMessageHelper.GetMessage(ApiResponseMessages.InternalServerError)
                    });

            }
        }

        public IResult SendMessage(ApiResponseMessages message)
        {
            switch (message)
            {
                case ApiResponseMessages.BadRequest:
                    return Results.BadRequest(new ApiErrorResponse
                    {
                        Message = ApiResponseMessageHelper.GetMessage(message)
                    });
                case ApiResponseMessages.Unauthorized:
                    return Results.Unauthorized();
                default:
                    return Results.InternalServerError(new ApiErrorResponse
                    {
                        Message = ApiResponseMessageHelper.GetMessage(ApiResponseMessages.InternalServerError)
                    });
            }
        }
    }
}
