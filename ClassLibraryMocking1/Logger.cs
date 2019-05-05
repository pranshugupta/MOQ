using ClassLibraryMocking1.Interfaces;
using System;

namespace ClassLibraryMocking1
{

    public class Logger
    {
        private readonly ISrcubSensitiveData scrubSensitiveData;
        private readonly ICreateLogEntryHeaders createLogEntryHeaders;
        private readonly ICreateLogEntryFooter createLogEntryFooter;
        private readonly IConfigureSystem configureSystem;

        public Logger(ISrcubSensitiveData scrubSensitiveData,
                       ICreateLogEntryHeaders createLogEntryHeaders,
                       ICreateLogEntryFooter createLogEntryFooter,
                       IConfigureSystem configureSystem)
        {
            this.scrubSensitiveData = scrubSensitiveData;
            this.createLogEntryHeaders = createLogEntryHeaders;
            this.createLogEntryFooter = createLogEntryFooter;
            this.configureSystem = configureSystem;
        }

        public void Log(string message, LogLevel logLevel)
        {
            createLogEntryHeaders.For(logLevel);
            if (configureSystem.LogStackFor(logLevel))
            {
                Console.WriteLine("Stack /n/n {0}",
                    Environment.StackTrace);
            }

            Console.WriteLine("{0} - {1}", logLevel, scrubSensitiveData.From(message));
            createLogEntryFooter.For(logLevel);
        }
    }
}
