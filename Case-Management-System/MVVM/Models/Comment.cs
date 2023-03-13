using System;

namespace Case_Management_System.MVVM.Models;

public class Comment
{
    public int Id { get; set; }
    public string CommentString { get; set; } = null!;
    public DateTime EntryTime { get; set; } = DateTime.Now;
    public Employee SigningEmployee { get; set; } = null!;
}
