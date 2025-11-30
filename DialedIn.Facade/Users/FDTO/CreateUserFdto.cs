namespace DialedUp.Facade.Users;

public record CreateUserFdto(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    List<int> RoleIds);