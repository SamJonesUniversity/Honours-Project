using System.Collections.Generic;

namespace ENP1
{
	/// <summary> Class to store network information for saving and loading networks. </summary>
    internal class NetworkSaveData
    {
        public string Name { get; set; }

        public string NetworkFile { get; set; }

		public string NetworkType { get; set; }

        public string CsvFile { get; set; }

        public string Path { get; set; }

        public string AnalystFile { get; set; }

        public string Inaccuracy { get; set; }

        public List<string> InputHeadings { get; set; }

        public List<string> OutputHeadings { get; set; }
    }
}
