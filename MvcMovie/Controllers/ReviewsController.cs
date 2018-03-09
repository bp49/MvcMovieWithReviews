using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly MvcMovieContext _context;

        public ReviewsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(string OrderBy, string Direction)
        {
           
            var mvcMovieContext = _context.Reviews.Include(r => r.Movie);
            var ReviewsSort = from r in _context.Reviews
                              select r;
            var MovieSort= from m in _context.Movie
                           select m;

            if (OrderBy =="Reviewer")
            {
                if(Direction == "Desc")
                {
                    ReviewsSort = ReviewsSort.OrderByDescending(r => r.Reviewer);
                }
                else
                {
                    ReviewsSort = ReviewsSort.OrderBy(r => r.Reviewer);
                }
            }
            if(OrderBy== "Movie")
            {
                if (Direction == "Desc")
                {
                    MovieSort = MovieSort.OrderByDescending(r => r.Title);
                }
                else
                {
                    MovieSort = MovieSort.OrderBy(r => r.Title);
                }
            }

                return View(await mvcMovieContext.ToListAsync());

            
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Movie)
                .SingleOrDefaultAsync(m => m.ReviewsID == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // GET: Reviews/Create
        public IActionResult Create(int? id)
        {
            ViewData["MovieCreate"] = _context.Movie.SingleOrDefault(M => M.ID == id);
            return View();;
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("ReviewsID,Reviewer,Review,MovieID")] Reviews reviews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reviews);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Movies", new { ID = reviews.MovieID });
            }
            ViewData["MovieID"] = new SelectList(_context.Movie, "ID", "Title", reviews.MovieID);
            return View(reviews);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews.SingleOrDefaultAsync(m => m.ReviewsID == id);
            if (reviews == null)
            {
                return NotFound();
            }
            ViewData["MovieID"] = new SelectList(_context.Movie, "ID", "Title", reviews.MovieID);
            return View(reviews);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewsID,Reviewer,Review,MovieID")] Reviews reviews)
        {
            if (id != reviews.ReviewsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewsExists(reviews.ReviewsID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Movies", new { id = reviews.MovieID });
            }
            ViewData["MovieID"] = new SelectList(_context.Movie, "ID", "Title", reviews.MovieID);
            return View(reviews);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Movie)
                .SingleOrDefaultAsync(m => m.ReviewsID == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviews = await _context.Reviews.SingleOrDefaultAsync(m => m.ReviewsID == id);
            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Movies", new { ID = reviews.MovieID });
        }

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewsID == id);
        }
    }
}
