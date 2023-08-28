using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class StandardFontService : IFontService
    {
        public string Render(string input)
        {
            return input;
        }
    }
}
