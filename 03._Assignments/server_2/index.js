const axios = require('axios');
const express = require('express');
const fs = require('fs').promises;
const path = require('path');
const xml2js = require('xml2js');
const yaml = require('js-yaml');

const app = express();
const port = 3000;

const dataDir = path.join(__dirname, 'data');

async function parseJson() {
  const content = await fs.readFile(path.join(dataDir, 'data.json'), 'utf-8');
  return JSON.parse(content);
}

async function parseYaml() {
  const content = await fs.readFile(path.join(dataDir, 'data.yaml'), 'utf-8');
  return yaml.load(content);
}

async function parseXml() {
  const content = await fs.readFile(path.join(dataDir, 'data.xml'), 'utf-8');
  return await xml2js.parseStringPromise(content);
}

async function parseTxt() {
  const content = await fs.readFile(path.join(dataDir, 'data.txt'), 'utf-8');
  return { text: content };
}


app.get('/get/json', async (req, res) => {
  try {
    // Replace with the URL of the server you want to request from
    const url = 'http://localhost:5000/json';

    const response = await axios.get(url);
    res.json({
      data: response.data
    });
  } catch (error) {
    res.status(500).json({
      error: 'Failed to fetch external data',
      details: error.message
    });
  }
});

app.get('/get/yaml', async (req, res) => {
  try {
    // Replace with the URL of the server you want to request from
    const url = 'http://localhost:5000/yaml';

    const response = await axios.get(url);
    res.json({
      data: response.data
    });
  } catch (error) {
    res.status(500).json({
      error: 'Failed to fetch external data',
      details: error.message
    });
  }
});

app.get('/get/xml', async (req, res) => {
  try {
    // Replace with the URL of the server you want to request from
    const url = 'http://localhost:5000/xml';

    const response = await axios.get(url);
    res.json({
      data: response.data
    });
  } catch (error) {
    res.status(500).json({
      error: 'Failed to fetch external data',
      details: error.message
    });
  }
});

app.get('/get/txt', async (req, res) => {
  try {
    // Replace with the URL of the server you want to request from
    const url = 'http://localhost:5000/txt';

    const response = await axios.get(url);
    res.json({
      data: response.data
    });
  } catch (error) {
    res.status(500).json({
      error: 'Failed to fetch external data',
      details: error.message
    });
  }
});

app.get('/json', async (req, res) => {
  try {
    const data = await parseJson();
    res.json(data);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

app.get('/yaml', async (req, res) => {
  try {
    const data = await parseYaml();
    res.json(data);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

app.get('/xml', async (req, res) => {
  try {
    const data = await parseXml();
    res.json(data);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

app.get('/txt', async (req, res) => {
  try {
    const data = await parseTxt();
    res.json(data);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

app.listen(port, () => {
  console.log(`Server listening on http://localhost:${port}`);
});
