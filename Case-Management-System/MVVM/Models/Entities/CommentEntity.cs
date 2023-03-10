using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Case_Management_System.MVVM.Models.Entities;

public class CommentEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength]
    public string Comment { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime EntryTime { get; set; }


    [Required]
    public int CaseId { get; set; }

    public CaseEntity Case { get; set; } = null!;

    [Required]
    public int EmployeeId { get; set; }

    public EmployeeEntity Employee { get; set; } = null!;


    #region implicit operators

    public static implicit operator CommentEntity(Comment comment)
    {
        return new CommentEntity
        {
            Comment = comment.CommentString,
            EntryTime = comment.EntryTime,
            EmployeeId = comment.SigningEmployee.Id
        };
    }

    public static implicit operator Comment(CommentEntity commentEntity)
    {
        return new Comment
        {
            Id = commentEntity.Id,
            CommentString = commentEntity.Comment,
            EntryTime = commentEntity.EntryTime,
            SigningEmployee = commentEntity.Employee
        };
    }

    #endregion
}
