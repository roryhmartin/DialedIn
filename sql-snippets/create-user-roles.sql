-- CREATE TABLE UserRoles (
-- 	UserId INT NOT NULL,
--     RoleId INT NOT NULL,
--     PRIMARY KEY (UserId, RoleId),
--     FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
--     FOREIGN KEY (ROLEId) REFERENCES Roles(Id) ON DELETE CASCADE
-- );

-- SELECT * FROM UserRoles; 

-- INSERT INTO UserRoles (UserId, RoleId)
-- SELECT 1, Id FROM Roles WHERE Name = 'Manager';

-- INSERT INTO UserRoles (UserId, RoleId)
-- SELECT 1, Id FROM Roles WHERE Name = 'Standard';

SELECT 
	Users.Id AS USerID,
    Users.first_name AS FirstName,
    Users.last_name AS LastName,
    Roles.Name AS RoleName
	FROM Users
    JOIN UserRoles ON Users.id = UserRoles.UserId
    JOIN Roles ON UserRoles.RoleId = Roles.Id
    WHERE Users.Id = 1;