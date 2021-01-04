using FindDuplicateBookings.Lib.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FindDuplicateBookings.ConsoleApp
{
    /// <summary>
    /// Class for background task to find and delete duplicate bookings.
    /// </summary>
    internal class DuplicateBookingService : IHostedService, IDisposable
    {
        private readonly IDuplicateBookingManager _duplicateBookingManager;
        private readonly ILogger _logger;
        private readonly ExecutionSettings _executionSettings;
        private Timer? _timer;

        /// <summary>
        /// Create a new object of DuplicateBookingService.
        /// </summary>
        /// <param name="duplicateBookingManager">Duplicate booking manager.</param>
        /// <param name="executionSettingsOptionsMonitor">Execution settings.</param>
        /// <param name="logger">Logger to log.</param>
        public DuplicateBookingService(
            IDuplicateBookingManager duplicateBookingManager,
            IOptionsMonitor<ExecutionSettings> executionSettingsOptionsMonitor,
            ILogger<DuplicateBookingService> logger)
        {
            _duplicateBookingManager = duplicateBookingManager;
            _logger = logger;
            _executionSettings = executionSettingsOptionsMonitor.CurrentValue;
        }

        /// <summary>
        /// Start the background task.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(_executionSettings.ExecutionFrequency));
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            try
            {
                _duplicateBookingManager.DeleteDuplicateBookingsAndSendMail();
            }
            catch (Exception ex)
            {
                _logger.DuplicateBookingsError(ex);
            }
        }

        /// <summary>
        /// Stop the background task.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
