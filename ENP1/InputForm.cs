﻿using Encog.App.Analyst;
using Encog.App.Analyst.CSV.Normalize;
using Encog.Util.CSV;
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
        private readonly List<List<string>> items = new List<List<string>>();
        private readonly List<NetworkSaveData> networkSaveDataList = new List<NetworkSaveData>();
        private readonly List<List<bool>> notIncluded = new List<List<bool>>();
        private int selectedNetwork;
        private Data info = new Data();
        private EncogAnalyst analyst = new EncogAnalyst();
        private bool csvInput = false;

        private void SetPanel(Panel panel)
        {
            foreach (Control p in Controls)
            {
                if (p is Panel)
                {
                    if (panel == p)
                    {
                        ClientSize = p.Size;
                        p.Visible = true;
                        p.Location = new System.Drawing.Point(0, 0);
                        backBtn.Location = new System.Drawing.Point(10, p.Height - backBtn.Height - 10);
                    }
                    else
                    {
                        p.Visible = false;
                    }
                }
            }
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            SetPanel(panel1);
        }

        /// <summary> Select json for stored networks. </summary>
        private void FileBtn_Click(object sender, EventArgs e)
        {
            networkSaveDataList.Clear(); items.Clear(); inputListBox.Items.Clear(); networkListBox.Items.Clear();

            //Get csv path and create normalized version path.
            openFileDialog1.Filter = "json files (*.json)|*.json";
            DialogResult dialogResult = openFileDialog1.ShowDialog();

            //Exit function if file selection was cancelled.
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            //Check the file is .csv format.
            if (!openFileDialog1.SafeFileName.EndsWith(".json"))
            {
                MessageBox.Show("The file you have selected is not in the correct format (.json)", "File Access Error");
                return;
            }

            if (Data.IsFileLocked(new FileInfo(openFileDialog1.FileName), false))
            {
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
                    vs.Add("");
                }

                items.Add(new List<string>(vs));
            }

            //Populate lists with headings.
            foreach (string item in networkSaveDataList[selectedNetwork].InputHeadings)
            {
                inputListBox.Items.Add(item);
            }

            fileLbl.Text = "Loaded File: " + networkSaveDataList[selectedNetwork].Name;

            inputListBox.SelectedIndex = 0;
            networkListBox.SelectedIndex = 0;

            SetPanel(panel2);
        }

		/// <summary> Save input for each heading in each saved network. </summary>
        private void InputTxt_TextChanged(object sender, EventArgs e)
        {
            if (networkSaveDataList.Count > 0)
            {
                items[selectedNetwork][inputListBox.SelectedIndex] = inputTxt.Text;
            }
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

            notIncluded.Clear();

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

            var normalisationData = new FileInfo(path + @"normal\normalizationData" + dataFile.Replace(".csv", ".ega"));

            if (Data.IsFileLocked(normalisationData, false))
            {
                return;
            }

            analyst.Load(normalisationData);

            FileInfo sourcefile = new FileInfo(path + dataFile);

            if (Data.IsFileLocked(sourcefile, false))
            {
                return;
            }

            AnalystNormalizeCSV norm = new AnalystNormalizeCSV();

            if (Data.IsFileLocked(new FileInfo(networkSaveDataList[selectedNetwork].NetworkFile), false))
            {
                return;
            }

            network.Load(networkSaveDataList[selectedNetwork].NetworkFile);

            List<bool> vb = new List<bool>();

            foreach (string heading in networkSaveDataList[selectedNetwork].InputHeadings)
            {
                vb.Add(false);
            }

            notIncluded.Add(vb);

            if (!csvInput)
            {
                string outString = "";

                for (int i = 0; i < items[selectedNetwork].Count; i++)
                {
                    notIncluded[0][i] = string.IsNullOrWhiteSpace(items[selectedNetwork][i]);

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
                            outString += ",";
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

                if (inputFile.Exists)
                {
                    if (Data.IsFileLocked(inputFile, false))
                    {
                        return;
                    }
                }

                norm.Analyze(inputFile, true, CSVFormat.English, analyst);

                if (inputFileNorm.Exists)
                {
                    if (Data.IsFileLocked(inputFileNorm, true))
                    {
                        return;
                    }
                }

                try
                {
                    norm.Normalize(inputFileNorm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\nThe inputs you have entered do not correctly follow the current normalisation data. This may because you " +
                        "have entered values above or below the currently stored highs and lows for numbers. It is also possible you have " +
                        "entered a textual word that has not been used before. Finally it is possible that you are have not used the correct " +
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
                    SetPanel(panel3);
                    return;
                }

                //Check the file is .csv format.
                if (!openFileDialog1.SafeFileName.EndsWith(".csv"))
                {
                    MessageBox.Show("The file you have selected is not in the correct format (.csv)", "File Access Error");
                    SetPanel(panel3);
                    return;
                }

                //Setup paths from file.
                dataFile = openFileDialog1.SafeFileName;
                path = openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "");
                var inputFile = new FileInfo(openFileDialog1.FileName);
                var inputFileNorm = new FileInfo(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, @"normal\" + openFileDialog1.SafeFileName.Replace(".csv", "Normal.csv")));

                if (Data.IsFileLocked(inputFile, false))
                {
                    return;
                }

                if (inputFileNorm.Exists)
                {
                    if (Data.IsFileLocked(inputFileNorm, true))
                    {
                        return;
                    }
                }

                output.Text += "Loading File: " + dataFile + ". . .\n";

                norm.Analyze(inputFile, true, CSVFormat.English, analyst);

                try
                {
                    norm.Normalize(inputFileNorm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\nThe inputs you have entered do not correctly follow the current normalisation data. This may because you " +
                        "have entered values above or below the currently stored highs and lows for numbers. It is also possible you have " +
                        "entered a textual word that has not been used before. Finally it is possible that you are have not used the correct " +
                        "case for a word as they are case sensitive.", "Normalisation Failure");

                    SetPanel(panel3);
                    return;
                }

                info = info.ReturnInfo(path + @"normal\" + dataFile.Replace(".csv", "Normal.csv"), networkSaveDataList[selectedNetwork].OutputHeadings, 0, true);

                answers = Data.CreateArray<double>(info.InputData.Length, networkSaveDataList[selectedNetwork].OutputHeadings.Count);

                //Get length of CSV, Inputs and Outputs.
                using (var reader = new StreamReader(openFileDialog1.FileName))
                {
                    reader.ReadLine();
                    int lineNo = 0;

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (lineNo > 0)
                        {
                            notIncluded.Add(new List<bool>(vb));
                        }

                        for (int i = 0; i < notIncluded[0].Count; i++)
                        {
                            notIncluded[lineNo][i] = string.IsNullOrWhiteSpace(values[i]);
                        }

                        lineNo++;
                    }
                }
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

            SetPanel(panel5);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (networkSaveDataList.Count == 0)
            {
                MessageBox.Show("You must select a network file first. If you do not have one you can create one in the network creater window", "No Network File Error");
                return;
            }

            if (info.InputData == null)
            {
                MessageBox.Show("You must predict an output before you try to save the results", "No Predicted Output.");
                return;
            }

            string dataFile = networkSaveDataList[selectedNetwork].CsvFile;
            string path = networkSaveDataList[selectedNetwork].Path;

            bool exists = true;

            //Checked for existing before using stream writer as the stream writer will create the file if it does not exist.
            if (!File.Exists(path + dataFile.Replace(".csv", "Saved.csv")))
            {
                exists = false;
            }

            if (new FileInfo(path + dataFile.Replace(".csv", "Saved.csv")).Exists)
            {
                if (Data.IsFileLocked(new FileInfo(path + dataFile.Replace(".csv", "Saved.csv")), true))
                {
                    return;
                }
            }

            if (new FileInfo(path + @"normal\" + dataFile.Replace(".csv", "Normal.csv")).Exists)
            {
                if (Data.IsFileLocked(new FileInfo(path + @"normal\" + dataFile.Replace(".csv", "Normal.csv")), false))
                {
                    return;
                }
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
                    int valuesPassed = 0;

                    for (int j = 0; j < countIn.Count; j++)
                    {
                        string prediction = "";

                        if (!notIncluded[i][j])
                        {
                            if (countIn[j] == 1)
                            {
                                try
                                {
                                    double[] temp = new double[1];
                                    temp[0] = info.InputData[i][valuesPassed];
                                    prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - countIn.Count - countOut.Count].DetermineClass(temp).Name;
                                }
                                catch
                                {
                                    prediction = Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - countIn.Count - countOut.Count].DeNormalize(info.InputData[i][valuesPassed]), 1).ToString();
                                }
                            }
                            else
                            {
                                double[] temp = new double[countIn[j]];

                                for (int c = 0; c < countIn[j]; c++)
                                {
                                    temp[c] = info.InputData[i][c + valuesPassed];
                                }

                                prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - countIn.Count - countOut.Count].DetermineClass(temp).Name;
                            }
                        }

                        outString += prediction + ",";
                        valuesPassed += countIn[j];
                    }

                    sw.Write(outString);
                    outString = "";
                    valuesPassed = 0;

                    for (int j = 0; j < countOut.Count; j++)
                    {
                        string prediction = "";

                        if (!notIncluded[i][j])
                        {
                            if (countOut[j] == 1)
                            {
                                try
                                {
                                    double[] temp = new double[1];
                                    temp[0] = info.Prediction[i][valuesPassed];
                                    prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - countOut.Count].DetermineClass(temp).Name;
                                }
                                catch
                                {
                                    prediction = Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - countOut.Count].DeNormalize(info.Prediction[i][valuesPassed]), 1).ToString();
                                }
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
                        }

                        outString += prediction + ",";
                        valuesPassed += countOut[j];
                    }

                    sw.WriteLine(outString);
                    outString = "";
                }
            }

            output.Text += "\n\n Prediction saved.";
        }

        private void NetworkSelectedBtn_Click(object sender, EventArgs e)
        {
            SetPanel(panel3);
        }

        private void CsvBtn_Click(object sender, EventArgs e)
        {
            csvInput = true;
            NetworkBtn_Click(sender, e);
        }

        private void ManualBtn_Click(object sender, EventArgs e)
        {
            csvInput = false;
            SetPanel(panel4);
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                Close();
            }
            else if (panel2.Visible)
            {
                SetPanel(panel1);
            }
            else if (panel3.Visible)
            {
                SetPanel(panel2);
            }
            else if (panel4.Visible)
            {
                SetPanel(panel3);
            }
            else if (panel5.Visible && csvInput == true)
            {
                SetPanel(panel3);
            }
            else
            {
                SetPanel(panel4);
            }
        }
    }
}
