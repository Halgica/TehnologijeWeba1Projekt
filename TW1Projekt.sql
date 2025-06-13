select * from Payments
select * from Users
select * from Promotions
select * from Reservations
select * from Resources
select * from Reviews
select * from TimeSlots
select * from AuthRoles
select * from AuthUsers

DELETE FROM Users
WHERE RoleId NOT IN (SELECT Id FROM Roles);

USE master;
ALTER DATABASE [tw] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE [tw];


use master

INSERT INTO AuthRoles (Name, RoleId)
VALUES 
    ('Admin', 1),
    ('User', 2);


