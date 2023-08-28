using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class SubzeroFontService : IFontService
    {
        public SubzeroFontService()
        {
        }

        public string Render(string input)
        {
            return Figgle.FiggleFonts.SubZero.Render(input);
        }
    }
}
