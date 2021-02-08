using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Prediktor.Log;

namespace Prediktor.Log
{

    public static class LogManager
    {
        private class LogFactory : ITraceLogFactory
        {
            private Func<string, ITraceLog> _traceLogFactory;
            private static Prediktor.Log.Implementation.TraceSourceLog _defaultTraceLog = new Prediktor.Log.Implementation.TraceSourceLog(GetDefaultLogName(), GetLogLevel());
            internal static Prediktor.Log.Implementation.TraceSourceLog DefaultTraceLog { get { return _defaultTraceLog; } }

            public static LogLevel GetLogLevel()
            {
                return LogLevel.Debug;
            }
            private static string GetDefaultLogName()
            {
                //if (System.Configuration.ConfigurationManager.AppSettings != null)
                //{
                //    if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["DefaultLogName"]))
                //    {
                //        return System.Configuration.ConfigurationManager.AppSettings["DefaultLogName"];
                //    }
                //}
                return "DefaultTraceSource";
            }
            public LogFactory(Func<string, ITraceLog> logFactoryImpl)
            {
                _traceLogFactory = logFactoryImpl;
            }
            public LogFactory()
            {
                _traceLogFactory = (name) => new Prediktor.Log.Implementation.TraceSourceLog(name, GetLogLevel(), _defaultTraceLog);
            }

            public ITraceLog GetLogger(string logName)
            {
                _traceLog.InfoFormat("Getting trace log: {0}", logName);
                return _traceLogFactory(logName);
            }

            public ITraceLog GetLogger(Type typeName)
            {
                return GetLogger(typeName.ToString());
            }
        }
        private static ITraceLogFactory _logFactory = new LogFactory();
        private static ITraceLog _traceLog = new Prediktor.Log.Implementation.TraceSourceLog("LogManager", LogFactory.GetLogLevel(), LogFactory.DefaultTraceLog);

        public static Func<string, ITraceLog> TraceLogFactory { set { _logFactory = new LogFactory(value); } }
        public static void SetTraceLogFactory(ITraceLogFactory logFactory)
        {
            _logFactory = logFactory;
        }
        public static ITraceLog GetLogger(string logName)
        {
            return _logFactory.GetLogger(logName);
        }
        public static ITraceLog GetLogger(Type typeName)
        {
            return _logFactory.GetLogger(typeName);
        }
    }
}
