using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class Analyse
    {
        public int AnalyseId { get; set; }


        public string Name { get; set; }

        public int NUint { get; set; }

        public List<NaturalField> NaturalFields { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }
    }
}
