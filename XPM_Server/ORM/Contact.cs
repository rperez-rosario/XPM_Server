using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class Contact
{
    public int Id { get; set; }

    public string? Named { get; set; }

    public string? Title { get; set; }

    public string? PhoneNumber { get; set; }

    public string? FaxNumber { get; set; }

    public string? EmailAddress { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
