using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnipDLL.BLL;
using X.PagedList;

namespace UnipDLL.DAL.Repositories.Contracts
{
    public interface IClienteRepository
    {
        Cliente Login(string email, string senha);

        //CRUD
        void Cadastrar(Cliente cliente);
        void AtualizarSenha(Cliente cliente);
        void Atualizar(Cliente colaborador);
        void Excluir(int Id);
        Cliente ObterCliente(int Id);
        IEnumerable<Cliente> ObterTodosClientes();
        IEnumerable<Cliente> ObterTodosClientesJuridicos();
        List<Cliente> ObterClientePorEmail(string email);
        IPagedList<Cliente> ObterTodosClientes(int? pagina,string pesquisa);
    }
}
