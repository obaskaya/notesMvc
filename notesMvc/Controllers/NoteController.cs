using System.Linq;
using Microsoft.AspNetCore.Mvc;
using notesMvc.Data;
using notesMvc.Models;

namespace notesMvc.Controllers
{
    public class NoteController : Controller
    {
        private readonly NoteDbContext _context;

        public NoteController(NoteDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var notes = _context.Notes.ToList();
            return View(notes);
        }

        [HttpPost]
        public IActionResult Create(Note note)
        {
            if (ModelState.IsValid)
            {
                if (note.ParentId != null)
                {
                    var parent = _context.Notes.Find(note.ParentId);
                    if (parent != null)
                    {
                        parent.Children.Add(note);
                    }
                }
                else
                {
                    _context.Notes.Add(note);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(note);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = _context.Notes.Find(id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost]
        public IActionResult Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Notes.Update(note);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(note);
        }
    }
}
