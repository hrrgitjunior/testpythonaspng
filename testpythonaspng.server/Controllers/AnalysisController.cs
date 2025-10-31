using CSnakes.Runtime;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Net.Http;
using testpythonaspng.Server.Models;

namespace testpythonaspng.Server.Controllers
{
    public class DataAnalysis
    {
        public int pv_cnt  {get;set;}
        public int amount { get; set;}
        public double price { get; set;}
        public int week { get; set;}
    }

   /* public class Column
    {
        public string data { get; set; }
        public string title { get; set; }
    }*/

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Filter
    {
        public string field;
        public string value;
    }

    public class DataTableAjaxPostModel
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
/*        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public Filter filter;*/
        //  public List<Order> order { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private MyLogger _myLogger;
        IPythonEnvironment _pythonEnv;

        public AnalysisController(ILogger<HomeController> logger,
                                  PythonEnv pythonEnv)
        {
            _logger = logger;
            _pythonEnv = pythonEnv.dict["PythonEnv"] as IPythonEnvironment;
        }

        public string[][] ReadCSV(string filename)
        {
            List<string[]> tempList = new List<string[]>();
            string line;
            StreamReader reader = new StreamReader("test.txt");

            while ((line = reader.ReadLine()) != null)
            {
                tempList.Add(SplitCSVLine(line));
            }
            reader.Close();
            return tempList.ToArray();
        }

        public string[] SplitCSVLine(string line)
        {
            List<string> result = new List<string>();
            result.AddRange(line.Split(new char[] { ',' }));

            return result.ToArray();
        }

        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }


        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] DataTableAjaxPostModel model)
        {
            DataTableAjaxPostModel dtModel;

            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                dtModel = JsonConvert.DeserializeObject<DataTableAjaxPostModel>(body);
            }

            List<DataAnalysis> dataAnalysis = new List<DataAnalysis>
            {
              new DataAnalysis
              {
                  pv_cnt = 50,
                  amount = 100,
                  price =  0.8,
                  week = 1
              },
              new DataAnalysis
              {
                  pv_cnt = 55,
                  amount = 120,
                  price =  0.8,
                  week = 2

              },
              new DataAnalysis
              {
                  pv_cnt = 60,
                  amount = 150,
                  price =  0.8,
                  week = 3
              },
              new DataAnalysis
              {
                  pv_cnt = 70,
                  amount = 200,
                  price =  0.9,
                  week = 4

              },
              new DataAnalysis
              {
                  pv_cnt = 80,
                  amount = 250,
                  price =  0.9,
                  week = 5

              },
              new DataAnalysis
              {
                  pv_cnt = 90,
                  amount = 300,
                  price =  0.9,
                  week = 6
              }


            };

            List<DataAnalysis> pageDataAnalysis = dataAnalysis
                                                   .Skip(dtModel.start)
                                                   .Take(dtModel.length).ToList();

            //return data;
            string fullPath = Path.Combine("uploads/", "product_vending_analysis.csv");
           // var csv_data = this.ReadCSV(fullPath);
            DataTable dt = ConvertCSVtoDataTable("uploads/product_vending_analisys.csv");
            DataExploratory dataExpl = new DataExploratory(_pythonEnv);
            var columnList = dataExpl.GetColumns("uploads/product_vending_analisys.csv");
            int numberOfRecords = dt.Rows.Count;
            var csv_page = dt
                        .AsEnumerable()
                        .Skip(dtModel.start)
                        .Take(dtModel.length).CopyToDataTable(); 

            string json = JsonConvert.SerializeObject(new { data = pageDataAnalysis, recordsTotal = 6, test = csv_page, columns = columnList, rowNumber = numberOfRecords });
            return Ok(json);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Exploratory()
        {

            /*          export let dataanalys_columns = [
            { 'data': 'pv_cnt', 'title': 'pv_cnt' },
            { 'data': 'amount', 'title': 'amount' },
            { 'data': 'price', 'title': 'price' },
            { 'data': 'week', 'title': 'week' }]*/
    /*        List<Column> columnList = new List<Column>()
            {
              new Column
              {
                  data = "pv_cnt",
                  title = "pv_cnt"
              },
              new Column
              {
                  data = "amount",
                  title = "amount"
              },
                            new Column
              {
                  data = "price",
                  title = "price"
              },
              new Column
              {
                  data = "week",
                  title = "week"
              }

            };*/
          /*  var pythonExploratory = _pythonEnv.Exploratory();
            var exploratoryColumns = pythonExploratory.GetColumns("aaa");*/
            DataExploratory dataExpl = new DataExploratory(_pythonEnv);
            var columnList = dataExpl.GetColumns("uploads/product_vending_analisys.csv");


            string json = JsonConvert.SerializeObject(new {columns = columnList, exploratory_columns = columnList});
            //string json = JsonConvert.SerializeObject(new { columns = columnList});
            return Ok(json);
        }
    }
}
