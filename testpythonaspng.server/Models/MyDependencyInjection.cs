namespace testpythonaspng.Server.Models
{
    public class MyDependencyInjection
    {
    }
    public interface ILog
    {
        void info(string str);
    }

    class MyConsoleLogger : ILog
    {
        public void info(string str)
        {
            Console.WriteLine(str);
        }
    }


    public struct IpInfo
    {
        public string ip;
        public bool is_not_allowed;
    }

    public interface IMyLogger
    {
        IpInfo GetUserIP(Microsoft.AspNetCore.Http.HttpContext context);
        void WriteLog(Microsoft.AspNetCore.Http.HttpContext context, string log_text);
        void WriteMsg(string msg);
        void Log(string logMessage, TextWriter w);

    }

    class SessionInfo
    {
        private string key; // field
        private string name; // field
        public string Key   // property
        {
            get { return key; }
            set { key = value; }
        }
        public string Name   // property
        {
            get { return name; }
            set { name = value; }
        }

        public SessionInfo(string k, string v)
        {
            this.Key = k;
            this.Name = v;
        }
    }

    public interface ISessionList
    {
        void AddSessionId(string SessionKey, string SessionName);
        string GetSessionId(int index);


    }

    public class MyLogger : IMyLogger
    {
        //string logFile = @"Log\hrembros_log.txt";
        //string logFile = Path.Combine(Directory.GetCurrentDirectory() + "\\Log\\","hrembros_log.txt");

        private string logFile = "MyLogger.txt";
        string[] not_allowed = { "46.10.", "87.126.", "90.154.", "151.251.", "66.249." };

        public MyLogger()
        {

            if (!(File.Exists(logFile)))
            {
                File.Create(logFile);
            }
        }

        public IpInfo GetUserIP(Microsoft.AspNetCore.Http.HttpContext context)
        {
            //TODO Can I get remote client ip addres;
            IpInfo ipInfo;
            ipInfo.ip = Convert.ToString(context.Connection.RemoteIpAddress);
            ipInfo.is_not_allowed = false;

            for (int i = 0; i <= not_allowed.Length - 1; i++)
            {
                ipInfo.is_not_allowed = ipInfo.ip.Contains(not_allowed[i]);
                if (ipInfo.is_not_allowed)
                {
                    break;
                }

            }
            return ipInfo;
        }



        public async void WriteLog(Microsoft.AspNetCore.Http.HttpContext context, string log_text)
        {
            IpInfo ipInfo = GetUserIP(context);
            if (!ipInfo.is_not_allowed)
            {

                using (StreamWriter w = File.AppendText(logFile))
                {
                    await Task.Run(() => Log(log_text + " :ip = " + ipInfo.ip, w));
                }

            }
            else
            {
                using (StreamWriter w = File.AppendText(logFile))
                {
                    await Task.Run(() => Log(log_text + " :ip = " + ipInfo.ip + " :self or google", w));
                }
            }
        }

        public async void WriteMsg(string msg)
        {
            using (StreamWriter w = File.AppendText(logFile))
            {
                await Task.Run(() => Log(msg, w));
            }
        }

        public void Log(string logMessage, TextWriter w)
        {
            w.Write($"{DateTime.Now}");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-----------------");
        }
    }


    public class SessionList : ISessionList
    {
        List<SessionInfo> sessionInfo = new List<SessionInfo>();
        public void AddSessionId(string SessionKey, string SessionName)
        {
            this.sessionInfo.Add(new SessionInfo(SessionKey, SessionName));
        }

        public string GetSessionId(int index)
        {
            SessionInfo sessInfo = this.sessionInfo.ElementAt(index);
            return sessInfo.Key;

        }
    }

}
