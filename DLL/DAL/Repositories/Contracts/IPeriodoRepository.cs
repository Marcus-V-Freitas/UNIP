using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnipDLL.BLL;
using X.PagedList;

namespace UnipDLL.DAL.Repositories.Contracts
{
   public interface IPeriodoRepository
    {
        void Cadastrar(Periodo periodo);
        void Atualizar(Periodo periodo);
        void Excluir(int Id);
        Periodo ObterPeriodo(int Id);
        IEnumerable<Periodo> ObterTodosPeriodos();
        IPagedList<Periodo> ObterTodosPeriodos(int? pagina, string pesquisa);
    }
}
