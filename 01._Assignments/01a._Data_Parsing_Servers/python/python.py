import yaml
import csv
import xmltodict
import json

class DataParser():
    def csv_to_dict(self, csv_file):
        parsed_data = []  # To store all the processed rows

        with open(csv_file, mode='r') as file:
            reader = csv.DictReader(file)
            
            for row in reader:
                for key, value in row.items():
                    if isinstance(value, str) and ';' in value:
                        row[key] = value.split(';')
                
                parsed_data.append(row)

        return parsed_data


    def json_to_dict(self, json_ref):
        with open (json_ref, 'r') as file:
            data = json.load(file)

        return data


    def yaml_to_dict(self, yaml_ref):
        with open (yaml_ref, 'r') as file:
            data = yaml.safe_load(file)

        return data

    def xml_to_dict(self,xml_ref):
        with open (xml_ref, 'r') as file:
            data = xmltodict.parse(file.read())

        return data
    

if __name__ == "__main__":
    json_ref = "data2/ppl.json"
    csv_ref = "data2/ppl.csv"
    xml_ref = "data2/ppl.xml"
    yaml_ref = "data2/ppl.yaml"

    dp = DataParser()

    print("######### JSON #########")
    print(dp.json_to_dict(json_ref))

    print ("######### CSV #########")
    print(dp.csv_to_dict(csv_ref))

    print("######### XML #########")
    print(dp.xml_to_dict(xml_ref))

    print("######### YAML #########")
    print(dp.yaml_to_dict(yaml_ref))