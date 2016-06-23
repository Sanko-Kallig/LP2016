using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administration_Sloepke
{
    public partial class LoginForm : Form
    {
        private Administration Admin;
        public LoginForm()
        {
            InitializeComponent();
            Admin = new Administration();

        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string email = tbxEmail.Text;
            string password = tbxPassword.Text;
            LoggedInAccount.AccountLoggedIn = Admin.GetAccount(email, password);
            if (LoggedInAccount.AccountLoggedIn != null)
            {
                this.Hide();
                OverviewForm oVM = new OverviewForm(Admin);
                oVM.ShowDialog();
                this.Show();
                this.Logout();
            }
            else
            {
                MessageBox.Show("Kon niet inloggen, controleer of uw Email of wachtwoord correct is.");
            }
        }

        /// <summary>
        /// Handles the logging out of the account, by purging all information from the static logged in account,
        /// and by clearing the text fields.
        /// </summary>
        private void Logout()
        {
            LoggedInAccount.AccountLoggedIn = null;
            this.tbxEmail.Text = null;
            this.tbxPassword.Text = null;
        }
    }
}
