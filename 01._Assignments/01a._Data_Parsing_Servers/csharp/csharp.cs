using System.Globalization;
using System.Xml.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

class DataParsing {
    public Dictionary<string, object> json_to_dict(string json_ref) {
        string json = File.ReadAllText(json_ref);
        Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        return dictionary;
    }

    public Dictionary<string, object> yaml_to_dict(string yaml_ref) {
        string yaml = File.ReadAllText(yaml_ref);
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var dictionary = deserializer.Deserialize<Dictionary<string, object>>(yaml);
        return dictionary;
    }

    public Dictionary<string, object> xml_to_dict(string xml_ref) {
        XDocument xmlDoc = XDocument.Load(xml_ref);
        Dictionary<string, object> dictionary = new Dictionary<string, object>();

        foreach (var element in xmlDoc.Root.Elements()) {
            dictionary[element.Name.LocalName] = element.Value;
        }

        return dictionary;
    }

    public List<Dictionary<string, string>> csv_to_dict(string csv_ref) {
        using (var reader = new StreamReader(csv_ref))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture))) {
            var records = new List<Dictionary<string, string>>();

            using (var dr = new CsvDataReader(csv)) {
                while (dr.Read()) {
                    var row = new Dictionary<string, string>();

                    for (int i = 0; i < dr.FieldCount; i++) {
                        row[dr.GetName(i)] = dr.GetValue(i)?.ToString();
                    }

                    records.Add(row);
                }
            }
            return records;
        }
    }
}

class Run {
    static void Main() {
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

    static void PrintDictionary(Dictionary<string, object> dict) {
        foreach (var kvp in dict) {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }

    static void PrintCsv(List<Dictionary<string, string>> records) {
        foreach (var row in records) {
            Console.WriteLine("Row:");
            foreach (var kvp in row) {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
        }
    }
}
