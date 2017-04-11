using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thermostat
{
    public partial class NestConfigForm : Form
    {
        public NestConfigForm()
        {
            InitializeComponent();
            webBrowser1.WebBrowserShortcutsEnabled = false;
            webBrowser1.Navigate(NestAPI.GetPinUrl()); //open the in-line browser to Nest's user auth page
            statusLabel.Text = "Status: Not Submitted";
            statusLabel.ForeColor = Color.Black;
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            try
            {
                statusLabel.Text = "Status: Submitting PIN";
                statusLabel.ForeColor = Color.Black;
                string pin = pinInput.Text;
                pin = pin.Replace(" ", ""); //remove spaces
                Console.WriteLine(pin);
                NestAPI.RequestToken(pin);
                statusLabel.Text = "Status: Success, close this window";
                statusLabel.ForeColor = Color.Green;
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Error in pin entry form.");
                statusLabel.Text = "Status: Failed, try again";
                statusLabel.ForeColor = Color.Red;
                webBrowser1.Navigate(NestAPI.GetPinUrl()); //go back to web page start
                pinInput.Text = ""; //clear pin input
            }
        }
    }
}
