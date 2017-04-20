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

        List<StructureSetting> structureSettings;

        private void SetupForm()
        {
            NestRootModel nestRoot = NestAPI.GetNestData();

            structureSettings = JsonConvert.DeserializeObject<List<StructureSetting>>(Properties.Settings.Default.SettingsJSON);
            if (structureSettings == null)
            {
               structureSettings = new List<StructureSetting>();
            }
            foreach (StructureModel structure in nestRoot.structures.Values)
            {
                Program.utcConsole.WriteLine("zoop");
                //create button for each home
                Button button = new Button();
                button.Text = structure.name;
                button.Tag = structure.structure_id;
                button.Size = new System.Drawing.Size(180, 60);
                button.Click += new EventHandler(StructureButton_Click);
                structuresPanel.Controls.Add(button);

                bool found = false;
                foreach (StructureSetting s in structureSettings)
                {
                    if (s.structure_id.Equals(structure.structure_id))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    structureSettings.Add(new StructureSetting(structure.structure_id));
                }
            }
            if (string.IsNullOrWhiteSpace(selected_structure_id))
            {
                Program.utcConsole.WriteLine("dooot");
                SelectStructure(nestRoot.structures.Values.First().structure_id);
            }
            
        }

        private string selected_structure_id = "";
        private void SelectStructure(string structure_id)
        {
            Program.utcConsole.WriteLine("yaaa");
            selected_structure_id = structure_id;
            foreach (StructureSetting s in structureSettings)
            {
                if (s.structure_id.Equals(structure_id))
                {
                    Program.utcConsole.WriteLine("yeet");
                    onButton.Checked = s.on;
                    offButton.Checked = !s.on;
                    motionTimeInput.Value = (decimal)s.minutesUntilInactive;
                }
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
        }

        private void UpdateTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            Program.utcConsole.WriteLine("Update timer tick");
            NestMain.UpdateThemostatModes();
            //SetupForm();
        }

        private void onButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void offButton_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
