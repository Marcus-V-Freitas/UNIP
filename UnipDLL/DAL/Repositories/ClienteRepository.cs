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

namespace UnipDLL.DAL.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
            private IConfiguration _conf;
            private UnipContext _banco;
            private LoginCliente _login;
            

            public ClienteRepository(UnipContext banco, IConfiguration configuration, LoginCliente loginCliente)
            {
                _banco = banco;
                _conf = configuration;
                 _login = loginCliente;
            }

            public void Atualizar(Cliente cliente)
            {
                _banco.Update(cliente);
                _banco.Entry(cliente).Property(a => a.Senha).IsModified = false;
                _banco.SaveChanges();
            }

            public void AtualizarSenha(Cliente cliente)
            {
                _banco.Update(cliente);
                _banco.Entry(cliente).Property(a => a.Nome).IsModified = false;
                _banco.Entry(cliente).Property(a => a.Email).IsModified = false;
                _banco.Entry(cliente).Property(a => a.CPF).IsModified = false;
            _banco.Entry(cliente).Property(a => a.Nascimento).IsModified = false;
            _banco.Entry(cliente).Property(a => a.Sexo).IsModified = false;
            _banco.Entry(cliente).Property(a => a.Telefone).IsModified = false;
            _banco.Entry(cliente).Property(a => a.Tipo).IsModified = false;
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

        public Cliente Login(string email, string senha)
            {
                return _banco.Clientes.Where(m => m.Email == email && m.Senha == senha && m.Situacao!=SituacaoConstant.Desativado).FirstOrDefault();
            }

            public Cliente ObterCliente(int Id)
            {
                return _banco.Clientes.Find(Id);
            }

            public IEnumerable<Cliente> ObterTodosClientes()
            {
            return (_login.Tipo() == null) ? _banco.Clientes.ToList() : _banco.Clientes.Where(a => a.Id == _login.Tipo()).ToList();
            }

        public IEnumerable<Cliente> ObterTodosClientesJuridicos()
        {
            return (_login.Tipo() == null) ? _banco.Clientes.Where(a=>a.Tipo=="J").ToList() : _banco.Clientes.Where(a => a.Id == _login.Tipo()).ToList();
        }

        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa)
            {
                int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
                int numeroPagina = pagina ?? 1;

                var bancoCliente = _banco.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoCliente = bancoCliente.Where(a => a.Nome.Contains(pesquisa.Trim()) || a.Email.Contains(pesquisa.Trim()));
            }

            return bancoCliente.ToPagedList<Cliente>(numeroPagina, RegistroPorPagina);
            }


        }
}
