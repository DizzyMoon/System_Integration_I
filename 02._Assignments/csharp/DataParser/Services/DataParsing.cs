using System.Globalization;
using System.Xml.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Services
{
    class DataParsing
    {
        public Dictionary<string, object> json_to_dict(string json_ref)
        {
            string json = File.ReadAllText(json_ref);
            JObject parsedJson = JObject.Parse(json);

            return ParseJToken(parsedJson);
        }

        public Dictionary<string, object> ParseJToken(JToken token)
        {
            var dictionary = new Dictionary<string, object>();

            // Iterate through all keys and values in the JObject
            if (token.Type == JTokenType.Object)
            {
                foreach (var property in token.Children<JProperty>())
                {
                    var key = property.Name;
                    var value = property.Value;

                    // Handle different types of values dynamically
                    if (value.Type == JTokenType.Array)
                    {
                        // Deserialize arrays into lists of strings or other types
                        var list = new List<object>();
                        foreach (var item in value)
                        {
                            if (item.Type == JTokenType.String)
                            {
                                list.Add(item.ToString());
                            }
                            else if (item.Type == JTokenType.Object)
                            {
                                list.Add(ParseJToken(item)); // Recursively handle nested objects
                            }
                            else
                            {
                                list.Add(item);
                            }
                        }
                        dictionary[key] = list;
                    }
                    else if (value.Type == JTokenType.Object)
                    {
                        // Recursively parse nested objects
                        dictionary[key] = ParseJToken(value);
                    }
                    else
                    {
                        // For primitive types (strings, numbers, etc.), just add them as-is
                        dictionary[key] = value.ToString();
                    }
                }
            }

            return dictionary;
        }

        public Dictionary<string, object> yaml_to_dict(string yaml_ref)
        {
            string yaml = File.ReadAllText(yaml_ref);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var dictionary = deserializer.Deserialize<Dictionary<string, object>>(yaml);
            return dictionary;
        }

        public Dictionary<string, object> xml_to_dict(string xml_ref)
        {
            XDocument xmlDoc = XDocument.Load(xml_ref);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            foreach (var element in xmlDoc.Root.Elements())
            {
                dictionary[element.Name.LocalName] = element.Value;
            }

            return dictionary;
        }

        public List<Dictionary<string, string>> csv_to_dict(string csv_ref)
        {
            using (var reader = new StreamReader(csv_ref))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = new List<Dictionary<string, string>>();

                using (var dr = new CsvDataReader(csv))
                {
                    while (dr.Read())
                    {
                        var row = new Dictionary<string, string>();

                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            row[dr.GetName(i)] = dr.GetValue(i)?.ToString();
                        }

                        records.Add(row);
                    }
                }
                return records;
            }
        }
    }

    class Run
    {
        static void Main()
        {
            DataParsing dp = new DataParsing();

            string json_ref = "../data1/bob.json";
            string yaml_ref = "../data1/bob.yaml";
            string xml_ref = "../data1/bob.xml";
            string csv_ref = "../data1/bob.csv";

            Console.WriteLine("######## JSON ########");
            PrintDictionary(dp.json_to_dict(json_ref));

            Console.WriteLine("######## YAML ########");
            PrintDictionary(dp.yaml_to_dict(yaml_ref));

            Console.WriteLine("######## XML ########");
            PrintDictionary(dp.xml_to_dict(xml_ref));

            Console.WriteLine("######## CSV ########");
            PrintCsv(dp.csv_to_dict(csv_ref));
        }

        static void PrintDictionary(Dictionary<string, object> dict)
        {
            foreach (var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }

        static void PrintCsv(List<Dictionary<string, string>> records)
        {
            foreach (var row in records)
            {
                Console.WriteLine("Row:");
                foreach (var kvp in row)
                {
                    Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
                }
            }
        }
    }
}
