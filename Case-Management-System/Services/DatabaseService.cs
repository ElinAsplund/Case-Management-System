using Case_Management_System.Contexts;
using Case_Management_System.MVVM.Models.Entities;
using Case_Management_System.MVVM.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Linq;

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


    public static async Task<ObservableCollection<Comment>> GetSpecificCommentsFromDbAsync(Case currentTask)
    {
        var _allComments = new ObservableCollection<CommentEntity>();
        var _actualComments = new ObservableCollection<Comment>();
        var currentTaskId = currentTask.Id;

        foreach (var _comment in await _context.Comments.ToListAsync())
        {
            _allComments.Add(_comment);
        };

        foreach(var _actualComment in _allComments.Where(x => x.CaseId == currentTaskId))
        {
            Comment _comment = _actualComment;
            _actualComments.Add(_comment);
        }    

        return _actualComments;
    }

    public static async Task ChangeStatusAsync(Case task)
    {
        var _dbCaseEntity = await _context.Cases.FirstOrDefaultAsync(x => x.Id == task.Id);

        _dbCaseEntity!.Status = task.Status;

        _context.Update(_dbCaseEntity);
        await _context.SaveChangesAsync();
    }

    public static async Task SaveCommentToDbAsync(Comment comment, int caseId)
    {
        CommentEntity commentEntity = comment;
        commentEntity.CaseId = caseId;
        _context.Add(commentEntity);
        await _context.SaveChangesAsync();
    }
}
