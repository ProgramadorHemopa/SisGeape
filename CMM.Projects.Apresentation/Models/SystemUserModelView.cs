using System.Collections.Generic;

namespace CMM.Projects.Apresentation.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("systemuser")]
    public class SystemUserModelView : IValidatableObject
    {
        [Key]
        [Display(Name = "USUÁRIO")]
        public int SUSR_ID { get; set; }

        [Display(Name = "NOME")]
        [StringLength(300)]
        [Required(ErrorMessage = "Informe o Nome do Usuário", AllowEmptyStrings = false)]
        public string SUSR_NAME { get; set; }

        [Display(Name = "SENHA")]
        [MinLength(6, ErrorMessage = "Minímo de 6 caracteres")]
        [Required(ErrorMessage = "Informe a senha", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string SUSR_PASSWORD { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "CONFIRMAR SENHA")]
        [Compare("SUSR_PASSWORD", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        [Required(ErrorMessage = "Informe novamente a senha", AllowEmptyStrings = false)]
        public string SUSR_CONFIRM_PASSWORD { get; set; }

        [Display(Name = "PERFIL")]
        [Required(ErrorMessage = "Informe o Perfil do Usuário")]
        public Perfil? SUSR_PERFIL { get; set; }

        [Display(Name = "SISTEMA")]
        [Required(ErrorMessage = "Informe o SISTEMA do Usuário")]
        public Sistema? SIS_ID { get; set; }


        [Display(Name = "LOGIN")]
        [ScaffoldColumn(false)]
        public string SUSR_LOGIN { get; set; }

        [ScaffoldColumn(false)]
        public int SUSR_REGUSER { get; set; }





        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string[] lName;
            lName = SUSR_NAME.Split(' ');
            if (lName.Length <= 1)
            {
                yield return new ValidationResult("Insira o nome e sobrenome do usuário", new[] { "SUSR_NAME" });

            }


        }
    }
    public enum Sistema
    {
        [Display(Name = "Geral")]
        Geral = 99,
        //[Display(Name = "SisGeape")]
        //SisGeape = 2,
        //[Display(Name = "SASS")]
        //SASS = 3,
        [Display(Name = "SisGeape")]
        SisGeape = 1,
        [Display(Name = "SASS")]
        SASS = 2

    }


    public enum Perfil
    {
        [Display(Name = "Administrador")]
        Admin = 1,
        [Display(Name = "Operador")]
        Operador = 2,
        [Display(Name = "GEAPE")]
        GEAPE = 3,
        [Display(Name = "Colaborador")]
        Colaborador = 4,

    }
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

    }


    public class SystemUserEditModelView
    {
        [Key]
        [Display(Name = "USUÁRIO")]
        public int SUSR_ID { get; set; }

        [Display(Name = " NOVA SENHA")]
        [MinLength(6, ErrorMessage = "Minímo de 6 caracteres")]
        [Required(ErrorMessage = "Informe a NOVA SENHA", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string SUSR_PASSWORD { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "CONFIRMAR SENHA")]
        [Compare("SUSR_PASSWORD", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        [Required(ErrorMessage = "Informe novamente a NOVA SENHA", AllowEmptyStrings = false)]
        public string SUSR_CONFIRM_PASSWORD { get; set; }

        [ScaffoldColumn(false)]
        public int SUSR_REGUSER { get; set; }
    }

    public class SystemRoleModelView
    {
        [Key]
        public int SUROL_ID { get; set; }

        [Display(Name = "USUÁRIO")]
        [Required(ErrorMessage = "Informe o Nome do Usuário", AllowEmptyStrings = false)]
        public int SUSR_ID { get; set; }


        [Display(Name = "PERFIL")]
        [Required(ErrorMessage = "Informe o Perfil do Usuário")]
        public Perfil? SUSR_PERFIL { get; set; }

        [Display(Name = "SISTEMA")]
        [Required(ErrorMessage = "Informe o SISTEMA do Usuário")]
        public Sistema? SIS_ID { get; set; }

        [ScaffoldColumn(false)]
        public int SUROL_REGUSER { get; set; }

    }
}