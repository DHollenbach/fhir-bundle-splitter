using FhirBundleSplitter.Model.Entities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace FhirBundleSplitter.Core
{
	public static class JsonFlattener
	{
		/// <summary>
		/// Takes in a Json formated string and splits it into a KeyValuePair<string, object>
		/// </summary>
		/// <param name="json">string</param>
		/// <returns>IEnumerable<KeyValuePair<string, object>></returns>
		public static IEnumerable<KeyValuePair<string, object>> GetFlattenedJsonKeyValuePair(string json)
		{
			var obj = JObject.Parse(json);

			var result = GetFlattenedJsonKeyValuePair(obj);

			return result;
		}

		/// <summary>
		/// Takes in a Newtonsoft.Json.Linq.JObject and splits it into a KeyValuePair<string, object>
		/// </summary>
		/// <param name="json">JObject</param>
		/// <returns>IEnumerable<KeyValuePair<string, object>></returns>
		public static IEnumerable<KeyValuePair<string, object>> GetFlattenedJsonKeyValuePair(JObject json)
		{
			var result = json.Descendants()
				.OfType<JProperty>()
				.Select(p => new KeyValuePair<string, object>(p.Path,
					p.Value.Type == JTokenType.Array || p.Value.Type == JTokenType.Object
						? null : p.Value));

			return result;
		}

		/// <summary>
		/// Takes in a Json formatted string and splits it into a self referencing parent/child IEnumerable hierachy
		/// </summary>
		/// <param name="json">string</param>
		/// <returns>IEnumerable<FlattenedBundle></returns>
		public static IEnumerable<FlattenedBundle> GetFlattenedTreeBundle(string json)
		{
			var flattenedList = new List<FlattenedBundle>();
			var result = GetFlattenedJsonKeyValuePair(json);

			foreach(var item in result)
			{
				if (item.Value != null)
				{
					flattenedList.Add(new FlattenedBundle
					{
						FullPath = item.Key,
						Value = item.Value.ToString(),
						ValueType = item.Value.GetType().ToString()
					});
				}
			}

			return flattenedList;
		}
	}
}
