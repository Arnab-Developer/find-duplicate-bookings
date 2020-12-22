using FindDuplicateBookings.ConsoleApp;
using FindDuplicateBookings.Lib;
using FindDuplicateBookings.Lib.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FindDuplicateBookings.ConsoleAppTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

IHost host = CreateHostBuilder().Build();

var duplicateBookingController = ActivatorUtilities.CreateInstance<DuplicateBookingController>(host.Services);
try
{
    duplicateBookingController.DeleteDuplicateBookingsAndSendMail();
}
catch (Exception ex)
{
    var logger = host.Services.GetRequiredService<ILogger<DuplicateBookingController>>();
    logger.DuplicateBookingsError(ex);
}

static IHostBuilder CreateHostBuilder() =>
    Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            services
                .AddTransient<IDuplicateBooking>(option =>
                {
                    string conStr = context.Configuration.GetConnectionString("ConStr");
                    DuplicateBooking duplicateBooking = new(conStr);
                    return duplicateBooking;
                })
                .Configure<DuplicateBookingDateRange>(context.Configuration);
        })
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddEventLog(configuration => configuration.SourceName = "FindDuplicateBookings");
        });