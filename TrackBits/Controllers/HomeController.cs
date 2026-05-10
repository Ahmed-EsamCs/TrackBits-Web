using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrackBits.Models;
using TrackBits.Models.Entities;

namespace TrackBits.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Support(string query)
        {
            var faqs = new List<FaqViewModel>
    {
        new FaqViewModel { Id = "faq1", Question = "How do I reset my account password?", Answer = "You can reset your password by going to the Login page and clicking on 'Forgot Password'." },
        new FaqViewModel { Id = "faq2", Question = "Can I upgrade my subscription plan later?", Answer = "Yes! You can upgrade your subscription at any time from your account settings." },
        new FaqViewModel { Id = "faq3", Question = "Is my network data secure?", Answer = "Absolutely. TrackBiTs uses end-to-end encryption to keep your data safe." },
        new FaqViewModel { Id = "faq4", Question = "Can I cancel my subscription at any time?", Answer = "Yes, there are no long-term contracts. You can cancel anytime." },
        new FaqViewModel { Id = "faq5", Question = "I didn't receive my Serial Number?", Answer = "Please check your Spam/Junk folder. If not found, contact support." },
        new FaqViewModel { Id = "faq7", Question = "Can I monitor multiple networks with one account?\r\n", Answer = "Yes! Our standard plan allows you to monitor up to 5 different network nodes. For enterprise needs with unlimited nodes, please contact our sales team." },
        new FaqViewModel { Id = "faq6", Question = "What devices does TrackBiTs support?", Answer = "TrackBiTs is compatible with Windows 10/11, systems. You can also access the monitoring dashboard from desktop app." }


            };


            if (!string.IsNullOrEmpty(query))
            {
                faqs = faqs.Where(f => f.Question.Contains(query, StringComparison.OrdinalIgnoreCase)
                                    || f.Answer.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

                ViewData["CurrentQuery"] = query;
            }

            return View(faqs);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
