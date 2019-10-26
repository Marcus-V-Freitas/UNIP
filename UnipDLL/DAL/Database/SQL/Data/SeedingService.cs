using ControleFrotasDLL.BLL;
using UnipDLL.DAL.Database.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //if (_banco.Veiculos.Any() ||
            //    _banco.Marcas.Any() ||
            //    _banco.Modelos.Any() || 
            //    _banco.VeiculosEmpresa.Any())
            //{
                return;
            }

          
        }
    }

