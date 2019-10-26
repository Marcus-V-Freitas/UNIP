using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnipDLL.BLL.Libraries.Validacao;

namespace UnipDLL.BLL
{
    public class Cliente
    {
        
        [Display(Name ="Código")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType =typeof(Mensagem), ErrorMessageResourceName ="MSG_E003")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E008")]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType =typeof(Mensagem), ErrorMessageResourceName ="MSG_E002")]
        [EmailUnicoCliente]
        public string Email { get; set; } 
 
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name ="Idade")]
        public string Nascimento { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Telefone { get; set; }


        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Sexo { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }

        [Required(ErrorMessageResourceType =typeof(Mensagem), ErrorMessageResourceName ="MSG_E001")]
        [Display(Name ="Ano Desejado")]
        public string AnoDesejadoCurso { get; set; }

        [Display(Name ="Curso")]
        public int? CursoId { get; set; }

        [ForeignKey(nameof(CursoId))]
        public virtual Curso Curso { get; set; }

        [Display(Name = "Período")]
        public int? PeriodoId { get; set; }

        [ForeignKey(nameof(PeriodoId))]
        public virtual Periodo Periodo { get; set; }

    }
}
