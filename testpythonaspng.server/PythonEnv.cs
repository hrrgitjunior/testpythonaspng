namespace testpythonaspng.Server
{
    public interface IMyPythonEnv
    {
        void AddObj(string key, object obj);
    }
    public class PythonEnv : IMyPythonEnv
    {
        public Dictionary<string, object> dict = new Dictionary<string, object>();
        public void AddObj(string key, object obj)
        {
            dict.Add(key, obj);
        }
    }
}
