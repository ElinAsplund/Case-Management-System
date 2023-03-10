SELECT * FROM Customers
SELECT * FROM Cases

SELECT 
	cu.FirstName,
	cu.LastName,
	cu.PhoneNumber,
	ca.Description,
	ca.Status
FROM Customers cu 
JOIN Cases ca ON cu.Id = ca.CustomerId 


SELECT *
FROM Cases ca
JOIN Customers cu ON ca.CustomerId = cu.Id 

SELECT 
	ca.Description, co.Comment, e.NameInitials, e.FirstName, cu.FirstName, cu.LastName
FROM Comments co
JOIN Cases ca ON co.CaseId = ca.Id
JOIN Employees e ON co.EmployeeId = e.Id
JOIN Customers cu ON ca.CustomerId = cu.Id