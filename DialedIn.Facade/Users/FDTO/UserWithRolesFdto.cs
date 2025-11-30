namespace DialedUp.Facade.Users;

public record UserWithRolesFdto(
    int Id,
    string FirstName,
    string LastName,
    string? Email,
    List<string> Roles);