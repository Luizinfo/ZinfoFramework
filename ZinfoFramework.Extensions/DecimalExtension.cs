using System;
using System.Collections.Generic;

namespace ZinfoFramework.Extensions
{
    /// <summary>
    /// Métodos de extensão para Decimal.
    /// </summary>
    public static class DecimalExtension
    {
        /// <summary>
        /// Retorna um dicionário com o número de parcelas e o valor de cada parcela, sempre levando em consideração o valor mínimo da parcela e a quantidade máxima de parcelas passados por parâmetro.
        /// </summary>
        /// <param name="value">Valor total a ser parcelado</param>
        /// <param name="maximumNumberOfInstallments">Quantidade máxima de parcelas</param>
        /// <param name="minimumValueOfInstallment">Valor mínimo da parcela</param>
        /// <returns>Número da parcela com o valor associado</returns>
        /// <example>
        /// <code>
        /// var valorTotal = 1250.32M;
        /// var quantidadeMaximaParcelas = 5;
        /// var valorMinimoParcela = 125M;
        ///
        /// var parcelas = valorTotal.GetInstallments(quantidadeMaximaParcelas, valorMinimoParcela);
        /// </code>
        /// </example>
        ///<exception cref = "ArgumentException" >Caso o valor passado para <c>maximumNumberOfInstallments</c> ou <c>minimumValueOfInstallment</c> seja igula a zero, é diaparada uma Exceção, pois esses campos são obrigatórios e devem ter seus valores maior que zero.</exception>
        public static Dictionary<int, decimal> GetInstallments(this decimal value, int maximumNumberOfInstallments, decimal minimumValueOfInstallment)
        {
            var retorno = new Dictionary<int, decimal>();

            if (minimumValueOfInstallment <= 0 || value <= 0 || maximumNumberOfInstallments <= 0)
                throw new ArgumentException("A quantidade máxima de parcelas e o valor mínimo da parcela devem ser maiores que zero.");

            if (value < minimumValueOfInstallment)
            {
                retorno.Add(1, value);
                return retorno;
            }

            decimal valorParcela;
            for (int parcela = 1; parcela <= maximumNumberOfInstallments; parcela++)
            {
                valorParcela = value / parcela;

                if (valorParcela < minimumValueOfInstallment)
                    break;

                retorno.Add(parcela, decimal.Round(valorParcela, 2, MidpointRounding.AwayFromZero));
            }

            return retorno;
        }
    }
}
