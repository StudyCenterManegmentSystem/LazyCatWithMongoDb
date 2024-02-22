namespace Application.Dtos.TeacherDto;

public class TeacherRegisterResponse
{
    public string Message { get; set; } = string.Empty;
    public string TeacherId { get; set; } = string.Empty;
    public bool Success { get; set; }
}
