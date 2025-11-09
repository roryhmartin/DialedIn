namespace DialedUp.Application.Users;

public record UserWithRolesAdto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    List<string> Roles);
