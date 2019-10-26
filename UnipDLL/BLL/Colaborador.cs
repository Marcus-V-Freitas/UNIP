using ControleFrotasDLL.BLL.Libraries.Lang;
using UnipDLL.BLL.Libraries.Validacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnipDLL.BLL
{
    public class Colaborador
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

       
        [Required(ErrorMessageResourceType =typeof(Mensagem),ErrorMessageResourceName ="MSG_E001")]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType =typeof(Mensagem),ErrorMessageResourceName ="MSG_E002")]
        [EmailUnicoColaborador]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Senha { get; set; }

        /*Tipo
         * C- Comum
         * G- Gerente
         */

        public string Tipo { get; set; }
    }
}
