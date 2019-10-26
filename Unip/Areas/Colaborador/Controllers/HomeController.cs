using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnipDLL.BLL.Libraries.Filtro;
using UnipDLL.BLL.Libraries.Login;
using UnipDLL.DAL.Repositories.Contracts;

namespace Unip.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]  //Especificar que o controller direciona 
    public class HomeController : Controller
    {

        private LoginColaborador _loginColaborador;
        private IColaboradorRepository _repositoryColaborador;

        public HomeController(IColaboradorRepository repositoryColaborador, LoginColaborador loginColaborador)
        {
            _repositoryColaborador = repositoryColaborador;
            _loginColaborador = loginColaborador;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]UnipDLL.BLL.Colaborador colaborador) //Evitar Confusões com o nome da area
        {
            UnipDLL.BLL.Colaborador colaboradorDB = _repositoryColaborador.Login(colaborador.Email, colaborador.Senha);


            if (colaboradorDB != null)
            {

                _loginColaborador.Login(colaboradorDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitado!";

                return View();
            }
        }

        [ColaboradorAutorizacao]
        public IActionResult Painel()
        {
            return View();
        }

        [ColaboradorAutorizacao]
        [ValidateHttpReferer]
        public IActionResult Logout()
        {
            _loginColaborador.Logout();
            return RedirectToAction("Login", "Home", "");
        }
    }
}