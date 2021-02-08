using System;
using System.Diagnostics.Contracts;

namespace Prediktor.Log
{
	/// <summary>
	/// Utility class for issuing start and end log-messages around a code-block, including the time spent executing the block
	/// Use with a using-construct to handle automatic disposal of the scopelog instance
	/// <code>
	/// using(new ScopeLog(LogManager.GetLogger("someLogger"), MethodBase.GetCurrentMethod().Name)
	/// {
	///		// Your codeblock goes here
	/// }
	/// </code>
	/// </summary>
	public class ScopeLog : IDisposable
	{
		ITraceLog _log;
		string _message;
		string _enterPrefix = "Entering";
		string _leavePrefix = "Leaving";
		string _methodName;
		bool _includeConsole = false;
		DateTime _start;
		private void DoLog(string logMessage)
		{
			_log.Debug(logMessage);
			if (_includeConsole)
				Console.WriteLine(logMessage);
		}
		private void LogEntering()
		{
			var logMessage = string.IsNullOrEmpty(_message) ?
				$"{_enterPrefix} method {_methodName}" :
				$"{_enterPrefix} method {_methodName} : {_message}";
			DoLog(logMessage);
		}
		private void LogLeaving()
		{
			var duration = TimeSpan.FromTicks(DateTime.Now.Ticks - _start.Ticks);
			var logMessage = string.IsNullOrEmpty(_message) ?
				$"{_leavePrefix} method {_methodName} : {duration}" :
				$"{_leavePrefix} method {_methodName} : {_message} : {duration}";
			DoLog(logMessage);
		}
		/// <summary>
		/// Create a scope logger for the current scope
		/// </summary>
		/// <param name="log">The tracelog that will be used to emit the messages</param>
		/// <param name="method">The current method (or other name identifying the codeblock)</param>
		/// <param name="message">An optional message to be included at the beginning and end of the codeblock</param>
		/// <param name="enterPrefix">An optional prefix to replace the default "Entering" prefix</param>
		/// <param name="leavePrefix">An optional prefix to replace the default "Leaving" prefix</param>
		/// <param name="includeConsole">Optional flag for also emitting the log messages to system.console</param>
		public ScopeLog(ITraceLog log, string method, string message = null, string enterPrefix = null, string leavePrefix = null, bool includeConsole = false)
		{
			Contract.Assert(log != null, "A trace logger must be specified");
			Contract.Assert(!string.IsNullOrEmpty(method), "Methodname must be specified");

			_includeConsole = includeConsole;
			_log = log;
			_methodName = method;
			_message = message;
			_start = DateTime.Now;

			if (!string.IsNullOrEmpty(enterPrefix))
				_enterPrefix = enterPrefix;
			if (!string.IsNullOrEmpty(leavePrefix))
				_leavePrefix = leavePrefix;

			Contract.Ensures(_log != null);
			Contract.Ensures(!string.IsNullOrEmpty(_enterPrefix));
			Contract.Ensures(!string.IsNullOrEmpty(_leavePrefix));
			Contract.Ensures(!string.IsNullOrEmpty(_methodName));

			LogEntering();
		}
		public void Dispose()
		{
			LogLeaving();
		}
	}
}
