namespace EventManager.Api.Models;

public record UserDto
{
    public required string Id { get; init; }
    public required string Username { get; init; }
    public required string Role { get; init; }
    public required DateTime CreatedAt { get; init; }
};

public record CreateUserDto : UserDto { };

public record UpdateUserDto : CreateUserDto { };

public record PatchUserDto
{
    public required string Id { get; init; }
    public string? Username { get; init; }

    public string? Role { get; init; }
    public required DateTime CreatedAt { get; init; }
}

public record UserResponseDto : UserDto { }
