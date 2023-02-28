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