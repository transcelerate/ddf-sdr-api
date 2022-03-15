using Microsoft.Extensions.Logging;
using System;

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

        /// <summary>
        /// Logs Information
        /// </summary>
        /// <param name="message">The message will be logged</param>
        public void LogInformation(string message)
        {
            try
            {
                _logger.LogInformation(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Logs Warning
        /// </summary>
       /// <param name="message">The message will be logged</param>
        public void LogWarning(string message)
        {
            try
            {
                _logger.LogWarning(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Logs Error
        /// </summary>
       /// <param name="message">The message will be logged</param>
        public void LogError(string message)
        {
            try
            {
                _logger.LogError(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Logger: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Logs When debug logging is is added 
        /// </summary>
       /// <param name="message">The message will be logged</param>
        public void LogDebug(string message)
        {
            try
            {
                _logger.LogDebug(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Logs Critical Failures
        /// </summary>
       /// <param name="message">The message will be logged</param>
        public void LogCriitical(string message)
        {
            try
            {
                _logger.LogCritical(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Logs Traces
        /// </summary>
       /// <param name="message">The message will be logged</param>
        public void LogTrace(string message)
        {
            try
            {
                _logger.LogTrace(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

    }
}
