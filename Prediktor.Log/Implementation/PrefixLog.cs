using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;

namespace Prediktor.Log
{
    /// <summary>
    /// Utility class for including a static prefix before all log-messages
    /// Typical use could be for following a call context from an rpc entry until the rpc-call is completed
    /// <code>
    /// this._log = new PrefixLog(LogManager.GetLogger("someLogger"), Guid.NewGuid(), "{{{0}}}-{1}");
    /// </code>
    /// </summary>
    public class PrefixLog : ITraceLog
    {
        private ITraceLog _log;
        private string _prefix;
        private string _format;
        /// <summary>
        /// Count the number of placeholders in the provided format string
        /// </summary>
        /// <param name="formatString">The format string to check</param>
        /// <returns>The number of placeholders in the format string</returns>
        private static int CountParameters(string formatString)
        {
            const string pattern = @"(?<!\{)(?>\{\{)*\{\d(.*?)";

            var matches = Regex.Matches(formatString, pattern);
            var totalMatchCount = matches.Count;
            var uniqueMatchCount = matches.OfType<Match>().Select(m => m.Value).Distinct().Count();
            var parameterMatchCount = (uniqueMatchCount == 0) ? 0 : matches.OfType<Match>().Select(m => m.Value).Distinct().Select(m => int.Parse(m.Replace("{", string.Empty))).Max() + 1;
            
            return parameterMatchCount;
        }
        /// <summary>
        /// Create a prefix logger for the current call-graph
        /// </summary>
        /// <param name="log">The tracelog that will be used to emit the messages</param>
        /// <param name="prefix">The prefix to prepend all log-messages</param>
        /// <param name="format">A format string that specifies how the prefix should be prepended to all messages.
        /// Note that the prefix is using index 0 for the placeholders</param>
        public PrefixLog(ITraceLog log, object prefix, string format)
        {
            Contract.Assert(log != null, "You have to specify an actual logger");
            Contract.Assert(prefix != null, "You have to specify a prefix object");
            int numParameters = string.IsNullOrEmpty(format) ? 0 : CountParameters(format);
            Contract.Assert(numParameters == 2, "You have to specify a format with 2 parameters");

            _log = log;
            _prefix = prefix.ToString();
            _format = format;

            Contract.Ensures(_log != null);
            Contract.Ensures(_prefix != null);
            Contract.Ensures(_format != null);
        }

        public bool IsDebugEnabled => _log.IsDebugEnabled;

        public bool IsInfoEnabled => _log.IsInfoEnabled;

        public bool IsWarnEnabled => _log.IsWarnEnabled;

        public bool IsErrorEnabled => _log.IsErrorEnabled;

        public bool IsFatalEnabled => _log.IsFatalEnabled;

        private string Combine(object logEntry)
        {
            return string.Format(_format, _prefix, logEntry);
        }
        public void Debug(object logEntry)
        {
            _log.Debug(Combine(logEntry));
        }

        public void Debug(object logEntry, Exception e)
        {
            _log.Debug(Combine(logEntry), e);
        }

        public void DebugFormat(string formatString, params object[] args)
        {
            _log.Debug(Combine(string.Format(formatString, args)));
        }

        public void Error(object logEntry)
        {
            _log.Error(Combine(logEntry));
        }

        public void Error(object logEntry, Exception e)
        {
            _log.Error(Combine(logEntry), e);
        }

        public void ErrorFormat(string formatString, params object[] args)
        {
            _log.Error(Combine(string.Format(formatString, args)));
        }

        public void Fatal(object logEntry)
        {
            _log.Error(Combine(logEntry));
        }

        public void Fatal(object logEntry, Exception e)
        {
            _log.Fatal(Combine(logEntry), e);
        }

        public void FatalFormat(string formatString, params object[] args)
        {
            _log.Fatal(Combine(string.Format(formatString, args)));
        }

        public void Info(object logEntry)
        {
            _log.Info(Combine(logEntry));
        }

        public void Info(object logEntry, Exception e)
        {
            _log.Info(Combine(logEntry), e);
        }

        public void InfoFormat(string formatString, params object[] args)
        {
            _log.Info(Combine(string.Format(formatString, args)));
        }

        public void Warn(object logEntry)
        {
            _log.Warn(Combine(logEntry));
        }

        public void Warn(object logEntry, Exception e)
        {
            _log.Warn(Combine(logEntry), e);
        }

        public void WarnFormat(string formatString, params object[] args)
        {
            _log.Warn(Combine(string.Format(formatString, args)));
        }
    }
}
