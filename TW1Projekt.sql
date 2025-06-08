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