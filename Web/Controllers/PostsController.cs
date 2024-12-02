using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Interfaces;
using Web.Models;
using Web.Models.Identities;

namespace Web.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubConnectionService _hubConnectionService;

        public PostsController(ApplicationDbContext context, IHubConnectionService hubConnectionService)
        {
            _context = context;
            _hubConnectionService = hubConnectionService;
        }

        // GET: Posts
        public async Task<IActionResult> Index(string authorId)
        {
            if (string.IsNullOrWhiteSpace(authorId))
            {
                return RedirectToAction("Index", "Home");
            }
            var list = await _context.Posts
                                .Where(p => p.AuthorId!.Equals(authorId))
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

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,AuthorId")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();

                await _hubConnectionService.SendNotificationAsync($"Novo post criado: {post.Title}");
                
                return RedirectToAction("Index", "Posts", routeValues: post.AuthorId);
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Content,AuthorId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Posts", routeValues: post.AuthorId);
            }

            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Posts", new { AuthorId = post?.AuthorId });
        }

        private bool PostExists(long id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
