using System;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;

namespace ADSyncManager
{
    public partial class CredentialsForm : Form
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool SavePassword { get; private set; }

        public CredentialsForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Username = txtUsername.Text.Trim();
            Password = txtPassword.Text;
            SavePassword = chkSavePassword.Checked;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    bool isValid = context.ValidateCredentials(Username, Password);
                    if (!isValid)
                    {
                        MessageBox.Show("Invalid username or password. Please try again.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while validating the credentials:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnTestCredentials_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    bool isValid = context.ValidateCredentials(username, password);
                    if (isValid)
                    {
                        MessageBox.Show("Credentials are valid.", "Test Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials. Please try again.", "Test Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while testing the credentials:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}