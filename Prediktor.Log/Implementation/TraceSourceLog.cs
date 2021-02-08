using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Prediktor.Log;

namespace Prediktor.Log.Implementation
{
	public class TraceSourceLog : ITraceLog
	{
		private LogLevel _logLevel;
		private TraceSource _traceSource = null;

		private static SourceLevels ConvertToSourceLevels(LogLevel logLevel)
		{
			switch (logLevel)
			{
				case LogLevel.Debug:
					return SourceLevels.Verbose;
				case LogLevel.Error:
					return SourceLevels.Error;
				case LogLevel.Fatal:
					return SourceLevels.Critical;
				case LogLevel.Info:
					return SourceLevels.Information;
				case LogLevel.Warning:
					return SourceLevels.Warning;
			}
			return SourceLevels.Verbose;
		}

		private static LogLevel ConvertToLogLevel(SourceLevels logLevel)
		{
			switch (logLevel)
			{
				case SourceLevels.Verbose:
					return LogLevel.Debug;
				case SourceLevels.Error:
					return LogLevel.Error;
				case SourceLevels.Critical:
					return LogLevel.Fatal;
				case SourceLevels.Information:
					return LogLevel.Info;
				case SourceLevels.Warning:
					return LogLevel.Warning;
			}
			return LogLevel.Debug;
		}



		public TraceSourceLog(string name, LogLevel logLevel)
			: this(name, ConvertToSourceLevels(logLevel), null)
		{
		}


		public TraceSourceLog(string name, LogLevel logLevel, TraceSourceLog defaultTraceLog)
			: this(name, ConvertToSourceLevels(logLevel), defaultTraceLog)
		{
		}

		public TraceSourceLog(string name, SourceLevels logLevel)
			: this(name, logLevel, null)
		{
		}

		public TraceSourceLog(string name, SourceLevels logLevel, TraceSourceLog defaultTraceLog)
		{
			_logLevel = ConvertToLogLevel(logLevel);
			_traceSource = new TraceSource(name, logLevel);
			int defaultTraceListener = _traceSource.Listeners.OfType<DefaultTraceListener>().Count();
			if ((defaultTraceListener == _traceSource.Listeners.Count) && defaultTraceLog != null && defaultTraceLog._traceSource.Listeners.Count > 0) // Adds only to those sources without listeners.
			{
				_traceSource.Listeners.AddRange(defaultTraceLog._traceSource.Listeners);
			}
		}

		public LogLevel LogLevel
		{
			get { return _logLevel; }
		}


		private void Trace(TraceEventType eventType, int id, string message, params object[] parameters)
		{
			if (parameters.Length > 0)
				_traceSource.TraceEvent(eventType, id, message, parameters);
			else
				_traceSource.TraceEvent(eventType, id, message);
			//_traceSource.Flush();
		}

		private static string ToString(object o)
		{
			return (o ?? string.Empty).ToString();
		}

		private static string ToString(object o, Exception e)
		{
			return string.Format("{0} - Exception: {1}", o, ToString(e));
		}

		#region ITraceLog Members

		public void Info(object logEntry)
		{
			Trace(TraceEventType.Information, 0, ToString(logEntry));
		}

		public void Info(object logEntry, Exception e)
		{
			Trace(TraceEventType.Information, 0, ToString(logEntry, e));
		}

        public virtual void InfoFormat(string formatString, params object[] args)
		{
			Trace(TraceEventType.Information, 0, formatString, args);
		}

        public virtual void Warn(object logEntry)
		{
			Trace(TraceEventType.Warning, 0, ToString(logEntry));
		}

        public virtual void Warn(object logEntry, Exception e)
		{
			Trace(TraceEventType.Warning, 0, ToString(logEntry, e));
		}

        public virtual void WarnFormat(string formatString, params object[] args)
		{
			Trace(TraceEventType.Warning, 0, formatString, args);
		}

        public virtual void Error(object logEntry)
		{
			Trace(TraceEventType.Error, 0, ToString(logEntry));
		}

        public virtual void Error(object logEntry, Exception e)
		{
			Trace(TraceEventType.Error, 0, ToString(logEntry, e));
		}

        public virtual void ErrorFormat(string formatString, params object[] args)
		{
			Trace(TraceEventType.Error, 0, formatString, args);
		}

        public virtual void Fatal(object logEntry)
		{
			Trace(TraceEventType.Critical, 0, ToString(logEntry));
		}

        public virtual void Fatal(object logEntry, Exception e)
		{
			Trace(TraceEventType.Critical, 0, ToString(logEntry, e));
		}

        public virtual void FatalFormat(string formatString, params object[] args)
		{
			Trace(TraceEventType.Critical, 0, formatString, args);
		}

        public virtual void Debug(object logEntry)
		{
			Trace(TraceEventType.Verbose, 0, ToString(logEntry));
		}

		public void Debug(object logEntry, Exception e)
		{
			Trace(TraceEventType.Verbose, 0, ToString(logEntry, e));
		}

        public virtual void DebugFormat(string formatString, params object[] args)
		{
			Trace(TraceEventType.Verbose, 0, formatString, args);
		}

		#endregion


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
	}

}
