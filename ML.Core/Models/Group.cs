using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class Group
    {
        public int GroupId { get; set; }
     
        [MaxLength(100)]
        public string GroupName { get; set; }


        public List<Analyse> Analyses { get; set; }

        
    }
}
