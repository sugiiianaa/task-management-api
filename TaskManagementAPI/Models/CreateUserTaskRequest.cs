﻿namespace TaskManagementAPI.Models
{
    public class CreateUserTaskRequest
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpectedFinishDate { get; set; }
        public required string TaskStatus { get; set; }
    }
}