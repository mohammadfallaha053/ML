using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class NaturalField
    {
        public int NaturalFieldId { get; set; }

        public double Max { get; set; }

        public double Min { get; set; }

        public Analyse Analyse { get; set; }

        public int AnalyseId { get; set; }

   
        public GenderType Gender { get; set; } // الحقل الجديد
    }
}


public enum GenderType
{
    Male = 0,
    Female = 1 ,
    Child = 2 
}