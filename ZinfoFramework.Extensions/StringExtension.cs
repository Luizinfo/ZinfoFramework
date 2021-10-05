using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ZinfoFramework.Extensions
{
    /// <summary>
    /// Extensões útes de <c>string</c> que podem ser utilizadas em vários cenários.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Retorna a string sem o primeiro caractere.
        /// Caso o valor seja vazio, nulo ou tenha apenas espaços é retornado o mesmo valor que foi passado.
        /// </summary>
        /// <param name="value">Valor que será removido o primeiro caractere</param>
        /// <returns>Valor sem o caracter inicial ou o valor passado</returns>
        /// <example>
        /// <code>
        /// var valor = "Um valor";
        /// var valorSemPrimeiroCaractere = valor.ToWithoutFirstCharacter(); 
        /// // <c>valorSemPrimeiroCaractere</c> terá o resultado "m valor".
        /// </code>
        /// </example>
        public static string ToWithoutFirstCharacter(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            return value.Length == 1 ? string.Empty : value.Substring(1);
        }


        /// <summary>
        /// Extrai parte de uma string a partir de duas strings como delimitadoras. 
        /// O método é case sensitive.
        /// </summary>
        /// <param name="value">String completa com a expressão necessária</param>
        /// <param name="beginDelimExpression">Delimitador inicial</param>
        /// <param name="endDelimExpression">Delimitador final</param>
        /// <returns>Parte da string ou vazio quando não encontrado.</returns>
        /// <example>
        /// <code>
        ///string retorno = @"
        ///        <![CDATA[<?xml version=""1.0"" encoding=""UTF-8""?>]]>
        ///        <![CDATA[<mse-response>]]>
        ///            <![CDATA[<status-code>19</status-code>]]>
        ///            <![CDATA[<profile>SMS_TXT_CEL_DIRETO_01</profile>]]>
        ///            <![CDATA[<transaction-id>2719011517106045503</transaction-id>]]>
        ///            <![CDATA[<ERROR-MESSAGE>CTN [21986176231] was not found or is not active in the corporate systems</ERROR-MESSAGE>]]>
        ///            <![CDATA[<error-message>CTN Billing is Pre-Active[21986176231]</error-message>]]>
        ///            <![CDATA[<error-message2></error-message2>]]>
        ///        <![CDATA[</mse-response>]]>";
        ///
        ///retorno.Extract("<![CDATA[<error-message>]]>", "<![CDATA[</error-message>]]>");
        ///    //Resultado: CTN Billing is Pre-Active[21986176231]
        ///
        ///    retorno.Extract("<![CDATA[<ERROR-MESSAGE>]]>", "<![CDATA[</ERROR-MESSAGE>]]>");
        ///    //Resultado: CTN [21986176231] was not found or is not active in the corporate systems
        ///
        ///    retorno.Extract("<![CDATA[<error-message2>]]>", "<![CDATA[</error-message2>]]>");
        ///    //Resultado: (vazio)
        ///
        ///    retorno.Extract("<![CDATA[<error-message3>]]>", "<![CDATA[</error-message3>]]>");
        ///    //Resultado: (vazio)
        /// </code>
        /// </example>
        public static string Extract(this string value, string beginDelimExpression, string endDelimExpression)
        {
            try
            {
                if (!value.Contains(beginDelimExpression) || !value.Contains(endDelimExpression)) return string.Empty;

                return value.Split(new[] { beginDelimExpression }, StringSplitOptions.None).Last().Split(new[] { endDelimExpression }, StringSplitOptions.None).First();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Retorna uma string somente com números. Caso não tenha número é retornado uma string vazia 
        /// e caso o valor seja nulo, tenha apenas espaço ou seja vazio o valor da string, é retornado o mesmo valor que foi passado.
        /// </summary>
        /// <param name="value">Valor que será removido o que não é número</param>
        /// <returns>Somente números ou o valor passado</returns>
        /// <example>
        /// <code>
        /// var valor = "ABC123def456";
        /// var somenteNumeros = valor.ToOnlyNumbers(); 
        /// // <c>somenteNumeros</c> terá o resultado "123456".
        /// </code>
        /// </example>
        public static string ToOnlyNumbers(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            Regex regex = new Regex("(?:[^0-9]|(?<=['\"])s)", RegexOptions.CultureInvariant | RegexOptions.Compiled);
            value = value.ToWithoutSpecialCharacter();
            return regex.Replace(value, string.Empty);
        }

        /// <summary>
        /// Retorna um conjunto de palavras que tenham a quantidade de letras maior ou igual ao valor passado no parâmetro <c>countLetter</c>. 
        /// Caso só tenham palavras com a quantidade de letras menor do que o valor passado no parâmetro <c>countLetter</c> e retornado uma string vazia 
        /// e caso o valor passado seja nulo, tenha somente espaço ou seja vazio é retornado o mesmo valor que foi passado.
        /// </summary>
        /// <param name="value">Conjunto de palavras</param>
        /// <param name="countLetter">Quantidade de letras para as palavras que serão mantidas no conjuntode palavras</param>
        /// <returns>Conjunto de palavras com apenas as palavras que contenham a quantidade de letras maior ou igual ao valor passado por parâmetro ou o valor passado</returns>
        /// <example>
        /// <code>
        /// var valor = "Fulano de Tal da Silva";
        /// var conjuntoDePalavras = valor.ToRemoveWords(4); 
        /// // <c>conjuntoDePalavras</c> terá o resultado "Fulano Silva".
        /// </code>
        /// </example>
        public static string ToRemoveWords(this string value, int countLetter)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var inputElements = value.Split(' ');
            var resultBuilder = new StringBuilder();

            foreach (var element in inputElements)
                if (element.Length >= countLetter)
                    resultBuilder.Append(element + " ");

            return resultBuilder.ToString().Trim();
        }

        /// <summary>
        /// Retorna a string respeitando o número máximo de caracteres removendo sempre os últimos caracteres que passarem do valor passado em <c>maxLength</c>.
        /// </summary>
        /// <param name="value">Valor que será truncado</param>
        /// <param name="maxLength">Tamanho máximo que deve conter o valor passado</param>
        /// <returns>Valor passado sem os últimos caracteres ou o valor passado</returns>
        /// <example>
        /// <code>
        /// var valor = "Inconstitucionalissimamente";
        /// var valorTruncado = valor.ToTruncateWithMaxLength(16); 
        /// // <c>valorTruncado</c> terá o resultado "Inconstitucional"
        /// </code>
        /// </example>
        public static string ToTruncateWithMaxLength(this string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            return value?.Length > maxLength ? value.Substring(0, maxLength) : value;
        }

        /// <summary>
        /// Retorna a string sem caracteres especiais. Caso o valor seja vazio, nulo ou tenha apenas espaços é retornado o mesmo valor que foi passado.
        /// </summary>
        /// <param name="value">valor que será removido os caracteres especiais</param>
        /// <returns>Valor sem os caracteres especiais ou o valor passado</returns>
        /// <example>
        /// <code>
        /// var valor = "21.000-000";
        /// var valorSemCaracteresEspeciais = valor.ToWithoutSpecialCharacter(); 
        /// // <c>valorSemCaracteresEspeciais</c> terá o resultado "21000000"
        /// </code>
        /// </example>
        public static string ToWithoutSpecialCharacter(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var regex = new Regex("(?:[^a-z0-9A-ZÁÉÍÓÚÂÊÔÀÔÃÇáéíóúâêôàõãç ]|(?<=['\"])s)", RegexOptions.CultureInvariant | RegexOptions.Compiled);

            regex.Replace(value, string.Empty);

            var result = string.Empty;

            const string vWithAccent = "ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÒÓÔÕÖÙÚÛÜàáâãäåçèéêëìíîïòóôõöùúûü";
            const string vWithoutAccent = "AAAAAACEEEEIIIIOOOOOUUUUaaaaaaceeeeiiiiooooouuuu";

            for (var i = 1; (i <= value.Length); i++)
            {
                int vPos = (vWithAccent.IndexOf(value.Substring((i - 1), 1), 0, comparisonType: StringComparison.Ordinal) + 1);

                if ((vPos > 0))
                    result += vWithoutAccent.Substring((vPos - 1), 1);
                else
                    result += value.Substring((i - 1), 1);
            }

            return result;
        }

        /// <summary>
        /// Retorna a string com a primeira letra maiúscula. Caso o valor seja vazio, nulo ou tenha apenas espaços é retornado o mesmo valor que foi passado.
        /// </summary>
        /// <param name="value">Valor que será alterado</param>
        /// <returns>Valor com a primeira letrá maiúscula ou o valor passado</returns>
        /// <example>
        /// <code>
        /// var valor = "framework";
        /// var valorFormatado = valor.ToUpperCaseFirstLetter(); 
        /// // <c>valorFormatado</c> terá o resultado "Framework"
        /// </code>
        /// </example>
        public static string ToUpperCaseFirstLetter(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            return char.ToUpperInvariant(value[0]) + value.Substring(1);
        }

        /// <summary>
        /// Retorna a string com a primeira letra maiúscula. Caso o valor seja vazio, nulo ou tenha apenas espaços é retornado o mesmo valor que foi passado.
        /// </summary>
        /// <param name="value">Valor que será alterado</param>
        /// <returns>Valor com a primeira letrá maiúscula ou o valor passado</returns>
        /// <example>
        /// <code>
        /// var valor = "FrameworkZinfo";
        /// var valorFormatado = valor.ToLowerCaseFirstLetter(); 
        /// // <c>valorFormatado</c> terá o resultado "frameworkZinfo"
        /// </code>
        /// </example>
        public static string ToLowerCaseFirstLetter(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }

        /// <summary>
        /// Retorna com a primeira letra de cada palavra passada como maiúscula. Caso o valor seja vazio, nulo ou tenha apenas espaços é retornado o mesmo valor que foi passado.
        /// </summary>
        /// <param name="value">Palavras que serão convertidas as primeiras letras</param>
        /// <returns>Palavras com as primeiras letras maiúsculas ou o valor passado</returns>
        /// <example>
        /// <code>
        /// var valor = "framework Zinfo";
        /// var valorFormatado = valor.ToUpperCaseFirstLetterEachWord();
        /// // <c>valorFormatado</c> terá o resultado "Framework Zinfo"
        /// </code>
        /// </example>
        public static string ToUpperCaseFirstLetterEachWord(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            value = value.ToStandardSpaces().ToLower();

            string[] strSplit = value.Split(' ');
            string novString = "";

            for (int i = 0; i < strSplit.Length; i++)
            {
                if (i > 0 && strSplit[i].Length <= 2)
                {
                    novString += strSplit[i] + " ";
                    continue;
                }

                novString += ToUpperCaseFirstLetter(strSplit[i]) + " ";
            }

            return novString.Trim();
        }

        /// <summary>
        /// Retorna palavras com os espaços padronizados. Caso o valor seja vazio, nulo ou tenha apenas espaços é retornado o mesmo valor que foi passado.
        /// </summary>
        /// <param name="value">Valor com as palavras que serão padronizadas</param>
        /// <returns>Palavras com espaços padronizados</returns>
        /// <example>
        /// <code>
        /// var valor = "Uma    frase        com espaços."
        /// var valorFormatado = valor.ToStandardSpaces();
        /// // <c>valorFormatado</c> terá o resultado "Uma frase com espaços."
        /// </code>
        /// </example>
        public static string ToStandardSpaces(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var regex = new Regex(@"\s+", RegexOptions.CultureInvariant | RegexOptions.Compiled);

            value = value.Replace("\t", " ");

            return regex.Replace(value, " ");
        }

        /// <summary>
        /// Retrorna a verificação se existe números no valor pássado.
        /// </summary>
        /// <param name="value">Valor que será verificado a existência de números</param>
        /// <returns>Verificação como verdadeiro ou falso para exisência de números</returns>
        /// <example>
        /// <code>
        /// var valor = "abc123";
        /// var verificacao = valor.ContainsNumbers();
        /// // <c>verificacao</c> terá o resultado "true"
        /// </code>
        /// </example>
        public static bool ContainsNumbers(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            return Regex.IsMatch(value, @"(?=.*\d)", RegexOptions.CultureInvariant | RegexOptions.ECMAScript);
        }

        /// <summary>
        /// Retorna a verificação se existe letras maiúsculas no valor passado.
        /// </summary>
        /// <param name="value">Valor que será verificado a existência de letras maiúsculas</param>
        /// <returns>Verificação com verdadeiro ou falso para existência de letra maiúscula</returns>
        /// <example>
        /// <code>
        /// var valor = "ABCdef";
        /// var verificacao = valor.ContainsUpperCaseLetter();
        /// // <c>verificacao</c> terá o resultado "true"
        /// </code>
        /// </example>
        public static bool ContainsUpperCaseLetter(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            return Regex.IsMatch(value, @"(?=.*[A-Z])", RegexOptions.CultureInvariant | RegexOptions.ECMAScript);
        }

        /// <summary>
        /// Retorna o valor referente a um código em base 64
        /// </summary>
        /// <param name="value">Valor codificado em base 64</param>
        /// <returns>Valor decodificado</returns>
        /// <example>
        /// Entrada: <c>YWRtaW46dGVzdGVXb296YQ==</c>
        /// Saída: <c>admin:teste==</c>
        ///</example>
        public static string FromBase64(this string value) => string.IsNullOrWhiteSpace(value) ? string.Empty : Encoding.UTF8.GetString(Convert.FromBase64String(value));

        /// <summary>
        /// Retorna o valor codificado em base 64
        /// </summary>
        /// <param name="value">Valor decodificado</param>
        /// <returns>Valor codificado em base 64</returns>
        /// <example>
        /// Entrada: <c>admin:testeZinfo==</c>
        /// Saída: <c>YWRtaW46dGVzdGVXb296YQ==</c>
        ///</example>
        public static string ToBase64(this string value) => string.IsNullOrWhiteSpace(value) ? string.Empty : Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

        /// <summary>
        /// Codifica em uma lista as credenciais quando passado um valor em base 64
        /// </summary>
        /// <param name="value">Valor codificado em base 64</param>
        /// <returns>Lista com as strings decodificadas</returns>
        /// <example>
        /// <code>
        /// ConcurrentDictionary<![CDATA[<string, string>]]> credenciais = "YWRtaW46dGVzdGVXb296YQ".FromCredentialBase64();
        /// </code>
        /// Saída: Objeto ConcurrentDictionary com a estrutura {key, value}: {"user", "admin"} e {"password", "testeZinfo"}
        ///</example>
        ///<exception cref = "NullReferenceException" >Caso a string seja nula ou vazia.</exception>
        ///<exception cref = "FormatException" >Caso o valor decodificado não contenha a estrutura "user":"password".</exception>
        public static ConcurrentDictionary<string, string> FromCredentialBase64(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new NullReferenceException("A string não pode ser nula ou vazia");

            var valorDecodificado = value.FromBase64();

            if (!valorDecodificado.Contains(":")) throw new FormatException("O valor passado não contém o formato correto");
            var valorSplitado = valorDecodificado.Split(':');

            var credencial = new ConcurrentDictionary<string, string>();
            credencial.TryAdd("user", valorSplitado[0]);
            credencial.TryAdd("password", valorSplitado[1]);

            return credencial;
        }
    }
}