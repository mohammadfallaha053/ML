using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class TestDetail
    {

        public int TestDetailId { get; set; }

        public string Result { get; set; }
        
        public Test Test { get; set; }

        public int TestId { get; set; }

        public Analyse Analyse { get; set; }

        public int AnalyseId { get; set; }







    }
}
