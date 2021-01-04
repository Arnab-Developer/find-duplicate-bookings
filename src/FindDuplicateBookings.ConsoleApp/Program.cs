using FindDuplicateBookings.ConsoleApp;
using FindDuplicateBookings.Lib;
using FindDuplicateBookings.Lib.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FindDuplicateBookings.ConsoleAppTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

using IHost host = CreateHostBuilder().Build();
await host.StartAsync();
await host.WaitForShutdownAsync();

static IHostBuilder CreateHostBuilder() =>
    Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            services
                .AddTransient<IDuplicateBooking>(options =>
                {
                    string conStr = context.Configuration.GetConnectionString("ConStr");
                    DuplicateBooking duplicateBooking = new(conStr);
                    return duplicateBooking;
                })
                .AddTransient<ISendMail>(options =>
                {
                    MailSettings mailSettings = new()
                    {
                        From = context.Configuration.GetValue<string>("MailSettings:From"),
                        To = context.Configuration.GetValue<string>("MailSettings:To"),
                        Subject = context.Configuration.GetValue<string>("MailSettings:Subject"),
                        Host = context.Configuration.GetValue<string>("MailSettings:Host"),
                        Port = context.Configuration.GetValue<int>("MailSettings:Port"),
                        UserName = context.Configuration.GetValue<string>("MailSettings:UserName"),
                        Password = context.Configuration.GetValue<string>("MailSettings:Password"),
                        EnableSsl = context.Configuration.GetValue<string>("MailSettings:EnableSsl")
                    };
                    SendMail sendMail = new(mailSettings);
                    return sendMail;
                })
                .AddTransient(typeof(IDuplicateBookingManager), typeof(DuplicateBookingManager))
                .Configure<DuplicateBookingDateRange>(context.Configuration.GetSection("DuplicateBookingDateRange"))
                .Configure<DeleteSettings>(context.Configuration)
                .Configure<ExecutionSettings>(context.Configuration)
                .AddHostedService<DuplicateBookingService>();
        })
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddEventLog(configuration => configuration.SourceName = "FindDuplicateBookings");
        });