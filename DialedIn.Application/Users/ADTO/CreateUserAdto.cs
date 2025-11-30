namespace DialedUp.Application.Users;

public record CreateUserAdto(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    List<int> Roles);