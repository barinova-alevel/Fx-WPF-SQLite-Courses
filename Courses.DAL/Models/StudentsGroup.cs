using System;
using System.Collections.Generic;

namespace Courses.DAL.Models;

public partial class StudentsGroup
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string? Name { get; set; }

    public int? TeacherId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Teacher? Teacher { get; set; }
}
