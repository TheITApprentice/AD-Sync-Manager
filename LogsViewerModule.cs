using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security;
using System.Threading.Tasks;
using Serilog;

namespace ADSyncManager
{
    // LogsViewerModule.cs
    public static class LogsViewerModule
    {
        public static async Task<string[]> GetADSyncLogs(string adSyncServer, string username, string plainTextPassword)
        {
            try
            {
                using (PowerShell ps = PowerShell.Create())
                {
                    ps.AddCommand("Invoke-Command");
                    ps.AddParameter("ComputerName", adSyncServer);
                    ps.AddParameter("ScriptBlock", ScriptBlock.Create(@"
                    Get-EventLog -LogName Application -Source ADSync |
                    Select-Object TimeGenerated, EntryType, Message
                "));

                    // Set the credentials for the PowerShell session
                    ps.AddParameter("Credential", new PSCredential(username, ConvertToSecureString(plainTextPassword)));

                    var results = await Task.Factory.FromAsync(ps.BeginInvoke(), ps.EndInvoke);

                    if (ps.HadErrors)
                    {
                        string errorMessage = $"Errors occurred while retrieving AD Sync logs from server: {adSyncServer}\n";
                        foreach (var error in ps.Streams.Error)
                        {
                            errorMessage += $"{error.Exception.Message} (Category: {error.CategoryInfo.Category})\n";
                            if (error.Exception.InnerException != null)
                            {
                                errorMessage += $"Inner Exception: {error.Exception.InnerException.Message}\n";
                            }
                        }
                        Log.Error(errorMessage);
                        throw new Exception(errorMessage);
                    }

                    List<string> logEntries = new List<string>();
                    foreach (var result in results)
                    {
                        string timeGenerated = result.Properties["TimeGenerated"].Value.ToString();
                        string entryType = result.Properties["EntryType"].Value.ToString();
                        string message = result.Properties["Message"].Value.ToString();
                        string logEntry = $"{timeGenerated} - {entryType}: {message}";
                        logEntries.Add(logEntry);
                    }

                    Log.Information($"Successfully retrieved {logEntries.Count} AD Sync log entries from server: {adSyncServer}");
                    return logEntries.ToArray();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred while retrieving AD Sync logs from server: {adSyncServer}\n";
                errorMessage += "Error Details:\n" + ex.ToString();
                Log.Error(errorMessage);
                return new string[] { errorMessage };
            }
        }

        private static SecureString ConvertToSecureString(string plainText)
        {
            if (plainText == null)
            {
                return null;
            }

            SecureString secureString = new SecureString();
            foreach (char c in plainText)
            {
                secureString.AppendChar(c);
            }

            return secureString;
        }
    }
}