using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Win32;
namespace Prediktor.Log.Implementation
{
    public static class EnvironmentLogPath
    {
        public static void SetEnvironmentLogPath()
        {
            string registryKeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Prediktor\Apis";
            string registryValueName = "ApisInstallRoot";

            string apisInstallRoot = Registry.GetValue(registryKeyPath, registryValueName, null) as string;

            if (apisInstallRoot == null)
            {
                throw new InvalidOperationException("The application is not correctly installed. 'ApisInstallRoot' registry key is missing.");
            }

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APIS_LOG_PATH")))
            {
                Environment.SetEnvironmentVariable("APIS_LOG_PATH", Path.Combine(apisInstallRoot, "Logs"), EnvironmentVariableTarget.Machine);
            }
            
        }

    }
}
