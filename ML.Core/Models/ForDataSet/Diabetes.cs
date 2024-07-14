using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models.ForDataSet
{
    public class Diabetes

    {
        public int Id { get; set; }
        public int? Age { get; set; }

        public int? Gender { get; set; }

        public double? Glucose { get; set; }

        public double? HbA1cLevel { get; set; }

        public int? Hypertension { get; set; }

        public int? HeartDisease { get; set; }

        public bool? Class { get; set; }
    }
}
