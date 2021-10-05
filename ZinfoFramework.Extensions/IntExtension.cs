namespace ZinfoFramework.Extensions
{
    /// <summary>
    /// Extensões para inteiro.
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// Tenta converter em inteiro, caso não consiga retorna nulo.
        /// </summary>
        /// <param name="value">Texto para tentar transformar em inteiro</param>
        /// <returns>inteiro transformada ou nulo</returns>
        /// <example>
        /// <code>
        /// var valor = IntExtension.TryParseIntNullable("2018");
        /// </code>
        /// </example>
        public static int? TryParseIntNullable(string value) => int.TryParse(value, out int integrer) ? (int?)integrer : null;
    }
}
