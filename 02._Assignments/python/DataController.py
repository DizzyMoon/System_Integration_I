from fastapi import FastAPI, UploadFile, File
from DataParser import DataParser

app = FastAPI()
data_parser = DataParser()

json_ref = "data2/ppl.json"
csv_ref = "data2/ppl.csv"
xml_ref = "data2/ppl.xml"
yaml_ref = "data2/ppl.yaml"

@app.get("/json")
async def parse_json():
    data = data_parser.json_to_dict(json_ref)
    return {"parsed_data": data}

@app.get("/yaml")
async def parse_yaml():
    data = data_parser.yaml_to_dict(yaml_ref)
    return {"parsed_data": data}

@app.get("/xml")
async def parse_xml():
    data = data_parser.xml_to_dict(xml_ref)
    return {"parsed_data": data}

@app.get("/csv")
async def parse_csv():
    data = data_parser.csv_to_dict(yaml_ref)
    return {"parsed_data": data}
