using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnipDLL.BLL.Libraries.Constants;
using Microsoft.AspNetCore.Mvc;
using UnipDLL.BLL.Libraries.Filtro;
using UnipDLL.DAL.Repositories.Contracts;
using X.PagedList;
using ControleFrotasDLL.BLL.Libraries.Lang;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Unip.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;
        private IPeriodoRepository _periodoRepository;
        private ICursoRepository _cursoRepository;

        public ClienteController(IClienteRepository clienteRepository, ICursoRepository cursoRepository, IPeriodoRepository periodoRepository)
        {
            _clienteRepository = clienteRepository;
            _cursoRepository = cursoRepository;
            _periodoRepository = periodoRepository;
        }

        [HttpGet]
        public IActionResult Index(int? pagina, string pesquisa)
        {
            IPagedList<UnipDLL.BLL.Cliente> clientes = _clienteRepository.ObterTodosClientes(pagina, pesquisa);
            return View(clientes);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Cursos = _cursoRepository.ObterTodosCursos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Periodos = _periodoRepository.ObterTodosPeriodos().Select(a => new SelectListItem(a.Horario, a.Id.ToString()));
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm]UnipDLL.BLL.Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.Cadastrar(cliente);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));

            }

            return View();

        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            ViewBag.Cursos = _cursoRepository.ObterTodosCursos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Periodos = _periodoRepository.ObterTodosPeriodos().Select(a => new SelectListItem(a.Horario, a.Id.ToString()));
            var cliente = _clienteRepository.ObterCliente(id);
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm]UnipDLL.BLL.Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.Atualizar(cliente);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Cursos = _cursoRepository.ObterTodosCursos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Periodos = _periodoRepository.ObterTodosPeriodos().Select(a => new SelectListItem(a.Horario, a.Id.ToString()));
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult AtivarDesativar(int id)
        {
            UnipDLL.BLL.Cliente cliente = _clienteRepository.ObterCliente(id);

            cliente.Situacao = (cliente.Situacao == SituacaoConstant.Ativo) ? cliente.Situacao = SituacaoConstant.Desativado : cliente.Situacao = SituacaoConstant.Ativo;

            _clienteRepository.Atualizar(cliente);

            TempData["MSG_S"] = Mensagem.MSG_S001;
            return RedirectToAction(nameof(Index));

        }
    }
}