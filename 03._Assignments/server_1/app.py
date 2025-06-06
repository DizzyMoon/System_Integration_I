import requests
from flask import Flask, jsonify
import json
import yaml
import xmltodict
import os

app = Flask(__name__)

DATA_DIR = os.path.join(os.path.dirname(__file__), 'data')

def parse_json():
    with open(os.path.join(DATA_DIR, 'data.json'), 'r') as f:
        return json.load(f)

def parse_yaml():
    with open(os.path.join(DATA_DIR, 'data.yaml'), 'r') as f:
        return yaml.safe_load(f)

def parse_xml():
    with open(os.path.join(DATA_DIR, 'data.xml'), 'r') as f:
        return xmltodict.parse(f.read())

def parse_txt():
    with open(os.path.join(DATA_DIR, 'data.txt'), 'r') as f:
        return {'text': f.read()}

@app.route('/get/json')
def get_json():
    try:
        url = 'http://localhost:3000/json'
        
        response = requests.get(url)
        response.raise_for_status()

        data = response.json()

        return jsonify({'data': data})
    except requests.RequestException as e:
        return josnify({'error': str(e)}, 500)

@app.route('/get/yaml')
def get_yaml():
    try:
        url = 'http://localhost:3000/yaml'
        
        response = requests.get(url)
        response.raise_for_status()

        data = response.json()

        return jsonify({'data': data})
    except requests.RequestException as e:
        return josnify({'error': str(e)}, 500)

@app.route('/get/xml')
def get_xml():
    try:
        url = 'http://localhost:3000/xml'
        
        response = requests.get(url)
        response.raise_for_status()

        data = response.json()

        return jsonify({'data': data})
    except requests.RequestException as e:
        return josnify({'error': str(e)}, 500)

@app.route('/get/txt')
def get_txt():
    try:
        url = 'http://localhost:3000/txt'
        
        response = requests.get(url)
        response.raise_for_status()

        data = resposne.json()

        return jsonify({'data': data})
    except requests.RequestException as e:
        return josnify({'error': str(e)}, 500)


@app.route('/json')
def json_route():
    try:
        data = parse_json()
        return jsonify(data)
    except Exception as e:
        return jsonify({'error': str(e)}), 500

@app.route('/yaml')
def yaml_route():
    try:
        data = parse_yaml()
        return jsonify(data)
    except Exception as e:
        return jsonify({'error': str(e)}), 500

@app.route('/xml')
def xml_route():
    try:
        data = parse_xml()
        return jsonify(data)
    except Exception as e:
        return jsonify({'error': str(e)}), 500

@app.route('/txt')
def txt_route():
    try:
        data = parse_txt()
        return jsonify(data)
    except Exception as e:
        return jsonify({'error': str(e)}), 500

if __name__ == '__main__':
    app.run(debug=True, port=5000)

