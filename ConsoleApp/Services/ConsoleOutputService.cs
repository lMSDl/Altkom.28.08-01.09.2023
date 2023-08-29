using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class ConsoleOutputService : IOutputService
    {

        private IFontService _fontService;
        protected ILogger _logger;

        public ConsoleOutputService(IFontService fontService, ILogger<ConsoleOutputService> logger)
        {
            _fontService = fontService;
            _logger = logger;

            _logger.LogDebug("Konstruktor ConsoleOutputService");
        }

        public void WriteText(string text)
        {
            Console.WriteLine(_fontService.Render(text));
        }
    }
}
