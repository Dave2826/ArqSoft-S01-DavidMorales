using MotoTrack.Application.Services;
using MotoTrack.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MotoTrack.Controllers
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
            return View(new LoginViewModel());
        }

        // =====================
        // LOGIN POST
        // =====================

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario =
                _usuarioService.ValidarLogin(
                    model.Correo,
                    model.Password);

            if (usuario == null)
            {
                ViewBag.Error =
                    "Correo o contraseña incorrectos.";

                return View(model);
            }

            HttpContext.Session.SetString(
                "UsuarioId",
                usuario.Id.ToString());

            HttpContext.Session.SetString(
                "Nombre",
                usuario.Nombre);

            HttpContext.Session.SetString(
                "Correo",
                usuario.Correo);

            return RedirectToAction(
                "Index",
                "Home");
        }

        // =====================
        // LOGOUT
        // =====================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Login");
        }
    }
}
