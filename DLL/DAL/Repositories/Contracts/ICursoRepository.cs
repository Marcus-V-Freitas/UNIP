using UnipDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace UnipDLL.DAL.Repositories.Contracts
{
    public interface ICursoRepository
    {
        void Cadastrar(Curso curso);
        void Atualizar(Curso curso);
        void Excluir(int Id);
        Curso ObterCurso(int Id);
        IEnumerable<Curso> ObterTodosCursos();
        IPagedList<Curso> ObterTodosCursos(int? pagina, string pesquisa);
    }
}
