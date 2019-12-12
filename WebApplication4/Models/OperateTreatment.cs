using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
	public class OperateTreatment
	{

		public int id { get; set; }
		public string visit { get; set; }
		public string scannum { get; set; }
		public string conditions { get; set; }
		public DateTime time { get; set; }
		public string content { get; set; }
		public string pic1 { get; set; }
		public string pic2 { get; set; }
		public string pic { get; set; }
		public int isfollowup { get; set; }
	}
}