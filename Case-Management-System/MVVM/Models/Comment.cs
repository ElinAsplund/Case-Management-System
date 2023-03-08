using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Case_Management_System.MVVM.Models.Entities;

namespace Case_Management_System.MVVM.Models;

public class Comment
{
    public int Id { get; set; }

    public string CommentString { get; set; } = null!;
    
    public DateTime EntryTime { get; set; } = DateTime.Now;

    public Employee Employee { get; set; } = null!;
}
