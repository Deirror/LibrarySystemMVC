namespace LibrarysSystem.User_Managment
{
    public class Account
    {
        public static bool IsLogged;

        public static string Nickname;

        public static string Role;

      

        public Account()
        {
            IsLogged = false;
            Nickname = string.Empty;
            Role = string.Empty;
        }
    }
}
