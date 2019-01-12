using System;
using System.Collections.Generic;
using System.IO;

namespace ENP1
{
    class data
    {
        public double[][] InputData { get; set; }
        public double[][] OutputData { get; set; }

        public double[][] InputDataSample { get; set; }
        public double[][] OutputDataSample { get; set; }

        public int InputNumber;
        public int OutputNumber;

        public data()
        {
            
        }

        static T[][] CreateArray<T>(int rows, int columns)
        {
            T[][] array = new T[rows][];

            for(int i = 0; i < rows; i++)
            {
                array[i] = new T[columns];
            }

            return array;
        }
        
        public data return_info(string path, List<string> titles, int sampleNumber)
        {
            int csvLength = 0;
            int inputNumber = 0, outputNumber = 0;

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
                            else
                            {
                                if (!updated)
                                {
                                    inputNumber++;
                                    updated = true;
                                }
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
            

            using (var reader = new StreamReader(path))
            {
                
            }
            
            bool passed = false;
            int numOfSamples = 0;
            int sampleCount = 0;
            sampleNumber = (csvLength - 1) / (((csvLength - 1) / 10) * sampleNumber);

            double[][] inputData = CreateArray<double>(csvLength - 1, inputNumber);
            double[][] outputData = CreateArray<double>(csvLength - 1, outputNumber);

            double[][] inputDataSample = CreateArray<double>((csvLength - 1) / sampleNumber, inputNumber);
            double[][] outputDataSample = CreateArray<double>((csvLength - 1) / sampleNumber, outputNumber);

            using (var reader = new StreamReader(path))
            {
                int i = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    
                    if (i >= 1)
                    {
                        
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

                        if (passed == true)
                        {
                            sampleCount = 0;
                            numOfSamples++;
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
