﻿namespace Business.Models.Login;

public class ChangePasswordForm
{
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
