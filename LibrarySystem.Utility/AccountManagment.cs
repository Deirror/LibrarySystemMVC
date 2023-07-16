using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Utility
{
    public class AccountManagment
    {
        public static string? Nickname;
        public static string? Role;
        public static string? IsConfirmed;

        public static bool IsAdmin()
        {
            if (Role == "Admin")
                return true;
            else
                return false;
        }

        public static bool IsLibrarian()
        {
            if (Role == "Librarian")
                return true;
            else
                return false;

        }

        public static bool IsReader()
        {
            if (Role == "Reader")
                return true;
            else
                return false;

        }



    }
}
