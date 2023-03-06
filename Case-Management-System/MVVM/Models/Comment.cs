using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Case_Management_System.MVVM.Models.Entities;

namespace Case_Management_System.MVVM.Models;

internal class Comment
{
    public int Id { get; set; }

    public string CommentString { get; set; } = null!;
    
    public DateTime EntryTime { get; set; } = DateTime.Now;

    //ÄR DETTA MÖJLIGT? ÄR DET GOD SED? Sätts mha en service???
    //Status här???????????? istället för i case???
    public CaseStatus Status { get; set; }

    public string EmployeeNameInitials { get; set; } = null!;
}
