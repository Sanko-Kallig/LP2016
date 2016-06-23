using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administration_Sloepke
{
    public static class LoggedInAccount
    {
        /// <summary>
        /// Gets or sets the account's database ID.
        /// </summary>
        public static int ID { get; set; }

        /// <summary>
        /// Gets or sets the account's Email.
        /// </summary>
        public static string Email { get; set; }

        /// <summary>
        /// Gets or sets the account's name.
        /// </summary>
        public static string Name { get; set; }

        public static bool IsAdmin { get; set; }

        public static Account AccountLoggedIn { get; set; }

    }
}
