using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BoxApi.V2;
using Microsoft.Win32;
using RestSharp.Serializers;

namespace BoxApi.V2.Tests
{
    public partial class TestConfig : Form
    {
        public string AppKey { get; private set; }
        public string TestEmail { get; private set; }
        public string AuthKey { get; private set; }

        public TestConfig()
        {
            InitializeComponent();
        }

        private void TestConfig_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (appKey.Text.Length == 0)
            {
                MessageBox.Show("Please enter a Box Application Key");
                return;
            }
            AppKey = appKey.Text;
            if (email.Text.Length == 0)
            {
                MessageBox.Show("Please enter an email");
                return;
            }
            TestEmail = email.Text;
            var auth = new BoxAuthenticator(appKey.Text);
            string authUrl = auth.GetAuthorizationUrl();
            OpenUrl(authUrl);
            MessageBox.Show(this, "Please confirm access to the app in the launched browser window");
            AuthKey = auth.GetAuthorizationToken();
            Close();
        }

        /// <summary>
        /// Opens <paramref name="url"/> in a default web browser
        /// </summary>
        /// <param name="url">Destination URL</param>
        public static void OpenUrl(string url)
        {
            string key = @"htmlfile\shell\open\command";
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(key, false);
            string defaultBrowserPath = ((string)registryKey.GetValue(null, null)).Split('"')[1];
            Process.Start(defaultBrowserPath, url);
        }
 

    }
}
