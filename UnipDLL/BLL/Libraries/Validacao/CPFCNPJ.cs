using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ControleFrotasDLL.BLL;
using UnipDLL.BLL.Libraries.Constants;
using ControleFrotasDLL.BLL.Libraries.Lang;
using UnipDLL.BLL.Libraries.Validacao.CalculoCPFCNPJ;

namespace UnipDLL.BLL.Libraries.Validacao
{
    /*
    * Classe que valida o formato do campo CPF/CNPJ digitado pelo usuário
    */
    public class CPFCNPJ:ValidationAttribute
    {
        //Variáveis utilizadas para fazer o cálculo de CPF/CNPJ
        private static readonly int[] multiplosCPF = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] multiplosCNPJ = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        private Cliente _cliente;

        protected override ValidationResult IsValid(object CPFCNPJ, ValidationContext validationContext)
        {
            _cliente = (Cliente)validationContext.ObjectInstance;

            if((_cliente.CPF == null || _cliente.Tipo==null))
            {
                return new ValidationResult(" ");
            }
            var apenasDigitos = new Regex(@"[^\d]");
            string cpfcnpj = apenasDigitos.Replace((string)_cliente.CPF, "");

            //Verificação para determinar se o CPF está atribuido a um Cliente Fisico e um CNPJ a um cliente Jurídico 
            if ((cpfcnpj.Length == 11) && _cliente.Tipo.Equals(ClienteTipoConstant.Fisico))
            {

            int digito1 = CalculoDigitos.CalcularDigitos(cpfcnpj.Substring(0, 9), multiplosCPF);
            int digito2 = CalculoDigitos.CalcularDigitos(cpfcnpj.Substring(0, 9) + digito1, multiplosCPF);
            if (!cpfcnpj.Equals(cpfcnpj.Substring(0, 9) + digito1.ToString() + digito2.ToString()))
            {
                return new ValidationResult(Mensagem.MSG_E006);
            }
            }
            else if((cpfcnpj.Length==14 && _cliente.Tipo.Equals(ClienteTipoConstant.Juridico)))
            {
                int digito1 = CalculoDigitos.CalcularDigitos(cpfcnpj.Substring(0, 12), multiplosCNPJ);
                int digito2 = CalculoDigitos.CalcularDigitos(cpfcnpj.Substring(0, 12) + digito1, multiplosCNPJ);
                if (!cpfcnpj.Equals(cpfcnpj.Substring(0, 12) + digito1.ToString() + digito2.ToString()))
                {
                    return new ValidationResult(Mensagem.MSG_E012);
                }
            }
            else
            {
                return new ValidationResult(Mensagem.MSG_E013);
            }
            return ValidationResult.Success;
        }
    }
}