using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        var host = new HostBuilder()
        .ConfigureFunctionsWebApplication()
        .ConfigureServices(services =>
        {
            services.AddApplicationInsightsTelemetryWorkerService();
            services.ConfigureFunctionsApplicationInsights();
            services.AddSingleton<IGreetingTransient, GreetingServiceTransient>();
        })
        .Build();

        host.Run();
    }

    public interface IGreetingTransient
    {
        public string Greeting(string name);
        public int GetCount();
        public string DoSomething();
    }

    public class GreetingServiceTransient : IGreetingTransient
    {
        private int _count = 0;
        private string _uniqueId;
        private string _createdDateTime;

        public string DoSomething()
        {
            return "Doing something...";
        }

        public GreetingServiceTransient()
        {
            _uniqueId = Guid.NewGuid().ToString();

            // Format the datetime according to the desired format
            _createdDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string Greeting(string name)
        {
            _count++;
            return $"{name}! \n Count: {GetCount()}\nUniqueId: {_uniqueId}\nCreated: {_createdDateTime}";
        }

        public int GetCount()
        {
            return _count;
        }
    }
}