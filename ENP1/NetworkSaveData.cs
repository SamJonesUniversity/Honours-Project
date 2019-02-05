using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENP1
{
    class NetworkSaveData
    {
        public string Name { get; set; }

        public string NetworkFile { get; set; }

        public string CsvFile { get; set; }

        public string AnalystFile { get; set; }

        public string Inaccuracy { get; set; }

        public List<string> Headings { get; set; }
    }
}
