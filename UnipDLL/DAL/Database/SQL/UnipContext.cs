using ControleFrotasDLL.BLL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using UnipDLL.BLL;

namespace UnipDLL.DAL.Database.SQL
{
    public class UnipContext:DbContext
    {
        /*
         * EF Core e ORM
         * ORM -> Biblioteca para mapear Objetos para banco de dados relacional
         * Unit Of Work -> Design Pattern Implementado pelo próprio Entity Framework, onde este realiza várias operações
         * sem precisar acionar a base de dados (Ex: Gerenciar várias operações (Transações) num banco de dados e só conclui se 
         * todas derem certo na mesma sessão com o comando ".SaveChanges()"
         */

        public UnipContext(DbContextOptions<UnipContext> options) : base(options)
        {

        }
        //Representação das tabelas criadas/existentes com as quais o EF Core irá trabalhar
       
        public DbSet<Colaborador>Colaboradores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
