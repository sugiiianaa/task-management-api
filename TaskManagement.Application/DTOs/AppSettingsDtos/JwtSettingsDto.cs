﻿namespace TaskManagement.Application.DTOs.AppSettingsDtos
{
    public class JwtSettingsDto
    {
        public string Secret { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public int TokenExpiryInHours { get; set; } = 1;
    }
}
