using CSnakes.Runtime;
using System.Linq;

namespace testpythonaspng.Server.Models
{
    public interface IDataExploratory
    {
        public object GetColumns(string FileName);
        public object GetColumnsType(string FileName);

        public string GetCorrelation(string FileName);
        public object GetMLREvaluation(string FileName);
    }

    public class Column
    {
        public string data { get; set; }
        public string title { get; set; }
    }
    public class DataExploratory : IDataExploratory
    {
        IPythonEnvironment _pythonEnv;
        public DataExploratory(IPythonEnvironment pythonEnv)
        {
            _pythonEnv = pythonEnv;
        }
        public object GetColumns(string FileName)
        {
            var pythonExploratory = _pythonEnv.Exploratory();
            var pythonColumnList = pythonExploratory.GetColumns(FileName);
            return pythonColumnList;

        }

        public object GetColumnsType(string FileName)
        {
            var pythonExploratory = _pythonEnv.Exploratory();
            var pythonColumnTypeList = pythonExploratory.GetColumnsType(FileName);
            return pythonColumnTypeList;
        }

        public string GetCorrelation(string FileName)
        {
            var pythonExploratory = _pythonEnv.Exploratory();
            var corr_image_path = pythonExploratory.GetCorralationPerWeek(FileName);
            return corr_image_path;
        }

        public object GetMLREvaluation(string FileName)
        {
            var pythonExploratory = _pythonEnv.Exploratory();
            var ml_regression_stats = pythonExploratory.GetMlrEvaluation(FileName);
            return ml_regression_stats;
        }

    }
}
