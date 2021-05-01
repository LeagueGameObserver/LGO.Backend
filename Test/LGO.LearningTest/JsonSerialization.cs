using System.Collections.Concurrent;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LGO.LearningTest
{
    [TestFixture]
    public class JsonSerialization
    {
        private class ImmutableClass
        {
            [JsonProperty]
            public string StringProperty { get; init; } = string.Empty;
        }

        private sealed record ImmutableRecord
        {
            [JsonProperty]
            public string StringProperty { get; init; } = string.Empty;
        }

        private sealed record InstanceControlledImmutableRecord
        {
            private static ConcurrentDictionary<string, InstanceControlledImmutableRecord> Instances { get; } = new();

            public static InstanceControlledImmutableRecord ForString(string value)
            {
                return Instances.GetOrAdd(value, _ => new InstanceControlledImmutableRecord {StringProperty = value});
            }
            
            [JsonProperty]
            public string StringProperty { get; init; } = string.Empty;
            
            [JsonConstructor]
            private InstanceControlledImmutableRecord()
            { }
        }

        [Test]
        public void TestSerializeImmutableClass()
        {
            var instance = new ImmutableClass {StringProperty = "Hello, Json.NET!"};

            var serializedInstance = JsonConvert.SerializeObject(instance);
            var expected = $@"{{""{nameof(ImmutableClass.StringProperty)}"":""Hello, Json.NET!""}}";

            Assert.AreEqual(expected, serializedInstance);
        }

        [Test]
        public void TestDeserializeImmutableClass()
        {
            var serializedInstance = $@"{{
  ""{nameof(ImmutableClass.StringProperty)}"": ""Hello, Json.NET!""
}}";

            var instance = JsonConvert.DeserializeObject<ImmutableClass>(serializedInstance);
            Assert.AreEqual("Hello, Json.NET!", instance.StringProperty);
        }

        [Test]
        public void TestSerializeImmutableRecord()
        {
            var instance = new ImmutableRecord {StringProperty = "Hello, Json.NET!"};
            var serializedInstance = JsonConvert.SerializeObject(instance);

            var expected = $@"{{""{nameof(ImmutableRecord.StringProperty)}"":""Hello, Json.NET!""}}";
            Assert.AreEqual(expected, serializedInstance);
        }

        [Test]
        public void TestDeserializeImmutableRecord()
        {
            var serializedInstance = $@"{{""{nameof(ImmutableRecord.StringProperty)}"":""Hello, Json.NET!""}}";
            var instance = JsonConvert.DeserializeObject<ImmutableRecord>(serializedInstance);
            
            Assert.AreEqual("Hello, Json.NET!", instance.StringProperty);
        }
        
        [Test]
        public void TestSerializeImmutableRecordWithPrivateConstructor()
        {
            var instance = InstanceControlledImmutableRecord.ForString("Hello, Json.NET!");
            var serializedInstance = JsonConvert.SerializeObject(instance);

            var expected = $@"{{""{nameof(InstanceControlledImmutableRecord.StringProperty)}"":""Hello, Json.NET!""}}";
            Assert.AreEqual(expected, serializedInstance);
        }

        [Test]
        public void TestDeserializeImmutableRecordWithPrivateConstructor()
        {
            var serializedInstance = $@"{{""{nameof(InstanceControlledImmutableRecord.StringProperty)}"":""Hello, Json.NET!""}}";
            var instance = JsonConvert.DeserializeObject<InstanceControlledImmutableRecord>(serializedInstance);
            
            Assert.AreEqual("Hello, Json.NET!", instance.StringProperty);
        }
    }
}