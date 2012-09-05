using System.Diagnostics;
using Microsoft.Win32;

namespace BoxApi.V2.Samples
{
	/// <summary>
	/// Defines methods for opening URLs in a default web browser
	/// Source: http://dotnetpulse.blogspot.com/2006/04/opening-url-from-within-c-program.html
	/// </summary>
	internal static class BrowserLauncher
	{
		/// <summary>
		/// Reads path of default browser from registry
		/// </summary>
		/// <returns>Path to the default web browser executable file</returns>
		private static string GetDefaultBrowserPath()
		{
			string key = @"htmlfile\shell\open\command";

			RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(key, false);

			return ((string)registryKey.GetValue(null, null)).Split('"')[1];

		}

		/// <summary>
		/// Opens <paramref name="url"/> in a default web browser
		/// </summary>
		/// <param name="url">Destination URL</param>
		public static void OpenUrl(string url)
		{
			string defaultBrowserPath = GetDefaultBrowserPath();

			Process.Start(defaultBrowserPath, url);
		}
	}
}
