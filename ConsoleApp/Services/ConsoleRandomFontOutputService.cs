using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class ConsoleRandomFontOutputService : ConsoleOutputService, IOutputService
    {
        public ConsoleRandomFontOutputService(IEnumerable<IFontService> fontServices, ILogger<ConsoleRandomFontOutputService> logger)
            : base(fontServices.ToList()[new Random().Next(0, fontServices.Count())], logger)
        {

            _logger.LogDebug("Konstruktor ConsoleRandomFontOutputService");
        }



    }
}
