using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities
{
    public class LogHelper : ILogHelper
    {
        private readonly ILogger _logger;

        public LogHelper(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger(Constants.LogConstant.Application);
        }

        public void LogInformation(string message)
        {
            try
            {
                _logger.LogInformation(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
        public void LogWarning(string message)
        {
            try
            {
                _logger.LogWarning(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public void LogError(string message)
        {
            try
            {
                _logger.LogError(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Logger: {ex.Message}");
                throw ex;
            }
        }

        public void LogDebug(string message)
        {
            try
            {
                _logger.LogDebug(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public void LogCriitical(string message)
        {
            try
            {
                _logger.LogCritical(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public void LogTrace(string message)
        {
            try
            {
                _logger.LogTrace(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

    }
}
