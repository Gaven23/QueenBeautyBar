using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QueenBeautyBar.Data;

namespace QueenBeautyBar.Controllers
{
    public class UsersController : Controller
    {
        dbsQueenzBeautyBarContext _context = new dbsQueenzBeautyBarContext();
        // GET: Users
        public async Task<IActionResult> Index()
        {
            var dbsQueenzBeautyBarContext = _context.Users.Include(u => u.Appointmnet).Include(u => u.Gender).Include(u => u.Race).Include(u => u.Role);
            return View(await dbsQueenzBeautyBarContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Appointmnet)
                .Include(u => u.Gender)
                .Include(u => u.Race)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["AppointmnetId"] = new SelectList(_context.Appointments, "AppointmnetId", "Email");
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderDecription");
            ViewData["RaceId"] = new SelectList(_context.Race, "RaceId", "RaceDescription");
            ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleDescription");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId,AppointmnetId,UsersToken,Loginname,Password,GenderId,RaceId,Firstname,Lastname,EmailAddress,MobileNo,PhysicalAddress,LastModifiedDate,CreateDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmnetId"] = new SelectList(_context.Appointments, "AppointmnetId", "Email", users.AppointmnetId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderDecription", users.GenderId);
            ViewData["RaceId"] = new SelectList(_context.Race, "RaceId", "RaceDescription", users.RaceId);
            ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleDescription", users.RoleId);
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            ViewData["AppointmnetId"] = new SelectList(_context.Appointments, "AppointmnetId", "Email", users.AppointmnetId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderDecription", users.GenderId);
            ViewData["RaceId"] = new SelectList(_context.Race, "RaceId", "RaceDescription", users.RaceId);
            ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleDescription", users.RoleId);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,RoleId,AppointmnetId,UsersToken,Loginname,Password,GenderId,RaceId,Firstname,Lastname,EmailAddress,MobileNo,PhysicalAddress,LastModifiedDate,CreateDate")] Users users)
        {
            if (id != users.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmnetId"] = new SelectList(_context.Appointments, "AppointmnetId", "Email", users.AppointmnetId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderDecription", users.GenderId);
            ViewData["RaceId"] = new SelectList(_context.Race, "RaceId", "RaceDescription", users.RaceId);
            ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleDescription", users.RoleId);
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Appointmnet)
                .Include(u => u.Gender)
                .Include(u => u.Race)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
