using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication4.Models
{
    public class Disease 
    {
        public int RefId;
        public string Name;
        public string Phone;
        public int Id { get; set; }
        public int DiseaseId { get; set; }
        public int Cardiovascular { get; set; }
        public int Breathing { get; set; }
        public int URM { get; set; }
        public int SportsOthers { get; set; }
        public string Specific { get; set; }
        public int Gastric { get; set; }
        public string Specificgastric { get; set; }
        public int Allergy { get; set; }
        public string Specificallergy { get; set; }
        
        
    }
}
