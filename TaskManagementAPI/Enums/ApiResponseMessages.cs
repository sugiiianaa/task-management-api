namespace TaskManagementAPI.Enums
{
    public enum ApiResponseMessages
    {
        Ok,
        Created,
        BadRequest,
        Unauthorized,
        Forbidden,
        NotFound,
        InternalServerError
    }

    public static class ApiResponseMessageHelper
    {
        private static readonly Dictionary<ApiResponseMessages, string> MessageToString = new Dictionary<ApiResponseMessages, string>
        {
            {ApiResponseMessages.Ok, "Request succeeded" },
            {ApiResponseMessages.Created, "New resource was created" },
            {ApiResponseMessages.BadRequest,  "There is client error"},
            {ApiResponseMessages.Unauthorized, "Unauthenticated" },
            {ApiResponseMessages.Forbidden, "The client does not have access rights to the content" },
            {ApiResponseMessages.NotFound, "Resource was not found" },
            {ApiResponseMessages.InternalServerError, "The server has encountered a situation it does not know how to handle"}
        };

        public static string GetMessage(ApiResponseMessages message)
        {
            return MessageToString[message];
        }
    }


}
