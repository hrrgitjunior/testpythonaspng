using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace testpythonaspng.Server.Controllers
{
    public class AnalysData
    {
        public string col1 { get; set; }
        public double col2 { get; set; }
        public double col3 { get; set; }

    }

    [ApiController]
    [Route("[controller]")]

   /* [Route("api/analysis")]
    [ApiController]*/
    public class AnalysisController : ControllerBase
    {

       // [HttpGet("{analysistable}")]
        [HttpGet(Name = "GetAnalysis")]

        public IEnumerable<AnalysData> Get()
        {
            List<AnalysData> analysData = new List<AnalysData>
            {
                new AnalysData {
                    col1 = "VAF01",
                    col2 = 10.10,
                    col3 = 20.20
                }
            };
            //return Json(new { data = "AAAAAA" });
            return analysData.ToArray();
        }
     }
               
}
