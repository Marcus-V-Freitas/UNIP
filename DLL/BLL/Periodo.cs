using ControleFrotasDLL.BLL.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnipDLL.BLL
{
    public class Periodo
    {
        
        [Display(Name ="Código")]
        public int Id { get; set; }

        [Display(Name ="Periodo")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Horario { get; set; }

        public Periodo() { }

        public Periodo(string horario)
        {
            Horario = horario;
        }
    }
}
