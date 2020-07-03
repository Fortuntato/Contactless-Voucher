﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactlessLoyaltyWebApp.Data;
using ContactlessLoyaltyWebApp.Models;
using System.Security.Cryptography;
using ContactlessLoyaltyWebApp.Security;

namespace ContactlessLoyaltyWebApp.Controllers
{
    public class UserController : Controller
    {
        private const int PBKDF2IterCount = 1000; // default for Rfc2898DeriveBytes
        private const int PBKDF2SubkeyLength = 256 / 8; // 256 bits
        private const int SaltSize = 128 / 8; // 128 bits

        private readonly LoyaltyDatabaseContext _context;

        public UserController(LoyaltyDatabaseContext context)
        {
            _context = context;
        }

        // GET: UserModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: UserModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: UserModels/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: UserModels/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                // Generate new Id for the user
                userModel.ID = GenerateUserID();
                // Hash the password before storing it into the DB
                userModel.Password = Crypto.HashPassword(userModel.Password);

                LoyaltyCardModel newCard = new LoyaltyCardModel();
                newCard.ID = GenerateCardID();
                newCard.User = userModel;

                userModel.LoyaltyCard = newCard;

                _context.Add(userModel);
                _context.Add(newCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: UserModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: UserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CardID,Name,MobilePhone,Password")] UserModel userModel)
        {
            if (id != userModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.ID))
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
            return View(userModel);
        }

        // GET: UserModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userModel = await _context.Users.FindAsync(id);
            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }

        private bool CardModelExists(int id)
        {
            return _context.LoyaltyCards.Any(e => e.ID == id);
        }

        private int GenerateUserID()
        {
            Random rnd = new Random();
            int id = rnd.Next(1, 1000000);
            // Loop until a new id is created
            while (UserModelExists(id))
            {
                id = rnd.Next(1, 1000000);
            }
            return id;
        }

        private int GenerateCardID()
        {
            Random rnd = new Random();
            int id = rnd.Next(1, 1000000);
            // Loop until a new id is created
            while (UserModelExists(id))
            {
                id = rnd.Next(1, 1000000);
            }
            return id;
        }
    }
}
