using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class ConsultantDiscipline
{
    public int Id { get; set; }

    public int? Consultant { get; set; }

    public int? Discipline { get; set; }

    public virtual ICollection<ConsultantEmployed> ConsultantEmployeds { get; set; } = new List<ConsultantEmployed>();

    public virtual Consultant? ConsultantNavigation { get; set; }

    public virtual Discipline? DisciplineNavigation { get; set; }
}
