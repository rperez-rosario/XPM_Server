using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class Discipline
{
    public int Id { get; set; }

    public string? Named { get; set; }

    public virtual ICollection<ConsultantDiscipline> ConsultantDisciplines { get; set; } = new List<ConsultantDiscipline>();
}
