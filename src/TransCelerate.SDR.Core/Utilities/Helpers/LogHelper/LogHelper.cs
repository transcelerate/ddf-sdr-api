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
                _logger.LogInformation("{message}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
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
                _logger.LogWarning("{message}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
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
                _logger.LogError("{message}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Logger: {ex}", ex);
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
                _logger.LogDebug("{message}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
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
                _logger.LogCritical("{message}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
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
                _logger.LogTrace("{message}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
                throw;
            }
        }

    }
}
