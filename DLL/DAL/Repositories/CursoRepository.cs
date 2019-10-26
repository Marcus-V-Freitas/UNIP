using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnipDLL.BLL;
using UnipDLL.BLL.Libraries.Login;
using UnipDLL.DAL.Database.SQL;
using UnipDLL.DAL.Repositories.Contracts;
using X.PagedList;

namespace UnipDLL.DAL.Repositories
{
    public class CursoRepository:ICursoRepository
    {
        private IConfiguration _conf;

        private UnipContext _banco;

        private LoginCliente _login;

        public CursoRepository(UnipContext banco, IConfiguration configuration, LoginCliente loginCliente)
        {
            _banco = banco;
            _conf = configuration;
            _login = loginCliente;
        }


        public void Atualizar(Curso curso)
        {
            _banco.Update(curso);
            _banco.SaveChanges();
        }

        public void Cadastrar(Curso curso)
        {

            _banco.Add(curso);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Curso curso = ObterCurso(Id);
            _banco.Remove(curso);
            _banco.SaveChanges();
        }

        public Curso ObterCurso(int Id)
        {
            return _banco.Cursos.Find(Id); //Include(a => a.Cliente).Include(a => a.Veiculo).Include(a => a.Veiculo.Modelo).FirstOrDefault(x => x.VeiculoClienteId.Equals(Id));
        }

        public IPagedList<Curso> ObterTodosCursos(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoCurso = _banco.Cursos.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoCurso = bancoCurso.Where(a => a.Nome.Contains(pesquisa.Trim()));
            }

            return bancoCurso.ToPagedList<Curso>(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<Curso> ObterTodosCursos()
        {
            return _banco.Cursos.AsNoTracking().ToList();
        }
    }
}
