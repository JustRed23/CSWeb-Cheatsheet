namespace ExternalAuth.ViewModels;

public class RegisterViewModel : LoginViewModel
{
    public string Email { get; set; }
    public string ConfirmPassword { get; set; }
}