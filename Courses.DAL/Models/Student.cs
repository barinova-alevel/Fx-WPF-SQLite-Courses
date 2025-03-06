using System;
using System.Collections.Generic;

namespace Courses.DAL.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public int GroupId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual StudentsGroup Group { get; set; } = null!;
}
