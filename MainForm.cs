using System;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.Configuration;
using System.Threading.Tasks;
using System.DirectoryServices.ActiveDirectory;
using System.Net;

namespace ADSyncManager
{
    public partial class MainForm : Form
    {
        private string adSyncServer;
        private TextBox txtUsername;
        private TextBox txtPassword;

        public MainForm()
        {
            InitializeComponent();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            LoadStoredSettings();
            DetectDomainEnvironment();
        }

        private void DetectDomainEnvironment()
        {
            try
            {
                Domain currentDomain = Domain.GetCurrentDomain();
                string domainName = currentDomain.Name;
                // Display the domain name in the designated control
                lblDomainName.Text = $"Current Domain: {domainName}";
            }
            catch (Exception ex)
            {
                ShowError($"Error detecting domain: {ex.Message}");
            }
        }

        private void LoadStoredSettings()
        {
            adSyncServer = ConfigurationManager.AppSettings["ADSyncServer"];
            txtServer.Text = adSyncServer;

            var storedCredentials = GetStoredCredentials();
            if (storedCredentials != null)
            {
                txtUsername.Text = storedCredentials.Username;
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["SavePassword"]))
                {
                    txtPassword.Text = storedCredentials.PlainTextPassword;
                }
                UpdateStatus("Credentials loaded from the previous session.");
            }
        }

        private void SaveStoredSettings()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ADSyncServer"].Value = adSyncServer;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private async void btnSync_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(adSyncServer))
            {
                ShowError("Please specify the AD Sync Server name.");
                return;
            }

            var storedCredentials = GetStoredCredentials();
            if (storedCredentials == null)
            {
                ShowError("Please set the Domain Admin credentials.");
                return;
            }

            // Verify and log credentials format
            if (storedCredentials != null)
            {
                Console.WriteLine($"Stored Credentials Format: {storedCredentials.Username}");
                Console.WriteLine($"Plain-text Password: {storedCredentials.PlainTextPassword}");
            }

            if (rbFullSync.Checked)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to perform a FULL sync? This is not a Delta sync.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            btnSync.Enabled = false;
            txtStatus.Clear();

            try
            {
                if (rbDeltaSync.Checked)
                {
                    UpdateStatus("Running Delta Sync...");
                    string result = await ADSyncModule.RunDeltaSync(storedCredentials.Username, storedCredentials.PlainTextPassword, this);
                    UpdateStatus(result);
                }
                else if (rbFullSync.Checked)
                {
                    UpdateStatus("Running Full Sync...");
                    string result = await ADSyncModule.RunFullSync(storedCredentials.Username, storedCredentials.PlainTextPassword, this);
                    UpdateStatus(result);
                }
            }
            catch (Exception ex)
            {
                ShowError("An error occurred while running the sync:\n" + ex.Message);
            }
            finally
            {
                btnSync.Enabled = true;
            }
        }

        // MainForm.cs
        private async void btnViewLogs_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(adSyncServer))
            {
                ShowError("Please specify the AD Sync Server name.");
                return;
            }

            var storedCredentials = GetStoredCredentials();
            if (storedCredentials == null)
            {
                ShowError("Please set the Domain Admin credentials.");
                return;
            }

            btnViewLogs.Enabled = false;
            txtStatus.Clear();
            UpdateStatus("Retrieving AD Sync logs...");

            try
            {
                var logs = await LogsViewerModule.GetADSyncLogs(adSyncServer, storedCredentials.Username, storedCredentials.PlainTextPassword);
                DisplayLogs(logs);
            }
            catch (Exception ex)
            {
                ShowError("An error occurred while retrieving AD Sync logs:\n" + ex.Message);
            }
            finally
            {
                btnViewLogs.Enabled = true;
            }
        }

        private void btnSetCredentials_Click(object sender, EventArgs e)
        {
            using (var credentialsForm = new CredentialsForm())
            {
                if (credentialsForm.ShowDialog() == DialogResult.OK)
                {
                    string username = credentialsForm.Username;
                    string password = credentialsForm.Password;
                    bool savePassword = credentialsForm.SavePassword;
                    StoreCredentials(username, password, savePassword);
                    UpdateStatus("Credentials stored successfully.");
                }
            }
        }

        private void StoreCredentials(string username, string password, bool savePassword)
        {
            // Assuming the username may include a domain, check and log
            Console.WriteLine($"Storing credentials for: {username}");

            var credentials = new StoredCredentials { Username = username, PlainTextPassword = password };
            var serializedCredentials = System.Text.Json.JsonSerializer.Serialize(credentials);
            var encryptedCredentials = ProtectedData.Protect(System.Text.Encoding.Unicode.GetBytes(serializedCredentials), null, DataProtectionScope.CurrentUser);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["EncryptedCredentials"].Value = Convert.ToBase64String(encryptedCredentials);
            config.AppSettings.Settings["SavePassword"].Value = savePassword.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private StoredCredentials GetStoredCredentials()
        {
            var encryptedCredentials = ConfigurationManager.AppSettings["EncryptedCredentials"];
            if (!string.IsNullOrEmpty(encryptedCredentials))
            {
                var decryptedBytes = ProtectedData.Unprotect(Convert.FromBase64String(encryptedCredentials), null, DataProtectionScope.CurrentUser);
                var decryptedString = System.Text.Encoding.Unicode.GetString(decryptedBytes);
                Console.WriteLine($"Decrypted Credentials: {decryptedString}");

                var serializedCredentials = decryptedString;
                var credentials = System.Text.Json.JsonSerializer.Deserialize<StoredCredentials>(serializedCredentials);

                Console.WriteLine($"Retrieved credentials for: {credentials.Username}"); // Debugging

                return credentials;
            }
            return null;
        }

        private void UpdateStatus(string message)
        {
            txtStatus.AppendText(message + Environment.NewLine);
        }

        private void DisplayLogs(string[] logs)
        {
            txtStatus.Lines = logs;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSetServer_Click(object sender, EventArgs e)
        {
            adSyncServer = txtServer.Text.Trim();
            UpdateStatus($"AD Sync Server set to: {adSyncServer}");
            SaveStoredSettings();
        }
    }

    [System.Serializable]
    public class StoredCredentials
    {
        public string Username { get; set; }
        public string PlainTextPassword { get; set; }
    }
}