﻿namespace SignalRChatApi.Dtos
{
    public class UserLoginDto
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool KeepLogedIn { get; set; } = true;
    }
}
