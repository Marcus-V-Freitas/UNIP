using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ControleFrotasDLL.BLL;
using UnipDLL.DAL.Repositories.Contracts;

namespace UnipDLL.BLL.Libraries.Email
{
   public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _conf;
        private ICursoRepository _cursoRepository;

        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration, ICursoRepository cursoRepository)
        {
            _smtp = smtp;
            _conf = configuration;
            _cursoRepository = cursoRepository;
        }


        /*
         * Envio de mensagem referente ao suporte que o cliente solicita (View Contato)
         */

        public void EnviarContatoEmail(Contato contato)
        {
           
            string corpoMsg = string.Format("<h2>Contato - CadastroUnip</h2>" +
                "<b>Nome: </b> {0} <br />" +
                "<b>Email: </b> {1} <br />" +
                "<b>Assunto: </b> {2} <br />"+
                "<b>Texto: </b> {3} <br />" +
                "<br /> E-mail enviado automaticamente do site LojaVirtual", contato.Nome, contato.Email, contato.Assunto, contato.Texto);

            /*
             * MailMessage -> Construir a mensagem 
             */

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_conf.GetValue<string>("Email:Username"));
            mensagem.To.Add(contato.Email);
            mensagem.Subject = "Contato - LojaVirtual - E-mail" + contato.Email;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar a mensagem via SMTP
            _smtp.Send(mensagem);
        }

        /*
        * Envio de mensagem referente a alteração de senha do Colaborador
        */

        public void EnviarSenhaParaColaboradorPorEmail(Colaborador colaborador)
        {
            string corpoMsg = string.Format("<h2>Colaborador - LojaVirtual</h2>" +
                "Sua senha é:" +
                "<h3> {0} </h3>", colaborador.Senha);

            /*
             * MailMessage -> Construir a mensagem 
             */

            MailMessage mensagem = new MailMessage
            {
                From = new MailAddress(_conf.GetValue<string>("Email:Username"))
            };
            mensagem.To.Add(colaborador.Email);
            mensagem.Subject = "Colaborador - LojaVirtual - Senha do colaborador - " + colaborador.Nome;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar a mensagem via SMTP
            _smtp.Send(mensagem);
        }

        public void EnviarDadosCadastro(Cliente cliente)
        {
            var curso = _cursoRepository.ObterCurso(Convert.ToInt32(cliente.CursoId));

            string corpoMsg = string.Format("<h2>Dados do candidato e Curso Escolhido:</h2>" +
                "<b> Nome: </b> {0} <br />" +
                "<b> Email: </b> {1} <br />" +
                "<b> Curso Escolhido: </b> {2} <br />" +
                "<b> Para mais informações do curso escolhido veja abaixo: </b> <a href=\"" +curso.Endereco +"\">  <br />" +
                "<br /> Clique Aqui <br /> " +
                "<br /> <b> E-mail enviado automaticamente do site DiaCampus </b>", cliente.Nome, cliente.Email, curso.Nome);

            /*
             * MailMessage -> Construir a mensagem 
             */

            MailMessage mensagem = new MailMessage
            {
                From = new MailAddress("atendimentoonline@unip.br") //new MailAddress(_conf.GetValue<string>("Email:Username"))
            };
            mensagem.To.Add(cliente.Email);
            mensagem.Subject = " Bem Vindo Futuro Aluno Unip, " + cliente.Nome;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar a mensagem via SMTP
            _smtp.Send(mensagem);
        }




    }
}
