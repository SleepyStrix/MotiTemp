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
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            NestAPI.GetNestData();
        }

        private NestConfigForm nestConfigForm;
        private void configButton_Click(object sender, EventArgs e)
        {
            if (nestConfigForm == null || nestConfigForm.IsDisposed)
            {
                nestConfigForm = new NestConfigForm();
            }
            nestConfigForm.Show();
        }
    }
}
