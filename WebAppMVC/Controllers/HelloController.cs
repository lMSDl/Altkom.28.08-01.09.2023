using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace WebAppMVC.Controllers
{
    [Authorize]
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello world", "text/html");
        }

        //nazwa parametru == nazwa parametru w routingu => wartosc pobierana z path
        //nazwa parametru != nazwa z routingu => wartość pobierana z query
        public IActionResult Hi(string name, int age)
        {
            return Content($"Hello {name}. Your age is {age}", "text/html");
        }

        public IActionResult Encode(string name)
        {
            return Content(HttpUtility.HtmlEncode($"<b>Hello</b> {name}"), "text/html");
        }
    }
}
