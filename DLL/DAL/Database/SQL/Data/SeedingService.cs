using ControleFrotasDLL.BLL;
using UnipDLL.DAL.Database.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnipDLL.BLL;

namespace UnipDLL.DAL.Database.SQL.Data
{
    public class SeedingService
    {
        /*
         * Classe responsável por popular as tabelas básicas que deverão sempre existir no sistema,
         * ou seja tudo relacionado aos veículos que a empresa já tem (Marcas, Modelos, Véiculos e Veículos da Empresa)
         */

        private UnipContext _banco;

        public SeedingService(UnipContext banco)
        {
            _banco = banco;
        }

        public void seed()
        {
            //Verifica se os campos estão vazios dentro da base de dados
            if (_banco.Cursos.Any() ||
                _banco.Periodos.Any() ||
                _banco.Colaboradores.Any())
            {
                return;
            }

            Curso c1 = new Curso("Análise e Desenvolvimento de Sistema", "https://www.unip.br/presencial/ensino/graduacao/tecnologicos/analise_desenv_sistemas.aspx");
            Curso c2 = new Curso("Gestão T.I.", "https://www.unip.br/presencial/ensino/graduacao/tecnologicos/gestao_tecnologia_info.aspx");
            Curso c3 = new Curso("Jogos Digitais", "https://www.unip.br/presencial/ensino/graduacao/tecnologicos/jogos_digitais.aspx");
            Curso c4 = new Curso("Redes Computadores", "https://www.unip.br/presencial/ensino/graduacao/tecnologicos/redes_computadores.aspx");

            Periodo p1 = new Periodo("Manhã");
            Periodo p2 = new Periodo("Tarde");
            Periodo p3 = new Periodo("Noite");

            Colaborador cl1 = new Colaborador("Admin","admim@admin.com","223344","G");

            _banco.Cursos.AddRange(c1, c2, c3, c4);
            _banco.Periodos.AddRange(p1, p2, p3);
            _banco.Colaboradores.AddRange(cl1);
            _banco.SaveChanges();
        }
    }
}

