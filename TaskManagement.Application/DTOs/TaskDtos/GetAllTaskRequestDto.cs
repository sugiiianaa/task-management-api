using TaskManagement.Application.Enums;

namespace TaskManagement.Application.DTOs.TaskDtos
{
    public class GetAllTaskRequestDto
    {
        public int UserId { get; set; }
        public Sort SortOrder { get; set; }
    }
}
