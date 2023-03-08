﻿using Case_Management_System.Contexts;
using Case_Management_System.MVVM.Models.Entities;
using Case_Management_System.MVVM.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Case_Management_System.Services;

internal class DatabaseService
{
    private static DatabaseContext _context = new DatabaseContext();

    public static async Task SaveToDbAsync(Case task)
    {
        CustomerEntity customerEntity = task;
        CaseEntity caseEntity = task;

        _context.Add(customerEntity);
        caseEntity.CustomerId = customerEntity.Id;
        _context.Add(caseEntity);
        await _context.SaveChangesAsync();
    }

    public static async Task<ObservableCollection<Case>> GetAllFromDbAsync()
    {
        var _cases = new ObservableCollection<Case>();

        foreach (var _case in await _context.Cases.Include(x => x.Customer).ToListAsync())
        {
            CustomerEntity customerEntity = _case.Customer;
            CaseEntity caseEntity = _case;

            _cases.Add(new Case
            {
                Id = caseEntity.Id,
                Description = caseEntity.Description,
                EntryTime = caseEntity.EntryTime,
                Status = caseEntity.Status,
                CustomerFirstName = customerEntity.FirstName,
                CustomerLastName = customerEntity.LastName,
                CustomerEmail = customerEntity.Email,
                CustomerPhoneNumber = customerEntity.PhoneNumber
            });
        }

        return _cases;
    }
    public static async Task<ObservableCollection<Employee>> GetAllEmployeesAsync()
    {
        var _employees = new ObservableCollection<Employee>();

        foreach (var _employee in await _context.Employees.ToListAsync())
        {
            EmployeeEntity employeeEntity = _employee;

            _employees.Add(new Employee
            {
                Id = employeeEntity.Id,
                FirstName = employeeEntity.FirstName,
                LastName = employeeEntity.LastName,
                NameInitials = employeeEntity.NameInitials
            });
        }

        return _employees;
    }

    public static async Task ChangeStatusAsync(Case task)
    {
        var _dbCaseEntity = await _context.Cases.FirstOrDefaultAsync(x => x.Id == task.Id);

        _dbCaseEntity!.Status = task.Status;

        _context.Update(_dbCaseEntity);
        await _context.SaveChangesAsync();
    }

    public static async Task SaveCommentToDbAsync(Comment comment)
    {

        //COMMENT ENTITY BEHÖVER:
        //Comment
        //EntryTime
        //CaseId
        //EmployeeId
        //CustomerEntity customerEntity = task;
        //CaseEntity caseEntity = task;

        //_context.Add(customerEntity);
        //caseEntity.CustomerId = customerEntity.Id;
        //_context.Add(caseEntity);
        await _context.SaveChangesAsync();
    }
}
