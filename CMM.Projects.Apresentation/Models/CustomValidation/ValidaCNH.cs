using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Models.CustomValidation
{
    public class ValidaCNH : ValidationAttribute, IClientValidatable
    {
        public ValidaCNH()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            bool valido = VerficaCNH(value.ToString());
            return valido;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "validacnh"
            };
        }

        public static string RemoveNaoNumericos(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

        /// <summary>
        /// Valida se um cnh é válido
        /// </summary>
        /// <param name="cnh"></param>
        /// <returns></returns>
        public static bool VerficaCNH(string cnh)
        {
            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cnh = RemoveNaoNumericos(cnh);

            bool isValid = false;
            var firstChar = cnh[0];
            if (cnh.Length == 11 && cnh != new string('1', 11))
            {

                var dsc = 0;
                var v = 0;
                for (int i = 0, j = 9; i < 9; i++, j--)
                {
                    v += (Convert.ToInt32(cnh[i].ToString()) * j);
                }

                var vl1 = v % 11;
                if (vl1 >= 10)
                {
                    vl1 = 0;
                    dsc = 2;
                }

                v = 0;
                for (int i = 0, j = 1; i < 9; ++i, ++j)
                {
                    v += (Convert.ToInt32(cnh[i].ToString()) * j);
                }

                var x = v % 11;
                var vl2 = (x >= 10) ? 0 : x - dsc;

                isValid = vl1.ToString() + vl2.ToString() == cnh.Substring(cnh.Length - 2, 2);

            }
            return isValid;
        }
    }

    public class ValidaDataValidadeCNH : ValidationAttribute, IClientValidatable
    {
        public ValidaDataValidadeCNH()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }
            bool valido = VerificaDataValidade((DateTime)value);
            return valido;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "validadatavalidadecnh"
            };
        }

        /// <summary>
        /// Valida se um cnh é válido
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool VerificaDataValidade(DateTime data)
        {
            if (data > DateTime.Now)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class ValidaDataEmissaoCNH : ValidationAttribute, IClientValidatable
    {
        public ValidaDataEmissaoCNH()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }
            bool valido = VerificaDataEmissao((DateTime)value);
            return valido;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "validadedataemissaocnh"
            };
        }

        /// <summary>
        /// Valida se um cnh é válido
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool VerificaDataEmissao(DateTime data)
        {
            if (data < DateTime.Now)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}