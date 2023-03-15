using System;
using System.ComponentModel;
using System.Linq;

namespace ZinfoFramework.Extensions
{
    /// <summary>
    /// Métodos de extensão para Enum.
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Obtem a descrição do enum. Para obter a descrição no enum é necessário que o mesmo esteja decorado com <c>[Description("Sua Descrição")]</c>.
        /// </summary>
        /// <param name="value">Objeto enum do tipo primitivo <c>enum</c></param>
        /// <returns>Texto com a descrição decorada no Enum</returns>
        /// <example>
        /// <code>
        /// public enum TipoPessoa
        /// {
        ///     [Description("Cadastro Nacional Pessoa Física")]
        ///     PF=1,
        ///     [Description("Cadastro Nacional Pessoa Física")]
        ///     PJ =2
        /// }
        /// 
        /// public class DescricaoEnum
        /// {
        ///     public void ObterDescricao()
        ///     {
        ///         var pfEnum = TipoPessoa.PF;
        ///         var descricao = pfEnum.ToDescription();
        ///     }
        /// }
        /// </code>
        /// </example>
        public static string ToDescription(this Enum value) => value.GetAttribute<DescriptionAttribute>() == null ? value.ToString() : value.GetAttribute<DescriptionAttribute>().Description;

        /// <summary>
        /// Obter o objeto do tipo <c>enum</c> pela descrição decorada como <c>[Description("Sua descrição)"]</c>.
        /// </summary>
        /// <typeparam name="TEnum">Enum no qual vai ser retornado</typeparam>
        /// <param name="description">Texto com a descrição decorada no Enum</param>
        /// <returns>Enum com o valor seleconado de acordo com a descrição passada</returns>
        /// <example>
        /// <code>
        /// public enum TipoPessoa
        /// {
        ///     [Description("Cadastro Nacional Pessoa Física")]
        ///     PF=1,
        ///     [Description("Cadastro Nacional Pessoa Física")]
        ///     PJ =2
        /// }
        /// 
        /// public class DescricaoEnum
        /// {
        ///     public void ObterEnum()
        ///     {
        ///         var pfDescricao = "Cadastro Nacional Pessoa Física";
        ///         var enumPF = EnumExtension.GetByDescription <c>TipoPessoa</c> (pfDescricao);
        ///     }
        /// }
        /// </code>
        /// </example>
        ///<exception cref = "ArgumentException" >Caso o tipo genérico passado seja diferente do tipo primitívo <c>Enum</c> é diaparada uma Exceção, pois o código precisa identificar onde está a descrição para retornar o Enum selecionado.</exception>
        public static Enum GetByDescription<TEnum>(string description)
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("O tipo passado não é um tipo Enum válido.");

            foreach (var field in typeof(TEnum).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute && attribute.Description.ToLower() == description.ToLower())
                    return (Enum)field.GetValue(null);

                if (field.Name.ToLower() == description.ToLower())
                    return (Enum)field.GetValue(null);
            }

            return default(Enum);
        }

        private static T GetAttribute<T>(this Enum enumType) where T : Attribute => enumType.GetType().GetField(enumType.ToString()).GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;

    }
}