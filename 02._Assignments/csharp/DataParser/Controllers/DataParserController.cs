using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Mvc;
using Services;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    string json_ref = "data1/bob.json";
    string yaml_ref = "data1/bob.yaml";
    string xml_ref = "data1/bob.xml";
    string csv_ref = "data1/bob.csv";

    DataParsing dp = new DataParsing();

    [HttpGet]
    [Route("/json")]
    public Dictionary<string, object> json_to_dict()
    {
        Dictionary<string, object> data = dp.json_to_dict(json_ref);
        return data;
    }

    [HttpGet]
    [Route("/yaml")]
    public Dictionary<string, object> yaml_to_dict()
    {
        Dictionary<string, object> data = dp.yaml_to_dict(yaml_ref);
        return data;
    }

    [HttpGet]
    [Route("/xml")]
    public Dictionary<string, object> xml_to_dict(){
        Dictionary<string, object> data = dp.xml_to_dict(xml_ref);
        return data;
    }

    [HttpGet]
    [Route("/csv")]
    public List<Dictionary<string, string>> csv_to_dict() {
        List<Dictionary<string, string>> data = dp.csv_to_dict(csv_ref);
        return data;
    }
}