using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFrotasDLL.BLL.Libraries.Lang;
using Microsoft.AspNetCore.Mvc;
using UnipDLL.BLL;
using UnipDLL.BLL.Libraries.Constants;
using UnipDLL.BLL.Libraries.Filtro;
using UnipDLL.DAL.Repositories.Contracts;

namespace Unip.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class PeriodoController : Controller
    {

        private IPeriodoRepository _periodoRepository;

        public PeriodoController(IPeriodoRepository periodoRepository)
        {
            _periodoRepository=periodoRepository;
        }

        [HttpGet]
        public IActionResult Index(int? pagina, string pesquisa)
        {
            var cursos = _periodoRepository.ObterTodosPeriodos(pagina, pesquisa);

            return View(cursos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            // ViewBag.Categorias = _categoriaRepository.ObterTodasMarcas().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm]Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                _periodoRepository.Cadastrar(periodo);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            // ViewBag.Categorias = _marcaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();

        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var periodo = _periodoRepository.ObterPeriodo(id);
            //ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Where(a => a.Id != id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View(periodo);
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm]Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                _periodoRepository.Atualizar(periodo);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            //ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Where(a => a.Id != id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            _periodoRepository.Excluir(id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }
    }
}