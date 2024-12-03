using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Posts
                                .Include(p => p.Author)
                                .OrderByDescending(p => p.PublishDate)
                                .Select(s => new PostViewModel
                                {
                                    Id = s.Id,
                                    Title = s.Title,
                                    Content = s.Content,
                                    Author = s.Author,
                                    PublishDate = s.PublishDate
                                })
                                .ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                                .Include(p => p.Author)
                                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(new PostViewModel() { Id = post.Id, Title = post.Title, Content = post.Content, PublishDate = post.PublishDate, Author = post.Author });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
