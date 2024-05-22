using System;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Security;
using System.Threading.Tasks;
using System.IO;
using System.DirectoryServices.ActiveDirectory;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;

namespace ADSyncManager
{
    public static class ADSyncModule
    {
        private static readonly string LogFilePath = "ADSyncManager.log";
        private static readonly string ErrorLogFilePath = "ADSyncErrors.log";

        public static async Task<string> RunDeltaSync(string username, string plainTextPassword, MainForm mainForm)
        {
            return await RunSync(username, plainTextPassword, "Delta", mainForm);
        }

        public static async Task<string> RunFullSync(string username, string plainTextPassword, MainForm mainForm)
        {
            return await RunSync(username, plainTextPassword, "Initial", mainForm);
        }

        private static async Task<string> RunSync(string username, string plainTextPassword, string policyType, MainForm mainForm)
        {
            string adSyncServer = GetADSyncServer(mainForm);

            try
            {
                string domainName = GetCurrentDomainName();
                Console.WriteLine($"Domain Name: {domainName}");

                // Check if the username is already in the domain\username format
                string[] usernameParts = username.Split('\\');
                string modifiedUsername;
                if (usernameParts.Length > 1)
                {
                    // Username is already in the domain\username format
                    modifiedUsername = username;
                }
                else
                {
                    // Modify the username format to domain\username
                    modifiedUsername = $"{domainName}\\{username}";
                }

                Console.WriteLine($"Modified Username: {modifiedUsername}");

                string psCommand = $"Import-Module ADSync; $securePassword = ConvertTo-SecureString '{plainTextPassword}' -AsPlainText -Force; Start-ADSyncSyncCycle -PolicyType {policyType} -Credential (New-Object System.Management.Automation.PSCredential('{modifiedUsername}', $securePassword))";

                using (var process = new Process())
                {
                    try
                    {
                        process.StartInfo.FileName = "powershell.exe";
                        process.StartInfo.Arguments = $"-Command \"& {{ {psCommand} }}\"";
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.RedirectStandardError = true;
                        process.StartInfo.CreateNoWindow = true;

                        process.Start();
                        string output = await process.StandardOutput.ReadToEndAsync();
                        string error = await process.StandardError.ReadToEndAsync();
                        process.WaitForExit();

                        if (process.ExitCode != 0)
                        {
                            throw new Exception($"PowerShell command execution failed with exit code {process.ExitCode}. Error: {error}");
                        }

                        string outputMessage = $"{policyType} Sync initiated successfully on server: {adSyncServer}\n";
                        outputMessage += output;

                        await LogMessage($"{policyType} Sync completed successfully on server: {adSyncServer}");
                        return outputMessage;
                    }
                    finally
                    {
                        // No need to clear the password pointer
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred while running {policyType} Sync on server: {adSyncServer}\n";
                errorMessage += "Error Details:\n" + ex.ToString();

                await LogError(errorMessage);
                await LogMessage($"{policyType} Sync failed on server: {adSyncServer}");

                throw new Exception(errorMessage);
            }
        }

        private static SecureString ConvertToSecureString(string plainText)
        {
            if (plainText == null)
            {
                return null; // or throw an exception if you prefer
            }

            SecureString secureString = new SecureString();

            foreach (char c in plainText)
            {
                secureString.AppendChar(c);
            }

            return secureString;
        }
        private static string GetCurrentDomainName()
        {
            try
            {
                Domain currentDomain = Domain.GetCurrentDomain();
                return currentDomain.Name;
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while retrieving the current domain name.\n";
                errorMessage += "Error Details:\n" + ex.ToString();

                LogError(errorMessage).Wait();
                throw new Exception(errorMessage);
            }
        }

        private static string GetADSyncServer(MainForm mainForm)
        {
            TextBox txtServer = mainForm.Controls["txtServer"] as TextBox;

            if (txtServer == null)
            {
                throw new InvalidOperationException("The 'txtServer' control was not found in the MainForm.");
            }

            string adSyncServerName = txtServer.Text.Trim();

            if (string.IsNullOrWhiteSpace(adSyncServerName))
            {
                throw new ArgumentException("AD Sync Server name cannot be empty.");
            }

            return adSyncServerName;
        }

        private static async Task LogMessage(string message)
        {
            try
            {
                await WriteToLogFile(LogFilePath, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the log file: {LogFilePath}");
                Console.WriteLine("Error Details: " + ex.ToString());
            }
        }

        private static async Task LogError(string errorMessage)
        {
            try
            {
                await WriteToLogFile(ErrorLogFilePath, errorMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the error log file: {ErrorLogFilePath}");
                Console.WriteLine("Error Details: " + ex.ToString());
            }
        }

        private static async Task WriteToLogFile(string filePath, string message)
        {
            using (var writer = new StreamWriter(filePath, true))
            {
                await writer.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }
    }
}