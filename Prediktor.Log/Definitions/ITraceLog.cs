using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prediktor.Log
{
    /// <summary>
    /// Describes the severity of the log entry
    /// </summary>
    public enum LogLevel : int
    {
        /// <summary>
        /// This is really bad. The system will most likely shut down.
        /// </summary>
        Fatal = 0,
        /// <summary>
        /// A severe error. This level should be used for non-intentional situations
        /// </summary>
        Error = 10,
        /// <summary>
        /// A problem was detected, but was handled. Indicative of a problem that should be solved
        /// </summary>
        Warning = 20,
        /// <summary>
        /// Informational message about something interesting that happened
        /// </summary>
        Info = 30,
        /// <summary>
        /// Low-level information. Normally only intented for developers.
        /// Debug messages will most likely not be generated/enabled for release builds.
        /// </summary>
        Debug = 100
    }
    /// <summary>
    /// Interface for logging run-time information about the system
    /// </summary>
    public interface ITraceLog
    {
        /// <summary>
        /// Generate an info-level <see cref="LogLevel.Info"/> logentry
        /// </summary>
        /// <param name="logEntry">The object containing the information to be logged</param>
        void Info(object logEntry /*, 
TODO: When we stop support for windows XP and require .NET runtime 4.0:
     When we switch to .NET 4.5 (Requires minimum Vista),
     we can add the following in order to automatically log source file / class member name and line number
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0*/);
        /// <summary>
        /// Generate an info-level <see cref="LogLevel.Info"/> logentry containing an exception
        /// </summary>
        /// <param name="logEntry">The object containing the information to be logged</param>
        /// <param name="e">The exception to be logged</param>
        void Info(object logEntry, Exception e);
        /// <summary>
        /// Generate an info-level <see cref="LogLevel.Info" logentry for a format string with parameters
        /// </summary>
        /// <param name="formatString">The string with placeholders to be logged <see cref="String.Format"/></param>
        /// <param name="args">The objects containing values to be put in the placeholders</param>
        void InfoFormat(string formatString, params object[] args);
        /// <summary>
        /// Generate an warning-level <see cref="LogLevel.Warning"/> logentry
        /// </summary>
        /// <param name="logEntry">The object containing the warning to be logged</param>
        void Warn(object logEntry);
        /// <summary>
        /// Generate an warning-level <see cref="LogLevel.Warning"/> logentry containing an exception
        /// </summary>
        /// <param name="logEntry">The object containing the warning to be logged</param>
        /// <param name="e">The exception to be logged</param>
        void Warn(object logEntry, Exception e);
        /// <summary>
        /// Generate an warning-level <see cref="LogLevel.Warning" logentry for a format string with parameters
        /// </summary>
        /// <param name="formatString">The string with placeholders to be logged <see cref="String.Format"/></param>
        /// <param name="args">The objects containing values to be put in the placeholders</param>
        void WarnFormat(string formatString, params object[] args);
        /// <summary>
        /// Generate an error-level <see cref="LogLevel.Error"/> logentry
        /// </summary>
        /// <param name="logEntry">The object containing the error-description to be logged</param>
        void Error(object logEntry);
        /// <summary>
        /// Generate an error-level <see cref="LogLevel.Error"/> logentry containing an exception
        /// </summary>
        /// <param name="logEntry">The object containing the error-description to be logged</param>
        /// <param name="e">The exception to be logged</param>
        void Error(object logEntry, Exception e);
        /// <summary>
        /// Generate an error-level <see cref="LogLevel.Error" logentry for a format string with parameters
        /// </summary>
        /// <param name="formatString">The string with placeholders to be logged <see cref="String.Format"/></param>
        /// <param name="args">The objects containing values to be put in the placeholders</param>
        void ErrorFormat(string formatString, params object[] args);
        /// <summary>
        /// Generate an fatal-level <see cref="LogLevel.Fatal"/> logentry
        /// </summary>
        /// <param name="logEntry">The object containing the description of the fatal event to be logged</param>
        void Fatal(object logEntry);
        /// <summary>
        /// Generate a fatal-level <see cref="LogLevel.Fatal"/> logentry containing an exception
        /// </summary>
        /// <param name="logEntry">The object containing the description of the fatal event to be logged</param>
        /// <param name="e">The exception to be logged</param>
        void Fatal(object logEntry, Exception e);
        /// <summary>
        /// Generate a fatal-level <see cref="LogLevel.Fatal"/> logentry for a format string with parameters
        /// </summary>
        /// <param name="formatString">The string with placeholders to be logged <see cref="String.Format"/></param>
        /// <param name="args">The objects containing values to be put in the placeholders</param>
        void FatalFormat(string formatString, params object[] args);
        /// <summary>
        /// Generate an debug-level <see cref="LogLevel.Debug"/> logentry
        /// </summary>
        /// <param name="logEntry">The object containing the debug-information to be logged</param>
        void Debug(object logEntry);
        /// <summary>
        /// Generate a debug-level <see cref="LogLevel.Debug"/> logentry containing an exception
        /// </summary>
        /// <param name="logEntry">The object containing the debug information to be logged</param>
        /// <param name="e">The exception to be logged</param>
        void Debug(object logEntry, Exception e);
        /// <summary>
        /// Generate a debug-level <see cref="LogLevel.Debug"/> logentry for a format string with parameters
        /// </summary>
        /// <param name="formatString">The string with placeholders to be logged <see cref="String.Format"/></param>
        /// <param name="args">The objects containing values to be put in the placeholders</param>
        void DebugFormat(string formatString, params object[] args);
        /// <summary>
        /// Wether debug-level <see cref="LogLevel.Debug"/> log-entries will be generated by this logger
        /// </summary>
        bool IsDebugEnabled { get; }
        /// <summary>
        /// Wether info-level <see cref="LogLevel.Info"/> log-entries will be generated by this logger
        /// </summary>
        bool IsInfoEnabled { get; }
        /// <summary>
        /// Wether warning-level <see cref="LogLevel.Warning"/> log-entries will be generated by this logger
        /// </summary>
        bool IsWarnEnabled { get; }
        /// <summary>
        /// Wether error-level <see cref="LogLevel.Error"/> log-entries will be generated by this logger
        /// </summary>
        bool IsErrorEnabled { get; }
        /// <summary>
        /// Wether fatal-level <see cref="LogLevel.Fatal"/> log-entries will be generated by this logger
        /// </summary>
        bool IsFatalEnabled { get; }
    }
    /// <summary>
    /// Interface for instantiating trace loggers
    /// </summary>
    public interface ITraceLogFactory
    {
        /// <summary>
        /// Get a logger with the given name as logsource
        /// </summary>
        /// <param name="logName">The name of the log (logsource)</param>
        /// <returns>A logger interface</returns>
        ITraceLog GetLogger(string logName);
        /// <summary>
        /// Get a logger with a name defined by a type as logsource
        /// </summary>
        /// <param name="typeName">The type identifying the name of the log (logsource)</param>
        /// <returns>A logger interface</returns>
        ITraceLog GetLogger(Type typeName);
    }
}
