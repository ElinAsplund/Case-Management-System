﻿using Case_Management_System.MVVM.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Case_Management_System.MVVM.Models;

public class Case
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime EntryTime { get; set; } = DateTime.Now;

    // SKA INTE VARA HÄR? UTAN I COMMENT?
    //public CaseStatus Status { get; set; } = CaseStatus.NotStarted;

    public string CustomerFirstName { get; set; } = null!;

    public string CustomerLastName { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public string? CustomerPhoneNumber { get; set; }
}
