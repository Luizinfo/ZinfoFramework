using System;
using System.Linq.Expressions;

namespace ZinfoFramework.Extensions
{
    /// <summary>
    /// Extensões para objetos.
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Retorna o nome do campo identificado na expressão. É retornado exatamente da forma como está no objeto.
        /// </summary>
        /// <typeparam name="T">Tipo no qual se encontra o campo</typeparam>
        /// <param name="value">Expressão com o objeto onde está o campo para que seja retornado o nome.</param>
        /// <returns>String representando o nome do campo</returns>
        /// <example>
        /// <code>
        /// public class Exemplo
        /// {
        ///     public class Cliente
        ///     {
        ///         public DateTime DataNascimento { get; set; }
        ///
        ///         public string ObterNomeCampo<![CDATA[<T>]]>(Expression<![CDATA[<Func<T, object>>]]> campo)
        ///         {
        ///             return campo.GetFieldName();
        ///         }
        ///     }
        ///
        ///     public string ObterNomeCampo()
        ///     {
        ///         var cliente = new Cliente();
        ///         return cliente.ObterNomeCampo<![CDATA[<Cliente>]]>(e => e.DataNascimento);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static string GetFieldName<T>(this Expression<Func<T, object>> value)
        {
            var member = (value.Body as MemberExpression);
            if (member != null)
            {
                return member.Member?.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// Retorna o nome do campo identificado na expressão. É retornado com a primeira letra minúscula.
        /// </summary>
        /// <typeparam name="T">Tipo no qual se encontra o campo</typeparam>
        /// <param name="value">Expressão com o objeto onde está o campo para que seja retornado o nome.</param>
        /// <returns>String representando o nome do campo</returns>
        /// <example>
        /// <code>
        /// public class Exemplo
        /// {
        ///     public class Cliente
        ///     {
        ///         public DateTime DataNascimento { get; set; }
        ///
        ///         public string ObterNomeCampo<![CDATA[<T>]]>(Expression<![CDATA[<Func<T, object>>]]> campo)
        ///         {
        ///             return campo.GetFieldNameLowerCaseFirstLetter();
        ///         }
        ///     }
        ///
        ///     public string ObterNomeCampo()
        ///     {
        ///         var cliente = new Cliente();
        ///         return cliente.ObterNomeCampo<![CDATA[<Cliente>]]>(e => e.DataNascimento);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static string GetFieldNameLowerCaseFirstLetter<T>(this Expression<Func<T, object>> value)
        {
            var field = string.Empty;

            var member = (value.Body as MemberExpression);
            if (member != null)
            {
                field = member.Member?.Name;

                if (field.Length > 1)
                    field = field.ToLowerCaseFirstLetter();
            }

            return field;
        }
    }
}