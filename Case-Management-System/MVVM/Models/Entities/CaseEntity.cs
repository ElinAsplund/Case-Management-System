using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Case_Management_System.MVVM.Models.Entities;

internal class CaseEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength]
    public string Description { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime EntryTime { get; set; } = DateTime.Now;

    [EnumDataType(typeof(CaseStatus))]
    [Column(TypeName = "nvarchar(20)")]
    public CaseStatus Status { get; set; } = CaseStatus.NotStarted;


    [Required]
    public Guid CustomerId { get; set; }
    
    public CustomerEntity Customer { get; set; } = null!;

    public ICollection<CommentEntity> Comments = new HashSet<CommentEntity>();
}

public enum CaseStatus
{
    [EnumMember(Value = "Ej påbörjad")]
    NotStarted,

    [EnumMember(Value = "Pågående")]
    InProgress,

    [EnumMember(Value = "Avslutad")]
    Completed
}