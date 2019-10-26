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
   public class PeriodoRepository:IPeriodoRepository
    {
        private IConfiguration _conf;

        private UnipContext _banco;

        private LoginCliente _login;

        public PeriodoRepository(UnipContext banco, IConfiguration configuration, LoginCliente loginCliente)
        {
            _banco = banco;
            _conf = configuration;
            _login = loginCliente;
        }


        public void Atualizar(Periodo periodo)
        {
            _banco.Update(periodo);
            _banco.SaveChanges();
        }

        public void Cadastrar(Periodo periodo)
        {

            _banco.Add(periodo);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Periodo periodo = ObterPeriodo(Id);
            _banco.Remove(periodo);
            _banco.SaveChanges();
        }

        public Periodo ObterPeriodo(int Id)
        {
            return _banco.Periodos.Find(Id); //Include(a => a.Cliente).Include(a => a.Veiculo).Include(a => a.Veiculo.Modelo).FirstOrDefault(x => x.VeiculoClienteId.Equals(Id));
        }

        public IPagedList<Periodo> ObterTodosPeriodos(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoPeriodo = _banco.Periodos.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoPeriodo = bancoPeriodo.Where(a => a.Horario.Contains(pesquisa.Trim()));
            }

            return bancoPeriodo.ToPagedList<Periodo>(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<Periodo> ObterTodosPeriodos()
        {
            return _banco.Periodos.AsNoTracking().ToList();
        }
    }
}
