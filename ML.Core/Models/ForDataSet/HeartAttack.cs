using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models.ForDataSet
{
    public class HeartAttack
    {
        public int Id { get; set; }
        public int? Age { get; set; }

        public int? Gender { get; set; }

        public double? Troponin { get; set; }

        public double? Glucose { get; set; }

        public int? Impluse { get; set; }

        public bool? Class { get; set; }



        

    }
}
