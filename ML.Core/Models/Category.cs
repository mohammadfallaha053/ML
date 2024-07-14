using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        [MaxLength(100)]
        public string CategoryName { get; set; }

        public List<Analyse> Analyses { get; set; }
    }
}
