using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class StandardFontService : IFontService
    {
        private ILogger _logger;
        public StandardFontService(ILogger<StandardFontService> logger)
        {
            _logger = logger;
        }

        public string Render(string input)
        {
            _logger.LogInformation(input);
            return input;
        }
    }
}
