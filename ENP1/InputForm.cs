using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ENP1
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }

        //Global path variable for working directory.
        List<List<string>> items = new List<List<string>>();
        List<NetworkSaveData> network = new List<NetworkSaveData>();
        int selectedNetwork = 0;

        private void FileBtn_Click(object sender, EventArgs e)
        {
            network.Clear(); items.Clear(); inputListBox.Items.Clear(); networkListBox.Items.Clear();

            //Get csv path and create normalized version path.
            openFileDialog1.Filter = "csv files (*.json)|*.json";
            DialogResult dialogResult = openFileDialog1.ShowDialog();

            //Exit function if file selection was cancelled.
            if (dialogResult == DialogResult.Cancel)
            {
                //Output.Text += "Cancelling File Selection. . .\n";
                return;
            }

            //Check the file is .csv format.
            if (!openFileDialog1.SafeFileName.EndsWith(".json"))
            {
                MessageBox.Show("The file you have selected is not in the correct format (.json)", "File Access Error");
                return;
            }

            try
            {
                using (var sr = new StreamReader(openFileDialog1.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        network.Add(JsonConvert.DeserializeObject<NetworkSaveData>(sr.ReadLine()));
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("The file you have selected is not in the correct format (.json)", "File Access Error");
                return;
            }

            foreach (NetworkSaveData item in network)
            {
                networkListBox.Items.Add(item.Name);

                List<string> vs = new List<string>();

                foreach (string heading in item.Headings)
                {
                    vs.Add(null);
                }

                items.Add(vs);
            }

            foreach (string item in network[selectedNetwork].Headings)
            {
                inputListBox.Items.Add(item);
            }

            fileLbl.Text = "Loaded File: " + network[selectedNetwork].Name;

            inputListBox.SelectedIndex = 0;
            networkListBox.SelectedIndex = 0;
        }

        private void InputTxt_TextChanged(object sender, EventArgs e)
        {
            items[selectedNetwork][inputListBox.SelectedIndex] = inputTxt.Text;
        }

        private void InputListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputTxt.Text = items[selectedNetwork][inputListBox.SelectedIndex];
        }

        private void NetworkListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedNetwork = networkListBox.SelectedIndex;

            inputListBox.Items.Clear();

            foreach (string item in network[selectedNetwork].Headings)
            {
                inputListBox.Items.Add(item);
            }

            inputListBox.SelectedIndex = 0;
        }

        private void networkBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
