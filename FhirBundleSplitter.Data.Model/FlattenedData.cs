using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FhirBundleSplitter.Data.Model
{
	public class FlattenedData
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Int64 ID { get; set; }
		[ForeignKey("SourceData")]
		public Int64 SourceId { get; set; }
		public string FullPath { get; set; }
		public string Parent { get; set; }
		public string Item { get; set; }
		public string Value { get; set; }
		public virtual SourceData SourceData { get; set; }
	}
}
