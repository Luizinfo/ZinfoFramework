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

        /// <summary>
        /// Método que informa se o número informado é ímpar.
        /// </summary>
        /// <param name="x"></param>
        /// <returns>true se o número for ímpar e false se for par.</returns>
        public static bool IsOdd(this int x) => x.IsEven() ? false : true;

        /// <summary>
        /// Método que informa se o número informado é par.
        /// </summary>
        /// <param name="x"></param>
        /// <returns>true se o número for par e false se for ímpar.</returns>
        public static bool IsEven(this int x) => (x % 2 == 0) ? true : false;
    }
}