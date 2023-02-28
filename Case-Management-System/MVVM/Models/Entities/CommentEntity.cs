using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Case_Management_System.MVVM.Models.Entities;

internal class CommentEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength]
    public string Comment { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime EntryTime { get; set; } = DateTime.Now;


    [Required]
    public int CaseId { get; set; }

    public CaseEntity Case { get; set; } = null!;

    [Required]
    public int EmployeeId { get; set; }

    public EmployeeEntity Employee { get; set; } = null!;

}
