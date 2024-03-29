﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Case_Management_System.MVVM.Models.Entities;

public class CaseEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength]
    public string Description { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime EntryTime { get; set; } = DateTime.Now;

    [EnumDataType(typeof(CaseStatus))]
    [Column(TypeName = "nvarchar(20)")]
    public CaseStatus Status { get; set; }

    [Required]
    public Guid CustomerId { get; set; }
    
    public CustomerEntity Customer { get; set; } = null!;

    public ICollection<CommentEntity> Comments = new HashSet<CommentEntity>();


    #region implicit operators

    public static implicit operator CaseEntity(Case _case)
    {
        return new CaseEntity
        {
            Id = _case.Id,
            Description = _case.Description,
            EntryTime = _case.EntryTime,
            Status = _case.Status
        };
    }

    public static implicit operator Case(CaseEntity caseEntity)
    {
        return new Case
        {
            Id = caseEntity.Id,
            Description = caseEntity.Description,
            EntryTime = caseEntity.EntryTime,
            Status = caseEntity.Status
        };
    }

    #endregion
}

public enum CaseStatus
{
    NotStarted,
    InProgress,
    Completed
}