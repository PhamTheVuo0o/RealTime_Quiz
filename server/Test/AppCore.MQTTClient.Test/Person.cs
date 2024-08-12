using MessagePack;
using System.Diagnostics;
using System.Text.Json;

namespace AppCore.MQTTClient.Test
{
    [MessagePackObject]
    public class Person
    {
        [Key(0)]
        public int Id { get; set; }

        [Key(1)]
        public string Name { get; set; }

        public void Test(Person data)
        {
            var stopwatch = Stopwatch.StartNew();
            var json = JsonSerializer.Serialize(data);
            var jsonDeserialized = JsonSerializer.Deserialize<Person>(json);
            stopwatch.Stop();
            Console.WriteLine($"JSON Serialization/Deserialization Time: {stopwatch.ElapsedMilliseconds} ms");

            stopwatch.Restart();
            var messagePack = MessagePackSerializer.Serialize(data);
            var messagePackDeserialized = MessagePackSerializer.Deserialize<Person>(messagePack);
            stopwatch.Stop();
            Console.WriteLine($"MessagePack Serialization/Deserialization Time: {stopwatch.ElapsedMilliseconds} ms");

        }
    }
}
