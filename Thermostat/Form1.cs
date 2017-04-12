using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thermostat.NestModels;

namespace Thermostat
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            //try 
            //{
                SetupForm();  
            //}
            //catch (Exception e)
            //{
                //throw e;
            //
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

        private void SetupForm()
        {
            NestRootModel nestRoot = NestAPI.GetNestData();
            //test setting thermostat to 60F
            ThermostatModel device = nestRoot.devices.thermostats.First().Value;
            Console.WriteLine(device.device_id);
            NestAPI.SetTargetTemperature(device, 60);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void motionTimeInput_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
