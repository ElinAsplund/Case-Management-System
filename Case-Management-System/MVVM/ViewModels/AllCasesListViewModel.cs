using Case_Management_System.Contexts;
using Case_Management_System.MVVM.Models;
using Case_Management_System.MVVM.Models.Entities;
using Case_Management_System.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Case_Management_System.MVVM.ViewModels;

public partial class AllCasesListViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Case> casesList = null!;

    public async Task populateCaseList()
    {
        CasesList = await DatabaseService.GetAllFromDbAsync();
    }

    public AllCasesListViewModel()
    {
        Task.Run(async () => await populateCaseList());
    }

    public async static Task RemoveCaseAsync(Case clickedCase)
    {

        var _databaseServiceContext = DatabaseService._context;

        var _dbCaseEntity = await _databaseServiceContext.Cases.FirstOrDefaultAsync(x => x.Id == clickedCase.Id);

        if (_dbCaseEntity != null)
        {
            var _allComments = new List<CommentEntity>();

            foreach (var _comment in await _databaseServiceContext.Comments.ToListAsync())
            {
                _allComments.Add(_comment);
            };

            foreach (var _associatedComment in _allComments.Where(x => x.CaseId == _dbCaseEntity.Id))
            {
                _databaseServiceContext.Remove(_associatedComment);
            }

            _databaseServiceContext.Remove(_dbCaseEntity);
            await _databaseServiceContext.SaveChangesAsync();
            
            //CasesList.Remove(clickedCase);
        }
    }
}
