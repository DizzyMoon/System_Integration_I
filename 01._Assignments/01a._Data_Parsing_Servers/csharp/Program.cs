DataParsing dp = new();

        string json_ref = "/home/mikkel/repos/System-Integration-1st-semester/01._Assignments/01a._Data_Parsing_Servers/data1/bob.json";
        string yaml_ref = "/home/mikkel/repos/System-Integration-1st-semester/01._Assignments/01a._Data_Parsing_Servers/data1/bob.yaml";
        string xml_ref = "/home/mikkel/repos/System-Integration-1st-semester/01._Assignments/01a._Data_Parsing_Servers/data1/bob.xml";
        string csv_ref = "/home/mikkel/repos/System-Integration-1st-semester/01._Assignments/01a._Data_Parsing_Servers/data1/bob.csv";

        Console.WriteLine("######## JSON ########");
        PrintDictionary(dp.json_to_dict(json_ref));

        Console.WriteLine("######## YAML ########");
        PrintDictionary(dp.yaml_to_dict(yaml_ref));

        Console.WriteLine("######## XML ########");
        PrintDictionary(dp.xml_to_dict(xml_ref));

        Console.WriteLine("######## CSV ########");
        PrintCsv(dp.csv_to_dict(csv_ref));

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