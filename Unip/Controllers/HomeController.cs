using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnipDLL.DAL.Repositories.Contracts;
using Unip.Models;
using UnipDLL.BLL;
using UnipDLL.BLL.Libraries.Email;
using UnipDLL.BLL.Libraries.Login;
using ControleFrotasDLL.BLL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Unip.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository _repositoryCliente;
        private LoginCliente _loginCliente;
        private GerenciarEmail _gerenciarEmail;
        private IPeriodoRepository _periodoRepository;
        private ICursoRepository _cursoRepository;

        public HomeController(IClienteRepository repositoryCliente, LoginCliente loginCliente, 
            GerenciarEmail gerenciarEmail, IPeriodoRepository periodoRepository, ICursoRepository cursoRepository)
        {
            _repositoryCliente = repositoryCliente;
            _loginCliente = loginCliente;
            _gerenciarEmail = gerenciarEmail;
            _periodoRepository = periodoRepository;
            _cursoRepository = cursoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Cursos()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Contato()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contato([FromForm]Contato contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _gerenciarEmail.EnviarContatoEmail(contato);

                    ViewData["MSG_S"] = "Mensagem de contato enviada com sucesso!";
                }
                else
                {

                }
            }
            catch (Exception )
            {
                ViewData["MSG_E"] = "Opps! Tivemos um erro, tente novamente mais tarde!";

               
            }

            return View("Contato");

        }

        [HttpGet]
        public IActionResult CadastroCliente()
        {
            ViewBag.Cursos = _cursoRepository.ObterTodosCursos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Periodos = _periodoRepository.ObterTodosPeriodos().Select(a => new SelectListItem(a.Horario, a.Id.ToString()));
            return View();
        }

        [HttpPost]
        public IActionResult CadastroCliente([FromForm]Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repositoryCliente.Cadastrar(cliente);
                _gerenciarEmail.EnviarDadosCadastro(cliente);

                TempData["MSG_S"] = "Cadastro realizado com sucesso!!!";

                //TODO - Implementar redirecionamentos diferentes

                return RedirectToAction(nameof(CadastroCliente));
            }
            ViewBag.Cursos = _cursoRepository.ObterTodosCursos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Periodos = _periodoRepository.ObterTodosPeriodos().Select(a => new SelectListItem(a.Horario, a.Id.ToString()));
            return View();
        }

    }
}
