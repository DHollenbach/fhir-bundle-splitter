using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FhirBundleSplitter.Data.Model
{
	public class SourceData
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Int64 ID { get; set; }
		public string Type { get; set; }
		public string JsonRaw { get; set; }
		public DateTime CreatedDate { get; set; }
		[NotMapped]
		public JObject Json => 
			string.IsNullOrEmpty(JsonRaw) ? null : JObject.Parse(JsonRaw);
	}
}
