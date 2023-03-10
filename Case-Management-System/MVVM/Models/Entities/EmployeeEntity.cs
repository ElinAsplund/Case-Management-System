using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Case_Management_System.MVVM.Models.Entities;


[Index(nameof(NameInitials), IsUnique = true)]

public class EmployeeEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "nvarchar(10)")]
    public string NameInitials { get; set; } = null!;


    public ICollection<CommentEntity> Comments = new HashSet<CommentEntity>();


    #region implicit operators

    public static implicit operator EmployeeEntity(Employee employee)
    {
        return new EmployeeEntity
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            NameInitials = employee.NameInitials
        };
    }

    public static implicit operator Employee(EmployeeEntity employeeEntity)
    {
        return new Employee
        {
            Id = employeeEntity.Id,
            FirstName = employeeEntity.FirstName,
            LastName = employeeEntity.LastName,
            NameInitials = employeeEntity.NameInitials
        };
    }

    #endregion
}
