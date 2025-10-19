using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Data.Common;
using System.Net.Http;

namespace testpythonaspng.Server.Controllers
{
    public class DataAnalysis
    {
        public int pv_cnt  {get;set;}
        public int amount { get; set;}
        public double price { get; set;}
        public int week { get; set;}
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

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
            string json = JsonConvert.SerializeObject(new { data = pageDataAnalysis, recordsTotal = 6,});
            return Ok(json);
        }
    }
}
