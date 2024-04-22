using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using static Program;

namespace depinjFA
{
    public class http1
    {
        private readonly ILogger<http1> _logger;
        private readonly IGreetingTransient _greetingServiceTransient;

        public http1(ILogger<http1> logger, IGreetingTransient greetingServiceTransient)
        {
            _logger = logger;
            _greetingServiceTransient = greetingServiceTransient;
        }

        [Function("http1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            var result = _greetingServiceTransient.Greeting("Azure Functions");

            _logger.LogInformation(result);
            return new OkObjectResult(result);
        }
    }
}
