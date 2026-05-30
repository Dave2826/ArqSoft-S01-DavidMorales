using Catalogo.Application.Services;
using Catalogo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Controllers
{
    public class AuthController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public AuthController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // =====================
        // REGISTRO GET
        // =====================

        public IActionResult Registro()
        {
            return View();
        }

        // =====================
        // REGISTRO POST
        // =====================

        [HttpPost]
        public IActionResult Registro(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            var registrado =
                _usuarioService.RegistrarUsuario(usuario);

            if (!registrado)
            {
                ViewBag.Error =
                    "Ya existe una cuenta con ese correo.";

                return View(usuario);
            }

            return RedirectToAction("Login");
        }

        // =====================
        // LOGIN GET
        // =====================

        public IActionResult Login()
        {
            return View();
        }
    }
}