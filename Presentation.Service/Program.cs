using Presentation.Service.Jobs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Quartz;

namespace Presentation.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(serviceConfiguration => {
                serviceConfiguration.UseNLog();
                serviceConfiguration.ScheduleQuartzJobAsService(service => {
                    service.WithJob(() => JobBuilder.Create<FileCreationJob>().Build())
                           .AddTrigger(() => TriggerBuilder.Create()
                                            .WithSimpleSchedule(action =>
                                                action.WithIntervalInSeconds(5).RepeatForever())
                                            .Build());
                });
                serviceConfiguration.StartAutomatically();
                serviceConfiguration.EnableServiceRecovery(recoveryOptions =>
                {
                    recoveryOptions.RestartService(1);
                });
                serviceConfiguration.SetDisplayName("File creator Service");
                serviceConfiguration.SetDescription("Creates a file for the lulz");
                serviceConfiguration.SetServiceName("File genesis");
            });
        }
    }
}
