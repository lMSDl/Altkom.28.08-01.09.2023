using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class MuzzleFontService : IFontService
    {

        public string Render(string input)
        {
            return Figgle.FiggleFonts.Muzzle.Render(input);
        }
    }
}
