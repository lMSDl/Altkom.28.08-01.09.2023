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

        public ConsoleOutputService(IFontService fontService)
        {
            _fontService = fontService;
        }

        public void WriteText(string text)
        {
            Console.WriteLine(_fontService.Render(text));
        }
    }
}
