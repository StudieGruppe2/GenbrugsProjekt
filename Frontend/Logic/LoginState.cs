public class LoginState
{
    public bool IsLoggedIn { get; private set; } = false;

    // ID på den indloggede bruger
    public int MemberId { get; private set; }

    public event Action? OnChange;

    public void SetLogin(int memberId)
    {
        MemberId = memberId;
        IsLoggedIn = true;
        OnChange?.Invoke();
    }

    public void Logout()
    {
        MemberId = 0;
        IsLoggedIn = false;
        OnChange?.Invoke();
    }
}