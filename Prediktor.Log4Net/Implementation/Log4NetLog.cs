﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net.Repository.Hierarchy;

namespace Prediktor.Log
{
    public class Log4NetLog : ITraceLog
    {
        private log4net.ILog _logger = null;
        private string _logName = string.Empty;

        private log4net.ILog Logger { get { return _logger; } }

        //public string LogName 
        //{ 
        //    set 
        //    { 
        //        _logName = value;
        //        _logger = log4net.LogManager.GetLogger(_logName);
        //    } 
        //}

        public Log4NetLog(Assembly repositoryAssembly, string name)
        {
            _logName = name;
            _logger = log4net.LogManager.GetLogger(repositoryAssembly, name);
        }

        public Log4NetLog()
        {
        }

        // Summary:
        //     Configures the log4net.Repository.ILoggerRepository using the specified configuration
        //     file instead of using the default application config file. (for use with .Net Core)
        //
        // Parameters:
        //   entryAssembly:
        //     The assembly to use to lookup the repository.
        //
        //   configFile:
        //     The XML file to load the configuration from.
        //
        // Remarks:
        //     The configuration file must be valid XML. It must contain at least one element
        //     called log4net that holds the configuration data.
        //     The log4net configuration file can possible be specified in the application's
        //     configuration file (either MyAppName.exe.config for a normal application on Web.config
        //     for an ASP.NET application).
        //     The first element matching <configuration> will be read as the configuration.
        //     If this file is also a .NET .config file then you must specify a configuration
        //     section for the log4net element otherwise .NET will complain. Set the type for
        //     the section handler to System.Configuration.IgnoreSectionHandler, for example:
        //     The following example configures log4net using a configuration file, of which
        //     the location is stored in the application's configuration file :
        //     using log4net.Config; using System.IO; using System.Configuration; ... XmlConfigurator.Configure(new
        //     FileInfo(ConfigurationSettings.AppSettings["log4net-config-file"]));
        public static void Configure(Assembly entryAssembly, System.IO.FileInfo configFile)
        {
            var logRepository = log4net.LogManager.GetRepository(entryAssembly);
            log4net.Config.XmlConfigurator.Configure(logRepository, configFile);
        }

        public static void Configure(Assembly entryAssembly)
        {
            var logRepository = log4net.LogManager.GetRepository(entryAssembly);
            log4net.Config.XmlConfigurator.Configure(logRepository);
        }

        // Find a named appender already attached to a logger
        public static log4net.Appender.IAppender FindAppender(Assembly repositoryAssembly, string appenderName)
        {
            return log4net.LogManager.GetRepository(repositoryAssembly)
                    .GetAppenders().Where(a => a.Name == appenderName).FirstOrDefault();
        }

        public static T FindAppender<T>(Assembly repositoryAssembly)
        {
            return (T)log4net.LogManager.GetRepository(repositoryAssembly)
                    .GetAppenders().Where(a => a.GetType().Equals(typeof(T))).FirstOrDefault();
        }

        public virtual void Info(object logEntry)
        {
            Logger.Info(logEntry);
        }

        public virtual void Info(object logEntry, Exception e)
        {
            Logger.Info(logEntry, e);
        }

        public virtual void InfoFormat(string formatString, params object[] args)
        {
            Logger.InfoFormat(formatString, args);
        }

        public virtual void Warn(object logEntry)
        {
            Logger.Warn(logEntry);
        }

        public virtual void Warn(object logEntry, Exception e)
        {
            Logger.Warn(logEntry, e);
        }

        public virtual void WarnFormat(string formatString, params object[] args)
        {
            Logger.WarnFormat(formatString, args);
        }

        public virtual void Error(object logEntry)
        {
            Logger.Error(logEntry);
        }

        public virtual void Error(object logEntry, Exception e)
        {
            Logger.Error(logEntry, e);
        }

        public virtual void ErrorFormat(string formatString, params object[] args)
        {
            Logger.ErrorFormat(formatString, args);
        }

        public virtual void Fatal(object logEntry)
        {
            Logger.Fatal(logEntry);
        }

        public virtual void Fatal(object logEntry, Exception e)
        {
            Logger.Fatal(logEntry, e);
        }

        public virtual void FatalFormat(string formatString, params object[] args)
        {
            Logger.FatalFormat(formatString, args);
        }

        public virtual void Debug(object logEntry)
        {
            Logger.Debug(logEntry);
        }

        public virtual void Debug(object logEntry, Exception e)
        {
            Logger.Debug(logEntry, e);
        }

        public virtual void DebugFormat(string formatString, params object[] args)
        {
            Logger.DebugFormat(formatString, args);
        }

        public virtual bool IsDebugEnabled
        {
            get { return Logger.IsDebugEnabled; }
        }

        public virtual bool IsInfoEnabled
        {
            get { return Logger.IsInfoEnabled; }
        }

        public virtual bool IsWarnEnabled
        {
            get { return Logger.IsWarnEnabled; }
        }

        public virtual bool IsErrorEnabled
        {
            get { return Logger.IsErrorEnabled; }
        }

        public virtual bool IsFatalEnabled
        {
            get { return Logger.IsFatalEnabled; }
        }

    }
}
