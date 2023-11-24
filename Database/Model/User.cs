using System;
using System.Collections.Generic;

namespace Diplom.Client.Database.Model;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? Password { get; set; }

    public string? Status { get; set; }

    public bool RulledPass { get; set; }
}
