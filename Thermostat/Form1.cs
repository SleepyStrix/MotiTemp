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
using Newtonsoft.Json;

namespace Thermostat
{
    public partial class Form1 : Form
    {
        private TimeStampedTimer updateTimer;
        public Form1()
        {
            InitializeComponent();
            updateTimer = new TimeStampedTimer(5 * 60 * 1000, false);
            updateTimer.Start();
            updateTimer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateTimer_Tick);
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

        List<Button> structureButtons = new List<Button>();
        public void SetupForm()
        {
            try
            {
                NestRootModel nestRoot = NestAPI.GetNestData();
                SettingCollection.LoadSettings();
                foreach (StructureModel structure in nestRoot.structures.Values)
                {
                    //create button for each home
                    Button button = new Button();
                    button.Text = structure.name;
                    button.Font = new Font(button.Font.FontFamily, 16);
                    button.Size = new System.Drawing.Size(220, 60);

                    button.Tag = structure.structure_id;
                    button.Click += new EventHandler(StructureButton_Click);
                    structuresPanel.Controls.Add(button);
                    structureButtons.Add(button);
                }
                if (string.IsNullOrWhiteSpace(selected_structure_id))
                {
                    SelectStructure(nestRoot.structures.Values.First().structure_id);
                }
                else
                {
                    SelectStructure(selected_structure_id); //reselect selected structure
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private string selected_structure_id = "";
        private void SelectStructure(string structure_id)
        {
            foreach (Button b in structureButtons)
            {
                if (b.Tag.ToString().Equals(structure_id))
                {
                    b.BackColor = Color.Green;
                }
                else
                {
                    b.BackColor = Color.Gray;
                }
            }
            selected_structure_id = structure_id;
            StructureSetting selected = SettingCollection.GetStructureSetting(selected_structure_id);
            if (selected != null)
            {
                onButton.Checked = selected.on;
                offButton.Checked = !selected.on;
                motionTimeInput.Value = (decimal)selected.minutesUntilInactive;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupForm();
        }

        private void StructureButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Console.WriteLine(button.Text + " button clicked");
            SelectStructure(button.Tag.ToString());
        }

        private void UpdateTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            Program.localConsole.WriteLine("Update timer tick");
            NestMain.UpdateThemostatModes();
            //SetupForm();
        }

        public void SetStatus(string status, Color color)
        {
            Console.WriteLine(status);
            statusLabel.Text = status;
            statusLabel.ForeColor = color;
        }

        private void onButton_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Checked change on");
            StructureSetting s = SettingCollection.GetStructureSetting(selected_structure_id);
            if (s != null)
            {
                s.on = ((RadioButton)sender).Checked;
                SettingCollection.SaveSettings();
            }
        }

        private void offButton_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Checked change off");
            StructureSetting s = SettingCollection.GetStructureSetting(selected_structure_id);
            if (s != null)
            {
                s.on = !((RadioButton)sender).Checked;
                SettingCollection.SaveSettings();
            }
        }

        private void motionTimeInput_ValueChanged(object sender, EventArgs e)
        {
            StructureSetting s = SettingCollection.GetStructureSetting(selected_structure_id);
            if (s != null)
            {
                s.minutesUntilInactive = (int)motionTimeInput.Value;
            }
        }
    }
}
