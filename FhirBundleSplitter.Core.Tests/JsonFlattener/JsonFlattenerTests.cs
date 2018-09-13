using Newtonsoft.Json.Linq;
using System.Linq;
using Xunit;

namespace FhirBundleSplitter.Core.Tests
{
	public class JsonFlattenerTests
	{
		[Fact]
		public void GetFlattenedJsonKeyValuePair_GivenValidJsonString_ShouldReturnKeyValuePairObject()
		{
			// Arrange
			var sampleData = "{ }";

			// Act
			var result = JsonFlattener.GetFlattenedJsonKeyValuePair(sampleData);

			// Assert
			Assert.NotNull(result);
			Assert.Empty(result);
		}

		[Fact]
		public void GetFlattenedJsonKeyValuePair_GivenValidJObject_ShouldReturnKeyValuePairObject()
		{
			// Arrange
			var sampleData = JObject.Parse("{ }");

			// Act
			var result = JsonFlattener.GetFlattenedJsonKeyValuePair(sampleData);

			// Assert
			Assert.NotNull(result);
			Assert.Empty(result);
		}

		[Fact]
		public void GetFlattenedJsonKeyValuePair_GivenValidJsonString_ShouldReturnFlattenedJson()
		{
			// Arrange
			var sampleData = GetSimpleSampleJson();

			// Act
			var result = JsonFlattener.GetFlattenedJsonKeyValuePair(sampleData).ToList();

			// Assert
			Assert.NotEmpty(result);
			Assert.Equal("firstName", result[0].Key);
			Assert.Equal("John", result[0].Value.ToString());
			Assert.Equal("age", result[3].Key);
			Assert.Equal(25, int.Parse(result[3].Value.ToString()));
		}

		[Fact]
		public void GetFlattenedJsonKeyValuePair_GivenValidJObject_ShouldReturnFlattenedJson()
		{
			// Arrange
			var sampleData = JObject.Parse(GetSimpleSampleJson());

			// Act
			var result = JsonFlattener.GetFlattenedJsonKeyValuePair(sampleData).ToList();

			// Assert
			Assert.NotEmpty(result);
			Assert.Equal("firstName", result[0].Key);
			Assert.Equal("John", result[0].Value.ToString());
			Assert.Equal("age", result[3].Key);
			Assert.Equal(25, int.Parse(result[3].Value.ToString()));
		}

		[Fact]
		public void GetFlattenedTreeBundle_GivenValidJsonString_ShouldReturnFlattenedBundle()
		{
			// Arrange
			var sampleData = GetSimpleSampleJson();

			// Arrange
			var result = JsonFlattener.GetFlattenedTreeBundle(sampleData).ToList();

			// Act
			Assert.NotNull(result);
			Assert.Equal(15, result.Count);
			Assert.Equal("firstName", result[0].FullPath);
			Assert.Equal("firstName", result[0].Item);
			Assert.Equal("John", result[0].Value);
			Assert.Equal(string.Empty, result[0].Parent);
			Assert.Equal("address", result[4].Parent);
			Assert.Equal("address.streetAddress", result[4].FullPath);
			Assert.Equal("streetAddress", result[4].Item);
			Assert.Equal("21 2nd Street", result[4].Value);
			Assert.Equal("phoneNumbers[0]", result[8].Parent);
			Assert.Equal("phoneNumbers[0].type", result[8].FullPath);
			Assert.Equal("type", result[8].Item);
			Assert.Equal("home", result[8].Value);
		}

		private string GetSimpleSampleJson()
		{
			return "{\"firstName\": \"John\", \"lastName\": \"Smith\", \"isAlive\": true, \"age\": 25, \"address\": { \"streetAddress\": \"21 2nd Street\", " +
						"\"city\": \"New York\", \"state\": \"NY\", \"postalCode\": \"10021-3100\" }, \"phoneNumbers\": [{ \"type\": \"home\", \"number\": \"212 555-1234\"" +
							"}, { \"type\": \"office\", \"number\": \"646 555-4567\" }, { \"type\": \"mobile\", \"number\": \"123 456-7890\" }], \"children\": []," +
								"\"spouse\": null }";
		}
	}
}
