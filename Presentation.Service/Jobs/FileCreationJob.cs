using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf.Logging;

namespace Presentation.Service.Jobs
{
    class FileCreationJob : IJob
    {
        private static readonly LogWriter _log = HostLogger.Get<FileCreationJob>();

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                string now = DateTime.Now.ToString("mmddyyyyhhmmss");
                _log.Info($"File {now}.txt was created");
                File.AppendAllLines($@"C:\Files\{now}.txt", new[] { "Hellow world from Lima, Peru" });
                
            }
            catch(Exception exception)
            {
                _log.Error(exception);
            }
        }
    }
}
