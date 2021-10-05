using System;

namespace ZinfoFramework.Extensions
{
    /// <summary>
    /// Extensões para o DateTime.
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Retorna a diferença de dias entre duas datas.
        /// </summary>
        /// <param name="date">Data menor</param>
        /// <param name="dateCompare">Data maior</param>
        /// <returns>Diferença de dias</returns>
        /// <example>
        /// <code>
        /// var data = DateTime.Now;
        /// var dataComparacao = DateTime.Now;
        ///
        /// dataComparacao = dataComparacao.AddDays(1);
        /// 
        /// var dias = data.ToDaysBetweenDifferentDates(dataCompraracao);
        /// </code>
        /// </example>
        public static int ToDaysBetweenDifferentDates(this DateTime date, DateTime dateCompare) => (dateCompare - date).Days;

        /// <summary>
        /// Retorna a diferença de meses entre duas datas. Não arredonda para cima.
        /// </summary>
        /// <param name="date">Data menor</param>
        /// <param name="dateCompare">Data maior</param>
        /// <returns>Direrença de meses</returns>
        /// <example>
        /// <code>
        /// var data = DateTime.Now;
        /// var dataComparacao = DateTime.Now;
        ///
        /// dataComparacao = dataComparacao.AddMonths(1);
        /// 
        /// var meses = data.ToMonthsBetweenDifferentDates(dataCompraracao);
        /// </code>
        /// </example>
        public static int ToMonthsBetweenDifferentDates(this DateTime date, DateTime dateCompare)
        {
            int compMonth = (dateCompare.Month + dateCompare.Year * 12) - (date.Month + date.Year * 12);
            double daysInEndMonth = (dateCompare - dateCompare.AddMonths(1)).Days;
            double months = compMonth + (date.Day - dateCompare.Day) / daysInEndMonth;

            return Convert.ToInt32(Math.Truncate(months));
        }

        /// <summary>
        /// Retorna a diferença de anos entre duas datas.
        /// </summary>
        /// <param name="date">Data menor</param>
        /// <param name="dateCompare">Data maior</param>
        /// <returns>Diferença de anos</returns>
        /// <example>
        /// <code>
        /// var data = DateTime.Now;
        /// var dataComparacao = DateTime.Now;
        ///
        /// dataComparacao = dataComparacao.AddYears(1);
        /// 
        /// var anos = data.ToYearsBetweenDifferentDates(dataCompraracao);
        /// </code>
        /// </example>
        public static int ToYearsBetweenDifferentDates(this DateTime date, DateTime dateCompare) => (dateCompare - date).Days / 365;

        /// <summary>
        /// Retorna o último dia do mês.
        /// </summary>
        /// <param name="date">Data</param>
        /// <returns>Último dia</returns>
        /// <example>
        /// <code>
        /// var data = DateTime.Now;
        /// var ultimoDiaDoMes = data.ToLastDayOfMonth();
        /// </code>
        /// </example>
        public static DateTime ToLastDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

        /// <summary>
        /// Indica se a data é o último dia do mês.
        /// </summary>
        /// <param name="date">Data</param>
        /// <returns>Verificação verdadeira ou falsa</returns>
        /// <example>
        /// <code>
        /// var data = DateTime.Now;
        /// var ehUltimoDiaDoMes = data.IsLastDayOfMonth();
        /// </code>
        /// </example>
        public static bool IsLastDayOfMonth(this DateTime date) => date.Date == date.ToLastDayOfMonth().Date;

        /// <summary>
        /// Retorna o primeiro dia do mês.
        /// </summary>
        /// <param name="date">Data</param>
        /// <returns>Primeiro dia</returns>
        /// <example>
        /// <code>
        /// var data = DateTime.Now;
        /// var primeiroDiaDoMes = data.ToFirstDayOfMonth();
        /// </code>
        /// </example>
        public static DateTime ToFirstDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, 1);

        /// <summary>
        /// Indica se a data é o primeiro dia do mês.
        /// </summary>
        /// <param name="date">Data</param>
        /// <returns>Verificação verdadeira ou falsa</returns>
        /// <example>
        /// <code>
        /// var data = DateTime.Now;
        /// var ehPrimeiroDiaDoMes = data.IsFirstDayOfMonth();
        /// </code>
        /// </example>
        public static bool IsFirstDayOfMonth(this DateTime date) => date.Date == date.ToFirstDayOfMonth().Date;

        /// <summary>
        /// Retorna a quantidade de dias restantes até o final do mês.
        /// </summary>
        /// <param name="date">Data</param>
        /// <param name="includeOneDay">Incluir mais um dia</param>
        /// <returns>Dias até o final do mês</returns>
        /// <example>
        /// <code>
        /// var data = DateTime.Now;
        /// var diasAteFimDoMes = data.ToDaysToEndOfMonth();
        /// 
        /// var incluirMaisUmDia = true;
        /// diasAteFimDoMes = data.ToDaysToEndOfMonth(incluirMaisUmDia);
        /// </code>
        /// </example>
        public static int ToDaysToEndOfMonth(this DateTime date, bool includeOneDay)
        {
            var ultimoDiaMes = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            return Convert.ToInt32(ultimoDiaMes.Subtract(date).TotalDays) + (includeOneDay ? 1 : 0);
        }

        /// <summary>
        /// Retorna a quantidade de dias restantes até o final do ano.
        /// </summary>
        /// <param name="date">Data</param>
        /// <param name="includeOneDay">Incluir mais um dia</param>
        /// <returns>Dias até o final do ano</returns>
        /// <example>
        /// <code>
        /// var data = DateTime.Now;
        /// var diasAteFimDoAno = data.ToDaysToEndOfYear();
        /// 
        /// var incluirMaisUmDia = true;
        /// diasAteFimDoAno = data.ToDaysToEndOfYear(incluirMaisUmDia);
        /// </code>
        /// </example>
        public static int ToDaysToEndOfYear(this DateTime date, bool includeOneDay)
        {
            var ultimoDiaMes = new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, date.Month));

            return Convert.ToInt32(ultimoDiaMes.Subtract(date).TotalDays) + (includeOneDay ? 1 : 0);
        }

        /// <summary>
        /// Retorna a data com a última hora, colocando a hora como 23:59:59.
        /// </summary>
        /// <param name="date">Data</param>
        /// <returns>Data com a última hora</returns>
        /// <example>
        /// <code>
        /// var data = DateTime.Now;
        /// var dataComTempo_23_59_59 = data.ToDateWithLastMinute();
        /// </code>
        /// </example>
        public static DateTime ToDateWithLastMinute(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

        /// <summary>
        /// Tenta converter em data, caso não consiga retorna nulo.
        /// </summary>
        /// <param name="value">Texto para tentar transformar em data</param>
        /// <returns>Data transformada ou nulo</returns>
        /// <example>
        /// <code>
        /// var data = DateTimeExtension.TryParseDateTimeNullable("16/01/2018");
        /// </code>
        /// </example>
        public static DateTime? TryParseDateTimeNullable(string value) => DateTime.TryParse(value, out DateTime date) ? (DateTime?)date : null;

        /// <summary>
        /// Calcula a idade.
        /// </summary>
        /// <param name="dateBirth">Data de nascimento</param>
        /// <param name="dateCompare">Data na qual a idade será calculada. Caso não seja passado valor, será assumido o dia atual.</param>
        /// <returns>Idade</returns>
        /// <example>
        /// <code>
        /// var dataAniversario = DateTime.Now.AddDays(-30);
        ///
        /// var idade = dataAniversario.ToAge();
        /// 
        /// //Ou
        /// 
        /// var dataComparacao = DateTime.Now;
        /// 
        /// idade = dataAniversario.ToAge(dataComparacao);
        /// </code>
        /// </example>
        public static int ToAge(this DateTime dateBirth, DateTime? dateCompare = null)
        {
            var dataNascimentoNormalizada = dateBirth.Date;

            if (dateCompare.HasValue)
                dateCompare = dateCompare.Value.Date;
            else
                dateCompare = DateTime.Today;

            var idade = dateCompare.Value.Year - dataNascimentoNormalizada.Year;

            if (dataNascimentoNormalizada > dateCompare.Value.AddYears(-idade))
                idade--;

            return idade;
        }
    }
}
