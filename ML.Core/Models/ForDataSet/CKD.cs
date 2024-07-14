using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models.ForDataSet
{
    public class CKD
    {
        public int Id { get; set; }
        public int? Age { get; set; }

        public int? Gender { get; set; }

        public double? SerumCreatinine { get; set; }

        public double? Potassium { get; set; }

        public int? BloodUrea { get; set; }

        public double? Hemoglobin { get; set; }

        public bool? DiabetesMellitus { get; set; }

        public bool? Appetite { get; set; }

        public bool? Class { get; set; }



    }
}
