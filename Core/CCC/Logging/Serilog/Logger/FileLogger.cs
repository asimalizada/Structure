﻿using Core.CCC.Logging.Serilog.ConfigurationModels;
using Core.CCC.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Core.CCC.Logging.Serilog.Logger
{
    public class FileLogger : LoggerServiceBase
    {
        private IConfiguration _configuration;

        public FileLogger(IConfiguration configuration)
        {
            _configuration = configuration;

            FileLogConfiguration logConfig = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
                                                          .Get<FileLogConfiguration>() ??
                                             throw new System.Exception(SerilogMessages.NullOptionsMessage);

            string logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + logConfig.FolderPath, ".txt");

            Logger = new LoggerConfiguration()
                     .WriteTo.File(
                         logFilePath,
                         rollingInterval: RollingInterval.Day,
                         retainedFileCountLimit: null,
                         fileSizeLimitBytes: 5000000,
                         outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                     .CreateLogger();
        }
    }
}