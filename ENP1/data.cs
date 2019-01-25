using System;
using System.Collections.Generic;
using System.IO;

namespace ENP1
{
    /// <summary>
    /// Data class used to load and validate CSV files.
    /// This data is then stored as a data object.
    /// </summary>
    class data
    {
        /// <summary> Training data input array. </summary>
        public double[][] InputData { get; set; }
        /// <summary> Training data output array. </summary>
        public double[][] OutputData { get; set; }

        /// <summary> Sample data input array. </summary>
        public double[][] InputDataSample { get; set; }
        /// <summary> Sample data output array. </summary>
        public double[][] OutputDataSample { get; set; }

        ///<summary> Count of inputs </summary>
        public int InputNumber;
        ///<summary> Count of outputs </summary>
        public int OutputNumber;

        ///<summary> Empty constuctor </summary>
        public data() { }

        ///<summary> Dynamically creates arrays of size (rows, columns) </summary>
        static T[][] CreateArray<T>(int rows, int columns)
        {
            T[][] array = new T[rows][];

            for(int i = 0; i < rows; i++)
            {
                array[i] = new T[columns];
            }

            return array;
        }

        ///<summary> Read and store data from csv (File path, Column titles, Sample Percent) </summary>
        public data return_info(string path, List<string> titles, int sampleNumber)
        {
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
                            }
                            else if (!updated)
                            {
                                inputNumber++;
                                updated = true;
                            }
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
            int sampleCount = 0;
            sampleNumber = (csvLength - 1) / (((csvLength - 1) / 10) * (sampleNumber + 1)) + 1;

            //Local InputData and OutputData
            double[][] inputData = CreateArray<double>(csvLength - 1, inputNumber);
            double[][] outputData = CreateArray<double>(csvLength - 1, outputNumber);

            //Local InputDataSample and OutputDataSample
            double[][] inputDataSample = CreateArray<double>((csvLength - 1) / sampleNumber, inputNumber);
            double[][] outputDataSample = CreateArray<double>((csvLength - 1) / sampleNumber, outputNumber);

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
                            if (sampleCount == sampleNumber)
                            {
                                inputDataSample[numOfSamples][j] = Convert.ToDouble(values[j]);
                                passed = true;
                            }
                            else
                            {
                                inputData[i - 1][j] = Convert.ToDouble(values[j]);
                            }
                        }

                        //Output array assigning.
                        for (int j = 0; j < outputNumber; j++)
                        {
                            if (sampleCount == sampleNumber)
                            {
                                outputDataSample[numOfSamples][j] = Convert.ToDouble(values[inputNumber + j]);
                                passed = true;
                            }
                            else
                            {
                                outputData[i - 1][j] = Convert.ToDouble(values[inputNumber + j]);
                            }
                        }
                        
                        //Reset logic.
                        if (passed == true)
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

            data info = new data();
            info.InputData = inputData; info.OutputData = outputData;
            info.InputDataSample = inputDataSample; info.OutputDataSample = outputDataSample;
            info.InputNumber = inputNumber; info.OutputNumber = outputNumber;

            return info;
        }
    }
}
