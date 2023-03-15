using System.IO;

namespace ZinfoFramework.Extensions
{
    /// <summary>
    /// Extensões para stream.
    /// </summary>
    public static class StreamExtension
    {
        /// <summary>
        /// Retorna um array de bytes.
        /// </summary>
        /// <param name="value">Stream que será convertido em array de bytes.</param>
        /// <returns>Areray de byte</returns>
        /// <example>
        /// <code>
        /// //Criando stream de string
        /// var valor = "Stream de string";
        /// var stream = new MemoryStream();
        /// var writer = new StreamWriter(stream);
        /// writer.Write(valor);
        /// writer.Flush();
        /// stream.Position = 0;
        /// 
        /// //Exemplo
        /// var bytesArray = stream.ToByteArray(); //Resultado para<c>bytesArray</c> [0]83[1]116[2]114[3]101[4]97[5]109[6]32[7]100[8]101[9]32[10]115[11]116[12]114[13]105[14]110[15]103
        /// </code>
        /// </example>
        public static byte[] ToByteArray(this Stream value)
        {
            if (value == null)
                return null;

            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = value.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}