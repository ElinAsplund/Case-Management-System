using Case_Management_System.MVVM.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Case_Management_System.MVVM.Models;

internal class Case
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime EntryTime { get; set; } = DateTime.Now;

    public CaseStatus Status { get; set; } = CaseStatus.NotStarted;

    //ÄR DETTA MÖJLIGT? ÄR DET GOD SED? Sätts mha en service???
    public string CustomerFirstName { get; set; } = null!;

    public string CustomerLastName { get; set; } = null!;
}
