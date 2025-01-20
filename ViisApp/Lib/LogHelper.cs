using NLog;

namespace ViisApp.Lib
{
    public class LogHelper
    {
        private static Logger logger;
        //public static Logger GetLogger(Type T)
        //{
        //    NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("nlog.config");
        //    logger = NLog.LogManager.GetLogger(T.Name);

        //    return logger;
        //}

        public static Logger GetLogger(string logerName)
        {
            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("nlog.config");
            logger = NLog.LogManager.GetLogger(logerName);

            return logger;
        }
    }
}
