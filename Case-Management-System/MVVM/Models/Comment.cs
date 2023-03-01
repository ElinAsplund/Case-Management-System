using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Case_Management_System.MVVM.Models;

internal class Comment
{
    public int Id { get; set; }

    public string CommentString { get; set; } = null!;
    
    public DateTime EntryTime { get; set; } = DateTime.Now;

    //ÄR DETTA MÖJLIGT? ÄR DET GOD SED? Sätts mha en service???
    public string EmployeeNameInitials { get; set; } = null!;
}
