using ControleFrotasDLL.BLL;
using UnipDLL.BLL.Libraries.Constants;
using UnipDLL.BLL.Libraries.Login;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using UnipDLL.DAL.Repositories.Contracts;
using UnipDLL.BLL;
using UnipDLL.DAL.Database.SQL;
using Microsoft.EntityFrameworkCore;

namespace UnipDLL.DAL.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
            private IConfiguration _conf;
            private UnipContext _banco;
          
            

            public ClienteRepository(UnipContext banco, IConfiguration configuration)
            {
                _banco = banco;
                _conf = configuration;
           
            }

            public void Atualizar(Cliente cliente)
            {
            Cliente banco = ObterCliente(cliente.Id);
            banco.Nascimento = cliente.Nascimento;
            banco.PeriodoId = cliente.PeriodoId;
            banco.CursoId = cliente.CursoId;
            banco.Email = cliente.Email;
            banco.Sexo = cliente.Sexo;
            banco.Situacao = SituacaoConstant.Ativo;
            banco.Telefone = cliente.Telefone;
            banco.Nome = cliente.Nome;

           
            _banco.SaveChanges();
            }

        public void Cadastrar(Cliente cliente)
            {
            cliente.Situacao = SituacaoConstant.Ativo;
                _banco.Add(cliente);
          
            _banco.SaveChanges();
        }

            public void Excluir(int Id)
            {
                Cliente cliente = ObterCliente(Id);
                _banco.Remove(cliente);
                _banco.SaveChanges();

            }

        public List<Cliente> ObterClientePorEmail(string email)
        {
            return _banco.Clientes.Where(a => a.Email == email).ToList();
        }

       

            public Cliente ObterCliente(int id)
            {
            return _banco.Clientes.Find(id);
        }

            public IEnumerable<Cliente> ObterTodosClientes()
            {
            return  _banco.Clientes.AsNoTracking().ToList();
            }

       

        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa)
            {
                int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
                int numeroPagina = pagina ?? 1;

                var bancoCliente = _banco.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoCliente = bancoCliente.AsNoTracking().Where(a => a.Nome.Contains(pesquisa.Trim()) || a.Email.Contains(pesquisa.Trim()));
            }

            return bancoCliente.AsNoTracking().ToPagedList<Cliente>(numeroPagina, RegistroPorPagina);
            }


        }
}
