using System.ComponentModel.DataAnnotations;

namespace Case_Management_System.MVVM.Models.Entities;

internal class SigningCommentRowEntity
{
    //I will probably not use this class

    [Required]
    public int CommentId { get; set; }

    public CommentEntity Comment { get; set; } = null!;


    [Required]
    public int EmployeeId { get; set; }

    public EmployeeEntity Employee { get; set; } = null!;
}
