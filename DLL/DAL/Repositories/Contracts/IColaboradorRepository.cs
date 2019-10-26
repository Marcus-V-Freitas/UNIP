using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnipDLL.BLL;
using X.PagedList;

namespace UnipDLL.DAL.Repositories.Contracts
{
   public interface IColaboradorRepository
    {
        Colaborador Login(string email, string senha);

        //CRUD
        void Cadastrar(Colaborador colaborador);
        void AtualizarSenha(Colaborador colaborador);
        void Atualizar(Colaborador colaborador);
        void Excluir(int Id);
        Colaborador ObterColaborador(int Id);
        List<Colaborador> ObterColaboradorPorEmail(string email);
        IEnumerable<Colaborador> ObterTodosColaboradores();
        IPagedList<Colaborador> ObterTodosColaboradores(int? pagina);
    }
}
