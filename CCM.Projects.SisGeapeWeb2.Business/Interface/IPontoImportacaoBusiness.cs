using CCM.Projects.SisGeape2.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IPontoImportacaoBusiness
    {

        bool AddImportacao(PontoImportacaoDomainModel domainModel);
        List<PontoImportacaoDomainModel> GetAll();
        bool Salvar();
        bool ImportarBatidasPorArquivoTxt(Stream arquivo, DateTime? inicio, DateTime? fim, string observacao);

    }
}
