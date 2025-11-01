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

            string json = JsonConvert.SerializeObject(new {data = csv_page, columns = columnList, rowNumber = numberOfRecords });
            return Ok(json);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> ExploratoryColumns()
        {
            DataExploratory dataExpl = new DataExploratory(_pythonEnv);
            var columnList = dataExpl.GetColumns("uploads/product_vending_analisys.csv");
            var columnTypeList = dataExpl.GetColumnsType("uploads/product_vending_analisys.csv");
            string json = JsonConvert.SerializeObject(new {tableColumns = columnList, exploratory_columns = columnList, columnsType = columnTypeList});
            return Ok(json);
        }
    }
}
