using System.Collections.Generic;

namespace FhirBundleSplitter.Model.Entities
{
	public class FlattenedBundle
	{
		private int indexOfSplit => FullPath.LastIndexOf('.');
		public string FullPath { get; set; }
		public string Item =>
			FullPath.Substring(indexOfSplit + 1);
		public string Value { get; set; }
		public string ValueType { get; set; }
		public string Parent =>
			indexOfSplit < 0 ? string.Empty : FullPath.Substring(0, indexOfSplit);
	}
}
