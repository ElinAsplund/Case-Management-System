using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Case_Management_System.MVVM.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
internal class CustomerEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column(TypeName = "char(13)")]
    public string? PhoneNumber { get; set; }


    public ICollection<CaseEntity> Cases = new HashSet<CaseEntity>();

    #region implicit operators

    //Takes a Customer and makes a CustomerEntity
    public static implicit operator CustomerEntity(Customer customer)
    {
        return new CustomerEntity
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber
        };
    }

    //Takes a CustomerEntity and makes a Customer
    public static implicit operator Customer(CustomerEntity customerEntity)
    {
        return new Customer
        {
            Id= customerEntity.Id,
            FirstName = customerEntity.FirstName,
            LastName = customerEntity.LastName,
            Email = customerEntity.Email,
            PhoneNumber = customerEntity.PhoneNumber
        };
    }

    //Takes a Case and makes a CustomerEntity
    public static implicit operator CustomerEntity(Case task)
    {
        return new CustomerEntity
        {
            FirstName = task.CustomerFirstName,
            LastName = task.CustomerLastName,
            Email = task.CustomerEmail, 
            PhoneNumber = task.CustomerPhoneNumber
        };
    }

    #endregion
}
