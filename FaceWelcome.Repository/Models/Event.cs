﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FaceWelcome.Repository.Models;

public partial class Event
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public int? GuestNumber { get; set; }

    public int? GroupNumber { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<WelcomeTemplate> WelcomeTemplates { get; set; } = new List<WelcomeTemplate>();
}