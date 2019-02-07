using Encog.App.Analyst;
using Encog.App.Analyst.CSV.Normalize;
using Encog.App.Analyst.Script;
using Encog.Util.CSV;
using Encog.Util.Normalize;
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
        List<NetworkSaveData> networkSaveDataList = new List<NetworkSaveData>();
        int selectedNetwork = 0;

		/// <summary> Select json for stored networks. </summary>
        private void FileBtn_Click(object sender, EventArgs e)
        {
            networkSaveDataList.Clear(); items.Clear(); inputListBox.Items.Clear(); networkListBox.Items.Clear();

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

			//Try reading file.
            try
            {
                using (var sr = new StreamReader(openFileDialog1.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        networkSaveDataList.Add(JsonConvert.DeserializeObject<NetworkSaveData>(sr.ReadLine()));
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("The file you have selected is not in the correct format (.json)", "File Access Error");
                return;
            }

			//Populate lists with networks and fill items with lists of nulls.
            foreach (NetworkSaveData item in networkSaveDataList)
            {
                networkListBox.Items.Add(item.Name);

                List<string> vs = new List<string>();

                foreach (string heading in item.InputHeadings)
                {
                    vs.Add(null);
                }

                items.Add(vs);
            }

			//Populate lists with headings.
            foreach (string item in networkSaveDataList[selectedNetwork].InputHeadings)
            {
                inputListBox.Items.Add(item);
            }

            fileLbl.Text = "Loaded File: " + networkSaveDataList[selectedNetwork].Name;

            inputListBox.SelectedIndex = 0;
            networkListBox.SelectedIndex = 0;
        }

		/// <summary> Save input for each heading in each saved network. </summary>
        private void InputTxt_TextChanged(object sender, EventArgs e)
        {
            items[selectedNetwork][inputListBox.SelectedIndex] = inputTxt.Text;
        }

		/// <summary> Display stored input for each heading in each saved network. </summary>
        private void InputListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputTxt.Text = items[selectedNetwork][inputListBox.SelectedIndex];
            inputTxt.Select();
        }

		/// <summary> Display all saved networks and thier corresponding inputs. </summary>
        private void NetworkListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedNetwork = networkListBox.SelectedIndex;

            inputListBox.Items.Clear();

            foreach (string item in networkSaveDataList[selectedNetwork].InputHeadings)
            {
                inputListBox.Items.Add(item);
            }

            inputListBox.SelectedIndex = 0;
        }

		/// <summary> Predict network outputs. </summary>
        private void NetworkBtn_Click(object sender, EventArgs e)
        {
			//Setup network.
			NeuralNetwork network;
			
			//Switch to set network type based on type stored in json file.
            switch (networkSaveDataList[selectedNetwork].NetworkType)
            {
                case "EncogNeuralNetwork":
                {
                    network = new EncogNeuralNetwork();
					break;
                }
                case "EncogDeepNeuralNetwork":
                {
                    network = new EncogDeepNeuralNetwork();
					break;
                }
                case "AccordNeuralNetwork":
                {
                    network = new AccordNeuralNetwork();
					break;
                }
                default:
                {
                    network = new AccordDeepNeuralNetwork();
					break;
                }
            }
			
			//Initialize variables
			Data info = new Data();
			string dataFile = networkSaveDataList[selectedNetwork].CsvFile;
			string path = networkSaveDataList[selectedNetwork].Path;
			double[][] answers;

            //Load analyst from earlier.
            var analyst = new EncogAnalyst();
            analyst.Load(new FileInfo(path + @"\normalizationData" + dataFile.Replace(".csv", ".ega")));

            var sourcefile = new FileInfo(path + dataFile);

            var norm = new AnalystNormalizeCSV();
            //norm.InputHeadings = networkSaveDataList[selectedNetwork].Headings.ToArray();
            network.Load(networkSaveDataList[selectedNetwork].NetworkFile);

            if (!csvBox.Checked)
			{
                string outString = "";

                for (int i = 0; i < items[selectedNetwork].Count; i++)
                {
                    if (i != items[selectedNetwork].Count - 1)
                    {
                        outString += items[selectedNetwork][i] + ",";
                    }
                    else
                    {
                        outString += items[selectedNetwork][i];
                    }
                }

                outString.Remove(outString.Length - 1);

                if (File.Exists(path + dataFile.Replace(".csv", "Temp.csv")))
                {
                    using (var sw = new StreamWriter(path + dataFile.Replace(".csv","Temp.csv"), true))
                    {
                        sw.WriteLine(outString);
                    }
                }
                else
                {
                    using (var sw = new StreamWriter(path + dataFile.Replace(".csv", "Temp.csv"), true))
                    {
                        for (int i = 0; i < networkSaveDataList[selectedNetwork].InputHeadings.Count; i++)
                        {
                            sw.Write(networkSaveDataList[selectedNetwork].InputHeadings[i] + ",");
                        }

                        for (int i = 0; i < networkSaveDataList[selectedNetwork].OutputHeadings.Count; i++)
                        {
                            if (i != networkSaveDataList[selectedNetwork].OutputHeadings.Count - 1)
                            {
                                sw.Write(networkSaveDataList[selectedNetwork].OutputHeadings[i] + ",");
                            }
                            else
                            {
                                sw.Write(networkSaveDataList[selectedNetwork].OutputHeadings[i]);
                            }
                        }

                        sw.WriteLine();
                        sw.WriteLine(outString);
                    }
                }

                var inputFile = new FileInfo(path + dataFile.Replace(".csv", "Temp.csv"));
                var inputFileNorm = new FileInfo(path + dataFile.Replace(".csv", "TempNormal.csv"));
                norm.Analyze(inputFile, true, CSVFormat.English, analyst);
                norm.Normalize(inputFileNorm);
                info = info.ReturnInfo(path + dataFile.Replace(".csv", "TempNormal.csv"), networkSaveDataList[selectedNetwork].OutputHeadings, 1, true);
                answers = Data.CreateArray<double>(1, info.OutputNumber);
            }
			else
			{
				//Get file path and name.
				//Data.GetFile(false, ref path, ref dataFile, 1);
				
				//Setup training dataset.
				//info = info.ReturnInfo(path + dataFile.Replace(".csv", "Normal.csv"), outputTitles, network.Outputs.Length, validation);
				
				//
				//if (info.InputData[0].Length > items[selectedNetwork].Count)
				{
					//MessageBox.Show("The file you have selected does not have to correct number of inputs/outputs, please make sure you are using the correct network or input/output parameters.", "Input/Output Size Error");
					//return;
				}

                var inputFile = new FileInfo(path + dataFile.Replace(".csv", "Temp.csv"));
                var inputFileNorm = new FileInfo(path + dataFile.Replace(".csv", "TempNormal.csv"));
                norm.Analyze(inputFile, true, CSVFormat.English, analyst);
                norm.Normalize(inputFileNorm);
                info = info.ReturnInfo(path + dataFile.Replace(".csv", "TempNormal.csv"), networkSaveDataList[selectedNetwork].OutputHeadings, 1, true);
                answers = Data.CreateArray<double>(1, info.OutputNumber);
            }
			
			//Compute network predictions.
			for (int i = 0; i < answers.Length; i++)
			{
                //Switch to set network type based on type stored in json file.
                switch (networkSaveDataList[selectedNetwork].NetworkType)
                {
                    case "AccordNeuralNetwork":
                    {
                        answers[i] = network.AccordNetwork.Compute(info.InputData[i]);
                        break;
                    }
                    case "AccordDeepNeuralNetwork":
                    {
                        answers[i] = network.DeepAccordNetwork.Compute(info.InputData[i]);
                        break;
                    }
                    default:
                    {
                        network.EncogNetwork.Compute(info.InputData[i], answers[i]);
                        break;
                    }
                }
            }

            //Output answers to text box.
            output.Text += network.Display(answers, analyst, networkSaveDataList[selectedNetwork].OutputHeadings, path + dataFile.Replace(".csv", "TempNormal.csv"));
        }
    }
}
