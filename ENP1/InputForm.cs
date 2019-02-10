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
        Data info = new Data();
        EncogAnalyst analyst = new EncogAnalyst();

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
                MessageBox.Show("The file you have selected is not in the correct format (.json) or does not meet the specified body for json files, make sure you have created at least one network in the network file creater and you are using the file generated from that (networks.json). If you would like to make your own json files, please copy the format of the networks.json file.", "File Access Error");
                return;
            }

            if (networkSaveDataList.Count == 0)
            {
                MessageBox.Show("This network file is empty, make sure you have selected the correct network file and have created at least one network.", "Empty Network File Error");
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
            if (networkSaveDataList.Count == 0)
            {
                MessageBox.Show("You must select a network file first. If you do not have one you can create one in the network creater window", "No Network File Error");
                return;
            }

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
            info = new Data();
            string dataFile = networkSaveDataList[selectedNetwork].CsvFile;
			string path = networkSaveDataList[selectedNetwork].Path;
			double[][] answers;

            if (!Directory.Exists(path + "normal"))
            {
                MessageBox.Show("You have no \"normal\" folder, please ensure that you are selecting the same csv as the one used in network creation. It may also be the case that you have moved the working directory of the csv file without also moving its dependant folders.", "File Access Error");
                return;
            }

            if (!Directory.Exists(path + "networks"))
            {
                MessageBox.Show("You have no \"networks\" folder, please ensure that you have created at least one network. It may also be the case that you have moved the working directory of the csv file without also moving its dependant folders.", "File Access Error");
                return;
            }

            //Load analyst from earlier.
            analyst = new EncogAnalyst();
            analyst.Load(new FileInfo(path + @"normal\normalizationData" + dataFile.Replace(".csv", ".ega")));

            var sourcefile = new FileInfo(path + dataFile);

            var norm = new AnalystNormalizeCSV();
            //norm.InputHeadings = networkSaveDataList[selectedNetwork].Headings.ToArray();
            network.Load(networkSaveDataList[selectedNetwork].NetworkFile);

            if (!csvBox.Checked)
			{
                string outString = "";

                for (int i = 0; i < items[selectedNetwork].Count; i++)
                {
                    outString += items[selectedNetwork][i] + ",";
                }

                outString += ",";

                outString.Remove(outString.Length - 1);

                if (File.Exists(path + dataFile.Replace(".csv", "Temp.csv")))
                {
                    File.Delete(path + dataFile.Replace(".csv", "Temp.csv"));
                }

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

                var inputFile = new FileInfo(path + dataFile.Replace(".csv", "Temp.csv"));
                var inputFileNorm = new FileInfo(path + @"normal\" + dataFile.Replace(".csv", "TempNormal.csv"));
                norm.Analyze(inputFile, true, CSVFormat.English, analyst);

                try
                {
                    norm.Normalize(inputFileNorm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "\n\nThe inputs you have entered do not correctly follow the current normalisation data. This may because you " +
                        "have entered values above or below the currently stored highs and lows for numbers. It is also possible you have " +
                        "entered a textual word that has not been used before. Finally it is possible that you are have not used the correct" +
                        "case for a word as they are case sensitive.", "Normalisation Failure");
                    return;
                }
                
                info = info.ReturnInfo(path + @"normal\" + dataFile.Replace(".csv", "TempNormal.csv"), networkSaveDataList[selectedNetwork].OutputHeadings, 0, true);
                answers = Data.CreateArray<double>(1, info.OutputNumber);
            }
			else
			{
                //Reset paths.
                path = null; dataFile = null;

                //Get csv path.
                openFileDialog1.Filter = "csv files (*.csv)|*.csv";
                DialogResult dialogResult = openFileDialog1.ShowDialog();

                //Exit function if file selection was cancelled.
                if (dialogResult == DialogResult.Cancel)
                {
                    output.Text += "Cancelling File Selection. . .\n";
                    return;
                }

                //Check the file is .csv format.
                if (!openFileDialog1.SafeFileName.EndsWith(".csv"))
                {
                    MessageBox.Show("The file you have selected is not in the correct format (.csv)", "File Access Error");
                    return;
                }

                //Setup paths from file.
                dataFile = openFileDialog1.SafeFileName;
                path = openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "");
                var sourceFile = new FileInfo(openFileDialog1.FileName);
                var normalFile = new FileInfo(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, @"normal\" + openFileDialog1.SafeFileName.Replace(".csv", "Normal.csv")));

                output.Text += "Loading File: " + dataFile + ". . .\n";

                Data.Normalise(sourceFile, normalFile, path, dataFile, networkSaveDataList[selectedNetwork].OutputHeadings.Count, false);

                norm.Analyze(sourceFile, true, CSVFormat.English, analyst);

                try
                {
                    norm.Normalize(normalFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "\n\nThe inputs you have entered do not correctly follow the current normalisation data. This may because you " +
                        "have entered values above or below the currently stored highs and lows for numbers. It is also possible you have " +
                        "entered a textual word that has not been used before. Finally it is possible that you are have not used the correct" +
                        "case for a word as they are case sensitive.", "Normalisation Failure");
                    return;
                }

                info = info.ReturnInfo(path + @"normal\" + dataFile.Replace(".csv", "Normal.csv"), networkSaveDataList[selectedNetwork].OutputHeadings, 0, true);

                answers = Data.CreateArray<double>(info.InputData.Length, info.InputData[0].Length);
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

            info.Prediction = answers;

            //Output answers to text box.
            output.Text += network.Display(answers, analyst, networkSaveDataList[selectedNetwork].OutputHeadings, path + @"normal\" + dataFile.Replace(".csv", "Normal.csv"));
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string dataFile = networkSaveDataList[selectedNetwork].CsvFile;
            string path = networkSaveDataList[selectedNetwork].Path;

            bool exists = true;

            if (!File.Exists(path + dataFile.Replace(".csv", "Saved.csv")))
            {
                exists = false;
            }

            using (var sw = new StreamWriter(path + dataFile.Replace(".csv", "Saved.csv"), true))
            {
                if (!exists)
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
                }

                string outString = "";

                List<int> countIn = new List<int>();
                List<int> countOut = new List<int>();

                for (int c = 0; c < networkSaveDataList[selectedNetwork].InputHeadings.Count; c++)
                {
                    countIn.Add(0);
                }

                for (int c = 0; c < networkSaveDataList[selectedNetwork].OutputHeadings.Count; c++)
                {
                    countOut.Add(0);
                }

                //Get length of CSV, Inputs and Outputs.
                using (var reader = new StreamReader(path + @"normal\" + dataFile.Replace(".csv", "Normal.csv")))
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    for (int v = 0; v < values.Length; v++)
                    {
                        for (int k = 0; k < networkSaveDataList[selectedNetwork].InputHeadings.Count; k++)
                        {
                            if (values[v].Contains(networkSaveDataList[selectedNetwork].InputHeadings[k]))
                            {
                                countIn[k]++;
                            }
                        }

                        for (int k = 0; k < networkSaveDataList[selectedNetwork].OutputHeadings.Count; k++)
                        {
                            if (values[v].Contains(networkSaveDataList[selectedNetwork].OutputHeadings[k]))
                            {
                                countOut[k]++;
                            }
                        }
                    }
                }

                for (int i = 0; i < info.InputData.Length; i++)
                {
                    for (int j = 0; j < countIn.Count; j++)
                    {
                        string prediction = "";

                        if (countIn[j] == 1)
                        {
                            try
                            {
                                double[] temp = new double[1];
                                temp[0] = info.InputData[i][j + countIn[j - 1] - 1];
                                prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - countIn.Count - countOut.Count].DetermineClass(temp).Name;
                            }
                            catch
                            {
                                prediction = Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - countIn.Count - countOut.Count].DeNormalize(info.InputData[i][j + countIn[j - 1] - 1]), 1).ToString();
                            }
                        }
                        else
                        {
                            double[] temp = new double[countIn[j]];

                            for (int c = 0; c < countIn[j]; c++)
                            {
                                temp[c] = info.InputData[i][c];
                            }

                            prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - countIn.Count - countOut.Count].DetermineClass(temp).Name;
                        }

                        outString += prediction + ",";
                    }

                    sw.Write(outString);
                    outString = "";

                    for (int j = 0; j < countOut.Count; j++)
                    {
                        string prediction = "";

                        if (countOut[j] == 1)
                        {
                            prediction = Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - countOut.Count].DeNormalize(info.Prediction[i][j]), 1).ToString();
                        }
                        else
                        {
                            double[] temp = new double[countOut[j]];

                            for (int c = 0; c < countOut[j]; c++)
                            {
                                temp[c] = info.Prediction[i][c];
                            }

                            prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - countOut.Count].DetermineClass(temp).Name;
                        }

                        outString += prediction + ",";
                    }

                    sw.WriteLine(outString);
                }
            }
        }
    }
}
