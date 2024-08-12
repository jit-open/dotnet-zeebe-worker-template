using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;
using Zeebe.Client.Impl.Builder;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DotNet_Camunda8_getting_started 
{
    class ZeebeClient
    {
        private static readonly String _ClientID = "your-client-id";
        private static readonly String _ClientSecret = "your-client-secret";
        private static readonly String _ContactPoint = "your-cluster-id.bru-2.zeebe.camunda.io:443";
        private static readonly String _JobType = "Hello_World"; 

        public static IZeebeClient zeebeClient;

        static async Task Main(string[] args)
        {
            zeebeClient = CamundaCloudClientBuilder
                                .Builder()
                                .UseClientId(_ClientID)
                                .UseClientSecret(_ClientSecret)
                                .UseContactPoint(_ContactPoint)
                                .Build();
            

            // Starting the Job Worker
            using (var signal = new EventWaitHandle(false, EventResetMode.AutoReset))
            {
                zeebeClient.NewWorker()
                         .JobType(_JobType)
                         .Handler(HelloWorld)
                         .MaxJobsActive(5)
                         .Name(Environment.MachineName)
                         .AutoCompletion()
                         .PollInterval(TimeSpan.FromSeconds(1))
                         .Timeout(TimeSpan.FromSeconds(10))
                         .Open();

                signal.WaitOne();
            }
        }

        /// <summary>
        /// Business Logic called by the Job Worker goes here 
        /// </summary>
        /// <param name=jobClient">The client with access to all job-related operation</param>
        /// <param name=job">Job Object</param>
        private static void HelloWorld(IJobClient jobClient, IJob job)
        {
            JObject jsonObject = JObject.Parse(job.Variables);
            

            Console.WriteLine("Hello World: Working on Task");
            jobClient.NewCompleteJobCommand(job.Key)
                    .Send()
                    .GetAwaiter()
                    .GetResult();
            Console.WriteLine("Completed the fetched Task");
        }  
    }
}
