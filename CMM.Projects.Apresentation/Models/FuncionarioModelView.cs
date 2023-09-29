namespace CMM.Projects.Apresentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    using System.Web.Mvc;
    public class FuncionarioModelView : IValidatableObject
    {
        public FuncionarioModelView()
        {
            Vinculos = new HashSet<VinculoModelView>();
        }
        [Key]
        public int FUN_ID { get; set; }

        [Display(Name = "MATRICULA")]
        [StringLength(15)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Somente números")]
        [Required]
        [Remote("ExistFuncionario", "Funcionario", ErrorMessage = "Já existe um Funcionário cadastrado com esta Matrícula.", AdditionalFields = "FUN_ID")]
        public string FUN_MATRICULA { get; set; }


        [Display(Name = "NOME")]
        [StringLength(300)]
        [Required(ErrorMessage = "Informe o NOME", AllowEmptyStrings = false)]
        public string FUN_NOME { get; set; }

        [Display(Name = "NOME CRACHÁ")]
        [StringLength(300)]
        public string FUN_NOMECRACHA { get; set; }

        [Display(Name = "SEXO")]
        [Required(ErrorMessage = "Informe o SEXO")]
        public string FUN_SEXO { get; set; }

        [Display(Name = "BANCO")]
        public int? BNC_ID { get; set; }

        [Display(Name = "AGÊNCIA")]
        [StringLength(300)]
        public string FUN_AGENCIA { get; set; }


        [Display(Name = "CONTA CORRENTE")]
        [StringLength(100)]
        public string FUN_CONTACORRENTE { get; set; }


        [Display(Name = "ESTADO CIVIL")]
        [Required(ErrorMessage = "Informe o ESTADO CIVIL")]
        public int? FUN_ESTADOCIVIL { get; set; }


        [Display(Name = "CÔNJUGE")]
        [StringLength(300)]
        public string FUN_CONJUGE { get; set; }


        [Display(Name = "PAI")]
        [StringLength(300)]
        public string FUN_PAI { get; set; }

        [Display(Name = "MÃE")]
        [StringLength(300)]
        [Required(ErrorMessage = "Informe MÃE")]
        public string FUN_MAE { get; set; }

        [Display(Name = "TIPAGEM")]
        [Required(ErrorMessage = "Informe a TIPAGEM")]
        public int? FUN_TIPOSANGUINEO { get; set; }

        [Display(Name = "LOGRADOURO")]
        [StringLength(300)]
        //[Required(ErrorMessage = "Informe LOGRADOURO")]
        public string FUN_LOGRADOURO { get; set; }


        [Display(Name = "ENDEREÇO")]
        [StringLength(300)]
        //[Required(ErrorMessage = "Informe ENDEREÇO")]
        public string FUN_ENDERECO { get; set; }

        [Display(Name = "NUMERO")]
        [StringLength(300)]
        public string FUN_NUMEROEND { get; set; }


        [Display(Name = "BAIRRO")]
        [StringLength(300)]
        //[Required(ErrorMessage = "Informe BAIRRO")]
        public string FUN_BAIRRO { get; set; }

        [Display(Name = "COMPLEMENTO")]
        [StringLength(300)]
        public string FUN_COMPLEMENTO { get; set; }


        [Display(Name = "CEP")]
        [StringLength(15)]
        [RegularExpression(@"^\d{8}$|^\d{5}-\d{3}$", ErrorMessage = "O CEP deverá estar no formato 00000000 ou 00000-000")]
        [Required(ErrorMessage = " Informe a CEP")]
        public string FUN_CEP { get; set; }

        [Display(Name = "CIDADE")]
        [StringLength(100)]
        // [Required(ErrorMessage = "Informe CIDADE")]
        public string FUN_CIDADE { get; set; }

        [Display(Name = "CIDADE NATAL")]
        [StringLength(100)]
        // [Required(ErrorMessage = "Informe CIDADE NATAL")]
        public string FUN_CIDADENATAL { get; set; }


        [Display(Name = "DATA NASCIMENTO")]
        [Required(ErrorMessage = "Informe a DATA NASCIMENTO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FUN_DATANASCIMENTO { get; set; }

        [Display(Name = "NACIONALIDADE")]
        [StringLength(100)]
        // [Required(ErrorMessage = "Informe NACIONALIDADE")]
        public string FUN_NACIONALIDADE { get; set; }


        [Display(Name = "RG")]
        [StringLength(45)]
        // [Required(ErrorMessage = "Informe RG", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Digite somente numeros")]
        public string FUN_RG { get; set; }


        [Display(Name = "EMISSÃO RG")]
        [StringLength(45)]
        public string FUN_RGEMISSOR { get; set; }


        [Display(Name = "ESTADO RG")]
        [StringLength(45)]
        public string FUN_RGESTADO { get; set; }

        [Display(Name = "RG DATA EMISSÃO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FUN_RGDATAEMISSAO { get; set; }


        [Display(Name = "CPF")]
        [StringLength(45)]
        [Required(ErrorMessage = " Informe o CPF", AllowEmptyStrings = false)]
        [CustomValidation.ValidaCPF(ErrorMessage = "CPF inválido")]
        public string FUN_CPF { get; set; }

        [Display(Name = "PIS/PASEP")]
        [StringLength(45)]
        [Required(ErrorMessage = " Informe o PIS")]
        [CustomValidation.ValidaPIS(ErrorMessage = "PIS inválido")]
        public string FUN_PIS { get; set; }

        [Display(Name = "CNS")]
        [StringLength(45)]
        public string FUN_CNS { get; set; }


        [Display(Name = "TITULO")]
        [StringLength(45)]
        public string FUN_TITULOELEITOR { get; set; }

        [Display(Name = "ZONA")]
        [StringLength(45)]
        public string FUN_TITULOZONA { get; set; }

        [Display(Name = "SESSÃO")]
        [StringLength(45)]
        public string FUN_TITULOSESSAO { get; set; }

        [Display(Name = "TITULO DATA EMISSÃO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FUN_TITULODATAEMISSAO { get; set; }


        [Display(Name = "CERTIFICADO MILITAR")]
        [StringLength(45)]
        public string FUN_CERTIFICADOMILITAR { get; set; }

        [Display(Name = "SERIE")]
        [StringLength(45)]
        public string FUN_CMSERIE { get; set; }

        [Display(Name = "CORPORAÇÃO")]
        [StringLength(45)]
        public string FUN_CMCORPORACAO { get; set; }

        [Display(Name = "CTPS")]
        [StringLength(45)]
        public string FUN_CTPS { get; set; }

        [Display(Name = "CTPS EMISSÃO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FUN_CTPSDATAEMISSAO { get; set; }

        [Display(Name = "CNH DATA VALIDADE")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FUN_CNHVALIDADE { get; set; }


        [Display(Name = "CNH")]
        [StringLength(45)]
        public string FUN_CNH { get; set; }

        [Display(Name = "CATEGORIA")]
        [StringLength(4)]
        public string FUN_CNHCATEGORIA { get; set; }

        [Display(Name = "EMAIL")]
        [StringLength(200)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string FUN_EMAIL { get; set; }


        [Display(Name = "TELEFONE")]
        [StringLength(100)]
        public string FUN_TELEFONE { get; set; }

        [Display(Name = "CELULAR")]
        [StringLength(100)]
        [DataType(DataType.PhoneNumber)]
        public string FUN_CELULAR { get; set; }

        [Display(Name = "ESCOLARIDADE")]
        public int? FUN_ESCOLARIDADE { get; set; }

        [Display(Name = "IDIOMAS")]
        [StringLength(200)]
        public string FUN_IDIOMA { get; set; }

        [Display(Name = "NECESSIDADES ESPECIAIS")]
        public int? FUN_NECESSIDADE_ESPECIAL { get; set; }

        [Display(Name = "ASSISTÊNCIA MÉDICA")]
        [StringLength(200)]
        public string FUN_ASSISTENCIAMEDICA { get; set; }

        [Display(Name = "POSSUI IMÓVEL PRÓPRIO OU FINANCIADO")]
        public int? FUN_IMOVEL { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        [DataType(DataType.MultilineText)]
        public string FUN_OBSERVACAO { get; set; }

        [ScaffoldColumn(false)]
        public int? FUN_REGUSER { get; set; }

        public byte[] FUN_FOTO { get; set; }

        [NotMapped]
        [Display(Name = "Imagem")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase FOTO { get; set; }

        public virtual ICollection<VinculoModelView> Vinculos { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FUN_DATANASCIMENTO >= DateTime.Today)
            {
                yield return new ValidationResult("Data de Nascimento não pode ser maior que a Data atual", new[] { "FUN_DATANASCIMENTO" });
            }
            if (FOTO == null && FUN_ID == 0)
            {
                yield return new ValidationResult("Insira a foto do servidor", new[] { "FOTO" });
            }
        }

    }


    public class FuncionarioNomeCrachaModelView
    {
        [Key]
        public int FUN_ID { get; set; }

        [Display(Name = "NOME CRACHÁ")]
        [StringLength(300)]
        [Required(ErrorMessage = "Informe o NOME DO CRACHÁ", AllowEmptyStrings = false)]
        public string FUN_NOMECRACHA { get; set; }

    }


    //Modificado por Angelo Matos em 20012020
    public class FuncionarioPesquisaModelView
    {
        [Key]
        public int FUN_ID { get; set; }

        [Display(Name = "MATRICULA")]
        [StringLength(15)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Somente números")]
        public string MATRIUCLA { get; set; }


        [Display(Name = "NOME")]
        [StringLength(300)]
        public string NOME { get; set; }

        public int? CARGO { get; set; }

        public int? UNIDADE { get; set; }

        public int? TIPO { get; set; }
        public int? SITUACAO { get; set; }

        [Display(Name = "DATA DE NASCIMENTO")]
        public DateTime? DATANASCIMENTO { get; set; }

        [Display(Name = "SEXO")]
        public string SEXO { get; set; }

        [Display(Name = "INICIO PERIODO ADMISSÃO")]
        public DateTime? INICIO_PERIODO_ADMISSAO { get; set; }

        [Display(Name = "FIM PERIODO ADMISSÃO")]
        public DateTime? FIM_PERIODO_ADMISSAO { get; set; }

        [Display(Name = "INICIO PERIODO DEMISSÃO")]
        public DateTime? INICIO_PERIODO_DEMISSAO { get; set; }

        [Display(Name = "FIM PERIODO DEMISSÃO")]
        public DateTime? FIM_PERIODO_DEMISSAO { get; set; }

        [Display(Name = "FUNÇÃO")]
        public virtual ICollection<FuncaoModelView> FUNCAO { get; set; }
        public int Idade()
        {
            if (DATANASCIMENTO.HasValue)
            {
                return (DateTime.Now.Year - DATANASCIMENTO.Value.Year);
            }
            else
            {
                return 0;
            }
        }

        public DateTime? INICIO { get; set; }
        public DateTime? FIM { get; set; }

    }

    public class FuncionarioSolicitarCrachaModelView : IValidatableObject
    {


        [Key]
        public int FUNCRC_ID { get; set; }

        public int VIA { get; set; }
        [Display(Name = "VÍNCULO")]
        [Required(ErrorMessage = "Informe a VÍNCULO")]
        public Nullable<int> VNC_ID { get; set; }


        [ScaffoldColumn(false)]
        public byte[] FUN_FOTO { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<int> FUN_ID { get; set; }

        [Display(Name = "SERVIDOR")]
        [ScaffoldColumn(false)]
        public string FUN_NOME { get; set; }
        [Display(Name = "SERVIDOR")]
        public string MATRICULA { get; set; }

        [ScaffoldColumn(false)]
        public string VNC_NOME { get; set; }


        [Display(Name = "Nº CARTÃO")]
        [StringLength(45)]
        public string FUNCRC_NUMERO { get; set; }

        [Display(Name = "DATA EMISSÃO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public Nullable<System.DateTime> FUNCRC_DATAEMISSAO { get; set; }


        [Display(Name = "TIPO")]
        [Required(ErrorMessage = "Informe a TIPO")]
        public Nullable<int> FUNCRC_TIPO { get; set; }

        [Display(Name = "DATA SOLICITAÇÃO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public Nullable<System.DateTime> FUNCRC_DATASOLICITACAO { get; set; }

        [Display(Name = "SITUAÇÃO")]
        [ScaffoldColumn(false)]
        public Nullable<int> FUNCRC_SITUACAO { get; set; }


        [Display(Name = "OBSERVAÇÃO")]
        [StringLength(600)]
        [DataType(DataType.MultilineText)]
        public string FUNCRC_OBSERVACAO { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<int> FUNCRC_REGUSER { get; set; }


        [Display(Name = "TIPO DE CRACHÁ")]
        public string TIPOCRACHA
        {
            get
            {
                switch (FUNCRC_TIPO)
                {
                    case 1:
                        return "IDENTIFICAÇÃO";
                    case 2:
                        return "PROXIMIDADE";
                    default:
                        return "";
                }

            }
        }

        [Display(Name = "SITUAÇÃO")]
        public string SITUACAO
        {
            get
            {
                switch (FUNCRC_SITUACAO)
                {
                    case 1:
                        return "PENDENTE";
                    case 2:
                        return "IMPRESSO";
                    case 3:
                        return "PERDA";
                    case 4:
                        return "DANIFICADO";
                    default:
                        return "";
                }

            }
        }


        public enum Tipo
        {
            IDENTIFICAÇÃO = 1,
            PROXIMIDADE = 2,
        }


        public enum Situacao
        {
            Pendente = 1,
            Impresso = 2,
            Perda = 3,
            Danificado = 4
        }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FUNCRC_TIPO == (int)Tipo.PROXIMIDADE && FUNCRC_NUMERO == null && FUNCRC_ID != 0)
            {
                yield return new ValidationResult("Insira a numeração do verso do crachá", new[] { "FUNCRC_NUMERO" });
            }

        }
    }

    public class FuncionarioRelatorioModelView
    {
        public int FUN_ID { get; set; }

        [Display(Name = "MATRICULA")]
        public string FUN_MATRICULA { get; set; }

        [Display(Name = "NOME")]
        public string FUN_NOME { get; set; }

        [Display(Name = "CARGO")]
        public string CARGO { get; set; }

        [Display(Name = "SITUAÇÃO")]
        public int SITUACAO { get; set; }

        public bool Ativo { get; set; }

        [Display(Name = "UNIDADE")]
        public string FUN_UNIDADE { get; set; }

        [Display(Name = "DATA ADMISSÃO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? FUN_DATA_ADMISSAO { get; set; }

        [Display(Name = "DATA DEMISSÃO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? FUN_DATA_DEMISSAO { get; set; }
    }

    //Adicionado por Angelo Matos em 26092019
    public class FuncionarioRelatorioFuncaoModelView : FuncionarioRelatorioModelView
    {
        [Display(Name = "FUNÇÃO")]
        public string FUNCAO { get; set; }
        [Display(Name = "INÍCIO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? FUNCAO_DATA_INICIO { get; set; }
        [Display(Name = "FIM")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? FUNCAO_DATA_FIM { get; set; }
    }

    //Adicionado por Angelo Matos em 26092019
    public class FuncionarioRelatorioAposentadoriaModelView : FuncionarioRelatorioModelView
    {
        [Display(Name = "SITUAÇÃO")]
        public string SITUACAO_NOME { get; set; }
        [Display(Name = "TIPO APOSENTADORIA")]
        public string TIPO_APOSENTADORIA { get; set; }
        [Display(Name = "ENTRADA APOSENTADORIA")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? ENTRADA_APOSENTADORIA { get; set; }
    }

    //Adicionado por Angelo Matos em 26092019
    public class FuncionarioRelatorioBeneficioModelView : FuncionarioRelatorioModelView
    {
        [Display(Name = "BENEFÍCIO")]
        public string BENEFICIO_NOME { get; set; }
        [Display(Name = "INÍCIO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? BENEFICIO_DATA_INICIO { get; set; }
        [Display(Name = "FIM")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? BENEFICIO_DATA_FIM { get; set; }
    }

    //Adicionado por Angelo Matos em 26092019
    public class FuncionarioRelatorioCessaoModelView : FuncionarioRelatorioModelView
    {
        [Display(Name = "ORGÃO DESTINO")]
        public string ORGAO_DESTINO { get; set; }
        [Display(Name = "ÔNUS")]
        public string ONUS { get; set; }
        [Display(Name = "DATA CESSÃO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? DATA_CESSAO { get; set; }
    }

    //Adicionado por Angelo Matos em 16012020
    public class CamposFuncionarioBuscaRelatorioModelView
    {
        public string MATRICULA { get; set; }
        public string NOME { get; set; }
        public int?[] CARGO { get; set; }
        public string[] TEXTO_CARGO { get; set; }
        public int?[] UNIDADE { get; set; }
        public string[] TEXTO_UNIDADE { get; set; }
        public int?[] TIPO_VINCULO { get; set; }
        public string[] TEXTO_TIPO_VINCULO { get; set; }
        public int?[] SITUACAO_VINCULO { get; set; }
        public string[] TEXTO_SITUACAO_VINCULO { get; set; }
        public string SEXO { get; set; }
        public int? IDADE { get; set; }

        public int?[] BENEFICIO { get; set; }
        public string[] TEXTO_BENEFICIO { get; set; }

        public int?[] FUNCAO { get; set; }
        public string[] TEXTO_FUNCAO { get; set; }

        public DateTime? INICIO_PERIODO_ADMISSAO { get; set; }
        public DateTime? FIM_PERIODO_ADMISSAO { get; set; }
        public DateTime? INICIO_PERIODO_DEMISSAO { get; set; }
        public DateTime? FIM_PERIODO_DEMISSAO { get; set; }

        public DateTime? FUNCAO_DATA_INICIO { get; set; }
        public DateTime? FUNCAO_DATA_FIM { get; set; }
        public DateTime? BENEFICIO_DATA_INICIO { get; set; }
        public DateTime? BENEFICIO_DATA_FIM { get; set; }

        public int?[] TIPO_APOSENTADORIA { get; set; }
        public string[] TEXTO_TIPO_APOSENTADORIA { get; set; }
        public DateTime? ENTRADA_APOSENTADORIA_DATA_INICIO { get; set; }
        public DateTime? ENTRADA_APOSENTADORIA_DATA_FIM { get; set; }

    }

}