using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessPartner.Context;
using BusinessPartner.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPartner.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientContext _context;
        
        public ClientController( ClientContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var clients = _context.Clients.ToList();
            return View(clients);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Client client)
        {
            if(ModelState.IsValid)
            {
                _context.Clients.Add(client);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        public IActionResult Editar(int id)
        {
            var client = _context.Clients.Find(id);

            if( client == null)
            return NotFound();

            return View();
        }
        [HttpPost]
        public IActionResult Editar (Client client)
        {
            var clientBanco = _context.Clients.Find(client.Id);

            clientBanco.CardName = client.CardName;
            clientBanco.Avatar = client.Avatar;
            clientBanco.Address = client.Address;
            clientBanco.ZipCode = client.ZipCode;
            clientBanco.CardCode = client.CardCode;

            _context.Clients.Update(clientBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes( int id)
        {
            var client = _context.Clients.Find(id);
            if (client ==null)
             return RedirectToAction(nameof(Index));

            return View(client);
        }

        public IActionResult Deletar (int id)
        {
            var client = _context.Clients.Find(id);
            if (client ==null)
             return RedirectToAction(nameof(Index));

            return View(client);
        }

        [HttpPost]
        public IActionResult Deletar(Client client)
        {
            var clientBanco = _context.Clients.Find(client.Id);
            _context.Clients.Remove(clientBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}