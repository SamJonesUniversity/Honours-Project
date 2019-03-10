using Encog.App.Analyst;
using Encog.App.Analyst.CSV.Normalize;
using Encog.App.Analyst.Missing;
using Encog.App.Analyst.Script.Normalize;
using Encog.App.Analyst.Wizard;
using Encog.Util.CSV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ENP1
{
    /// <summary>
    /// Data class used to load and validate CSV files.
    /// This data is then stored as a data object.
    /// </summary>
    internal class Data
    {
        /// <summary> Training data input array. </summary>
        public double[][] InputData { get; set; }

        /// <summary> Training data output array. </summary>
        public double[][] OutputData { get; set; }

        /// <summary> Training data output array. </summary>
        public double[][] Prediction { get; set; }

        /// <summary> Sample data input array. </summary>
        public double[][] InputDataSample { get; set; }

        /// <summary> Sample data output array. </summary>
        public double[][] OutputDataSample { get; set; }

        ///<summary> Count of inputs </summary>
        public int InputNumber;

        ///<summary> Count of outputs </summary>
        public int OutputNumber;

        ///<summary> Empty constuctor </summary>
        public Data() { }

        ///<summary> Dynamically creates arrays of size (rows, columns) </summary>
        public static T[][] CreateArray<T>(int rows, int columns)
        {
            T[][] array = new T[rows][];

            for(int i = 0; i < rows; i++)
            {
                array[i] = new T[columns];
            }

            return array;
        }

        /// <summary> Removes a set number of arrays from a 2D array. </summary>
        public static T[][] RemoveFromArray<T>(T[][] input, int[] index, int count)
        {
            List<List<T>> tmp = new List<List<T>>();

            for (int i = 0; i < input.Length; i++)
            {
                tmp.Add(new List<T>(input[i]));
            }

            for (int i = 0; i < count; i++)
            {
                int id = index[i] - i;
                if(id < 0)
                {
                    id = 0;
                }

                tmp.RemoveAt(id);
            }

            T[][] array = Data.CreateArray<T>(tmp.Count, tmp[0].Count);

            int j = 0;
            foreach (List<T> entry in tmp)
            {
                array[j] = entry.ToArray();
                j++;
            }

            return array;
        }

        /// <summary> Checks to see if file is open in another program. </summary>
        public static bool IsFileLocked(FileInfo file, bool write)
        {
            FileStream stream = null;

            try
            {
                if (!write)
                {
                    stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                }
                else
                {
                    stream = file.Open(FileMode.Open, FileAccess.Write, FileShare.None);
                }
            }
            catch (IOException exception)
            {
                //File is locked.
                MessageBox.Show(exception.Message, "File Access Error");
                return true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            //file is not locked.
            return false;
        }

        /// <summary> need comments </summary>
        public static List<string> Normalise(FileInfo sourceFile, FileInfo normalFile, string path, string dataFile, int outputs)
        {
            List<string> titles = new List<string>();

            //Setup analyst with orignal csv.
            var analyst = new EncogAnalyst();
            var wizard = new AnalystWizard(analyst);

            //Additional validation to check that the file is not empty.
            try
            {
                wizard.Wizard(sourceFile, true, AnalystFileFormat.DecpntComma);
            }
            catch (Exception)
            {
                MessageBox.Show("The file you have selected is empty.", "File Access Error");
                return null;
            }

            if (analyst.Script.Fields.Length - 1 < outputs)
            {
                MessageBox.Show("You have specified " + outputs + " outputs but there are only " + analyst.Script.Fields.Length + " headings in the file.", "Too Many Outputs Error");
                return null;
            }

            //Setup max and min range for normalization.
            foreach (AnalystField field in analyst.Script.Normalize.NormalizedFields)
            {
                field.NormalizedHigh = 1;
                field.NormalizedLow = 0;
                //field.Action = Encog.Util.Arrayutil.NormalizationAction.OneOf; //Use this to change normalizaiton type.
            }

            analyst.Script.Normalize.MissingValues = new NegateMissing();
            //Normalization.
            var norm = new AnalystNormalizeCSV();
            norm.Analyze(sourceFile, true, CSVFormat.English, analyst);
            norm.ProduceOutputHeaders = true;

            try
            {
                norm.Normalize(normalFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnknown application failure, please report this bug with a screenshot of" +
                    " the message to the relevant engineer.", "Normalisation Failure");
                return null;
            }
            
            for (int i = outputs; i + analyst.Script.Fields.Length > analyst.Script.Fields.Length; i--)
            {
                titles.Add(analyst.Script.Fields[analyst.Script.Fields.Length - i].Name);
            }

            //Save configuration to be used later.
            analyst.Save(new FileInfo(path + @"\normal\" + "normalizationData" + dataFile.Replace(".csv", ".ega")));

            return titles;
        }

        ///<summary> Read and store data from csv (File path, Column titles, Sample Percent, Validation type) </summary>
        public Data ReturnInfo(string path, List<string> titles, int sampleNumber, bool validation)
        {
            if (IsFileLocked(new FileInfo(path), false))
            {
                return null;
            }

            int csvLength = 0;
            int inputNumber = 0, outputNumber = 0;

            //Get length of CSV, Inputs and Outputs.
            using (var reader = new StreamReader(path))
            {
                if (csvLength == 0)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    for (int v = 0; v < values.Length; v++)
                    {
                        bool updated = false;

                        for (int k = 0; k < titles.Count; k++)
                        {
                            if (values[v].Contains(titles[k]))
                            {
                                outputNumber++;
                                updated = true;
                            }
                        }

                        if (!updated)
                        {
                            inputNumber++;
                        }
                    }
                }

                csvLength++;

                while (!reader.EndOfStream)
                {
                    reader.ReadLine();
                    csvLength++;
                }
            }

            //Logic for allocating the correct amount of sample data.
            bool passed = false;
            int numOfSamples = 0;
            int sampleCount = 1;

            if (!validation)
            {
                if (csvLength - 1 <= 1)
                {
                    MessageBox.Show("The file you have selected does not have enough rows to use a sample size.", "Input Size Error");
                    return null;
                }
                else
                {
                    decimal tmpSampleNumber = csvLength * decimal.Divide(sampleNumber, 100); //(csvLength - 1) / (((csvLength - 1) / 10) * (sampleNumber + 1)) + 1;
                    sampleNumber = (int)tmpSampleNumber;
                }

                while ((csvLength - 1) % sampleNumber > (csvLength - 1) / sampleNumber)
                {
                    sampleNumber--;
                }
            }

            //Local InputData and OutputData
            double[][] inputData = CreateArray<double>(0, 0);
            double[][] outputData = CreateArray<double>(0, 0);

            //Local InputDataSample and OutputDataSample
            double[][] inputDataSample = CreateArray<double>(sampleNumber, inputNumber);
            double[][] outputDataSample = CreateArray<double>(sampleNumber, outputNumber);

            if (validation)
            {
                inputDataSample = null;
                outputDataSample = null;

                inputData = CreateArray<double>(csvLength - 1, inputNumber);
                outputData = CreateArray<double>(csvLength - 1, outputNumber);
            }
            else
            {
                inputData = CreateArray<double>(csvLength - 1 - sampleNumber, inputNumber);
                outputData = CreateArray<double>(csvLength - 1 - sampleNumber, outputNumber);

                sampleNumber = (csvLength - 1) / sampleNumber;
            }

            //Read CSV and assign data to arrays.
            using (var reader = new StreamReader(path))
            {
                int i = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    //Skips first line with headings.
                    if (i >= 1)
                    {
                        //Input array assigning.
                        for (int j = 0; j < inputNumber; j++)
                        {
                            if (sampleCount == sampleNumber && inputDataSample != null && numOfSamples < inputDataSample.Length)
                            {
                                inputDataSample[numOfSamples][j] = Convert.ToDouble(values[j]);
                                passed = true;
                            }
                            else
                            {
                                inputData[i - 1 - numOfSamples][j] = Convert.ToDouble(values[j]);
                            }
                        }

                        //Output array assigning.
                        for (int j = 0; j < outputNumber; j++)
                        {
                            if (sampleCount == sampleNumber && outputDataSample != null && numOfSamples < inputDataSample.Length)
                            {
                                outputDataSample[numOfSamples][j] = Convert.ToDouble(values[inputNumber + j]);
                                passed = true;
                            }
                            else
                            {
                                outputData[i - 1 - numOfSamples][j] = Convert.ToDouble(values[inputNumber + j]);
                            }
                        }

                        //Reset logic.
                        if (passed)
                        {
                            sampleCount = 0;
                            numOfSamples++;
                            passed = false;
                        }

                        sampleCount++;
                    }

                    i++;
                }
            }

            Data info = new Data
            {
                InputData = inputData,
                OutputData = outputData,
                InputDataSample = inputDataSample,
                OutputDataSample = outputDataSample,
                InputNumber = inputNumber,
                OutputNumber = outputNumber
            };

            return info;
        }
    }
}
