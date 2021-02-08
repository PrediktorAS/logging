using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prediktor.Log;

namespace Prediktor.Log.Implementation
{
    public class SimpleTraceLog : ITraceLog
    {
        private LogLevel _logLevel = LogLevel.Fatal;
        private string _logName;

        private bool LogDebug { get { return _logLevel >= LogLevel.Debug; } }
        private bool LogInfo { get { return _logLevel >= LogLevel.Info; } }
        private bool LogWarning { get { return _logLevel >= LogLevel.Warning; } }
        private bool LogFatal { get { return _logLevel >= LogLevel.Fatal; } }

		public SimpleTraceLog(string logName, LogLevel logLevel)
        {
            _logLevel = logLevel;
            _logName = logName;
        }

        private void LogImpl(object logEntry)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("{0:yyyy/MM/dd HH:mm:ss.fff}({1}) : {2}", DateTime.Now, _logName, logEntry.ToString()));
        }

        private void Log(LogLevel logLevel, object logEntry)
        {
            if (logLevel <= _logLevel)
            {
                LogImpl(logEntry);
            }
        }
        private void Log(LogLevel logLevel, object logEntry, Exception e)
        {
            if (logLevel <= _logLevel)
            {
                LogImpl(string.Format("{0} - Exception: {1}", logEntry, e.ToString()));
            }
        }
        private void Log(LogLevel logLevel, string formatString, params object[] args)
        {
            if (logLevel <= _logLevel)
            {
                LogImpl(string.Format(formatString, args));
            }
        }


        #region ITraceLog Members

		public LogLevel LogLevel
		{
			get { return _logLevel; }
		}

        public virtual void Info(object logEntry)
        {
            Log(LogLevel.Info, logEntry);
        }

        public virtual void Info(object logEntry, Exception e)
        {
            Log(LogLevel.Info, logEntry, e);
        }

        public virtual void InfoFormat(string formatString, params object[] args)
        {
            Log(LogLevel.Info, formatString, args);
        }

        public virtual void Warn(object logEntry)
        {
            Log(LogLevel.Warning, logEntry);
        }

        public virtual void Warn(object logEntry, Exception e)
        {
            Log(LogLevel.Warning, logEntry, e);
        }

        public virtual void WarnFormat(string formatString, params object[] args)
        {
            Log(LogLevel.Warning, formatString, args);
        }

        public virtual void Error(object logEntry)
        {
            Log(LogLevel.Error, logEntry);
        }

        public virtual void Error(object logEntry, Exception e)
        {
            Log(LogLevel.Error, logEntry, e);
        }

        public virtual void ErrorFormat(string formatString, params object[] args)
        {
            Log(LogLevel.Error, formatString, args);
        }

        public virtual void Fatal(object logEntry)
        {
            Log(LogLevel.Fatal, logEntry);
        }

        public virtual void Fatal(object logEntry, Exception e)
        {
            Log(LogLevel.Fatal, logEntry, e);
        }

        public virtual void FatalFormat(string formatString, params object[] args)
        {
            Log(LogLevel.Fatal, formatString, args);
        }

        public virtual void Debug(object logEntry)
        {
            Log(LogLevel.Debug, logEntry);
        }

        public virtual void Debug(object logEntry, Exception e)
        {
            Log(LogLevel.Debug, logEntry, e);
        }

        public virtual void DebugFormat(string formatString, params object[] args)
        {
            Log(LogLevel.Debug, formatString, args);
        }

		public bool IsDebugEnabled
		{
			get { return (int)LogLevel >= (int)LogLevel.Debug; }
		}

		public bool IsInfoEnabled
		{
			get { return (int)LogLevel >= (int)LogLevel.Info; }
		}

		public bool IsWarnEnabled
		{
			get { return (int)LogLevel >= (int)LogLevel.Warning; }
		}

		public bool IsErrorEnabled
		{
			get { return (int)LogLevel >= (int)LogLevel.Error; }
		}

		public bool IsFatalEnabled
		{
			get { return (int)LogLevel >= (int)LogLevel.Fatal; }
		}

        #endregion
    }
}
