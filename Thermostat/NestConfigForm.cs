using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thermostat
{
    public partial class NestConfigForm : Form
    {
        public NestConfigForm()
        {
            InitializeComponent();
            webBrowser1.Navigate(NestAPI.GetPinUrl()); //open the in-line browser to Nest's user auth page
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            try
            {
                string pin = pinInput.Text;
                Console.WriteLine(pin);

            }
            catch (Exception)
            {
                Console.Error.WriteLine("Error in pin entry form.");
            }
        }
    }
}
