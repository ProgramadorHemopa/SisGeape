using System.IO;

namespace CCM.Utils
{
    public static class SalvaArquivo
    {
        public static string SalvaArquivoDiretorio(string diretorio, byte[] arrayByte, string nomeArquivo, string extensao, bool sobreescrever = false)
        {
            int cont = 0;
            string caminhoSalvo = "";
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }
            if (!sobreescrever)
            {
                while (File.Exists(caminhoSalvo = string.Format(diretorio + nomeArquivo + (cont > 0 ? " (" + cont + ")" : "") + extensao)))
                { cont++; };
                var fileStream = new FileStream(caminhoSalvo, FileMode.Create, FileAccess.Write);
                fileStream.Write(arrayByte, 0, arrayByte.Length);
                fileStream.Close();
            }
            else
            {
                var fileStream = new FileStream(string.Format(diretorio + nomeArquivo + extensao), FileMode.Create, FileAccess.Write);
                caminhoSalvo = string.Format(diretorio + nomeArquivo + extensao);
                fileStream.Write(arrayByte, 0, arrayByte.Length);
                fileStream.Close();
            }
            return caminhoSalvo;
        }
    }
}
