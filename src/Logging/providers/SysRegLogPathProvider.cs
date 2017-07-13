/// <summary>
/// SysRegLogPathProvider.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Diagnostics.Logging
{
    using System;
    using Microsoft.Win32;

    /// <summary>
    /// Uses the system registry to find the path were to log Rosetta stuff.
    /// </summary>
    public class SysRegLogPathProvider : ILogPathProvider
    {
        private const string UserRoot = "HKEY_CURRENT_USER";
        private const string Subkey = "SOFTWARE\\Rosetta";
        private const string KeyName = UserRoot + "\\" + Subkey;

        private const string Name = "LogPath";

        // Cached values (for the whole application lifecycle)
        private static bool initialized = false;
        private static string logPath;

        /// <summary>
        /// Gets the path to where logging info should be emitted.
        /// If the key is not found or it has an empty value, <code>null</code> is returned.
        /// </summary>
        public string LogPath
        {
            get
            {
                if (!initialized)
                {
                    logPath = this.FetchValueFromRegistry();
                    initialized = true;
                }

                return logPath;
            }
        }

        private string FetchValueFromRegistry()
        {
            var value = Registry.GetValue(KeyName, Name, string.Empty) as string;

            if (value == null)
            {
                // Invalid key, we just return null
                return null;
            }

            if (value == string.Empty)
            {
                return null;
            }

            return value;
        }

        private bool KeyExists => Registry.GetValue(UserRoot, Subkey, null) != null;
    }
}
