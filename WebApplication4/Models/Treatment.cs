﻿using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication4.Models
{
    public class Treatment 
    {
        public int Id { get; set; }
        public int refId;
        public string scannum { get; set; }
        public string visit { get; set; }
        public int name { get; set; }
        public string condition { get; set; }
        public DateTime time { get; set; }
      
        public int Omeprazole { get; set; }
        public int Rabeprazole { get; set; }
        public int Esomeprazole { get; set; }
        public int Pantoprazole { get; set; }
        public int OtherPPI { get; set; }
        public int Amoxicillin { get; set; }
        public int tetracycline { get; set; }
        public int Levofloxacin { get; set; }
        public int Clarithromycin { get; set; }
        public int Furazolidone { get; set; }
        public int Metronidazole { get; set; }
        public int treatTime { get; set; }
        public int isOnTime { get; set; }
        public string content { get; set; }
		public int biji { get; set; }
        public int isEradicated { get; set; }
        public int isfollowup { get; set; }
        public DateTime followuptime { get; set; }
        public string bingfa { get; set; }
        public string pic1 { get; set; }

		public string pic2 { get; set; }
		public string pic3 { get; set; }
        public string pic4 { get; set; }

        public string pic5 { get; set; }
        public string pic6 { get; set; }
        public string pic7 { get; set; }

        public string pic8 { get; set; }
        
    }
}
