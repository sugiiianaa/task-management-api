using TaskManagement.Application.Models.Enums;

namespace TaskManagement.Application.Models
{
    public class ApplicationOutputGenericModel<T>
    {
        public ApplicationErrorMessage? ErrorMessage { get; set; }
        public T? Data { get; set; }
    };
}
