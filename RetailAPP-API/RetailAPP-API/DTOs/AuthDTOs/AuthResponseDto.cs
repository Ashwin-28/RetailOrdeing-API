namespace RetailAPP_API.DTOs.AuthDTOs
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public List<string>? Errors { get; set; }
    }
}

