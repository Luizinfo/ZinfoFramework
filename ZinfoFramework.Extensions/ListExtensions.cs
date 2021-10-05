using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ZinfoFramework.Extensions
{
    /// <summary>
    /// Extensão para listas.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Retornar um objeto do tipo <c>PagedList</c> a partir de um <c>IQueryable</c>
        /// </summary>
        /// <param name="source">A colecao que vai ser paginada</param>
        /// <param name="order">Exemplo: (e => e.Id)</param>
        /// <param name="page">Numero da pagina</param>
        /// <param name="itemsPerPage">Registros por pagina</param>
        /// <param name="orderedAsc">Ordenação, por padrão crescente</param>
        /// <example>
        /// <code>
        /// var clientes; //Lista de Clientes do tipo List
        ///
        /// var query = clientes.AsQueryable();
        ///
        /// var clientesPaginados = query.ToPagedList(c => c.IdCliente, 2, 3);
        /// </code>
        /// </example>
        public static PagedList<TSource> ToPagedList<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> order, int page, int itemsPerPage, bool orderedAsc = true)
        {
            if (page == 0)
                page = 1;

            if (itemsPerPage == 0)
                itemsPerPage = 1;

            var pagedList = new PagedList<TSource>(page, itemsPerPage, orderedAsc);

            var query = orderedAsc ? source.OrderBy(order) : source.OrderByDescending(order);

            pagedList.Items = query
                    .Skip(itemsPerPage * (page - 1))
                    .Take(itemsPerPage).ToList();

            pagedList.TotalCount = source.Count();
            pagedList.TotalPages = (pagedList.TotalCount == 0 ? 1 : ((pagedList.TotalCount - 1) / itemsPerPage) + 1);

            return pagedList;
        }

        /// <summary>
        /// Concatena uma lista de strings em uma única string utilizando o delimitador indicado. Caso o valor seja vazio ou nulo é retornado uma string vazia.
        /// </summary>
        /// <param name="value">Lista com os valores que serão concatenados</param>
        /// <param name="delimiter">Delimitador que vai separar os valores dentro da lista</param>
        /// <returns>Um único valor concatenado com o delimitador passado</returns>
        /// <example>
        /// <code>
        /// var valores; // Lista de string
        /// var valores.Add("Framework");
        /// var valores.Add("Zinfo");
        /// var valores.Add("2.0");
        /// 
        /// var valorConcatenado = valores.ToInLineConcat(" | ");
        /// // <c>valorConcatenado</c> terá o resultado "Framework | Zinfo | 2.0"
        /// </code>
        /// </example>
        public static string ToInLineConcat(this List<string> value, string delimiter = "")
        {
            if (value == null || !value.Any()) return string.Empty;

            return value.Aggregate((i, j) => i + delimiter + j);
        }
    }

    /// <summary>
    /// Classe utilizada para extensão de lista paginada.
    /// </summary>
    /// <typeparam name="T">Lista de objetos que será exibido em <c>Itens</c></typeparam>
    [DataContract]
    [Serializable]
    public class PagedList<T>
    {
        /// <summary>
        /// Construtor padrão para criar um tipo PagedList completo. Muito útil no momento de transformar o objeto e passar os valores de uma lista paginada para outro objeto.
        /// </summary>
        /// <param name="items">Coleção de itens</param>
        /// <param name="totalCount">Total de registros</param>
        /// <param name="totalPages">Total paginas</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="itemsPerPage">Total páginas</param>
        /// <param name="orderedAsc">Ordenação crescente se verdadeiro</param>
        public PagedList(List<T> items, int totalCount, int totalPages, int currentPage, int itemsPerPage, bool orderedAsc)
            : this(currentPage, itemsPerPage, orderedAsc)
        {
            Items = items;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }

        internal PagedList(int currentPage, int itemsPerPage, bool orderedAsc)
            : this()
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            OrderedAsc = orderedAsc;
        }

        private PagedList() { }

        /// <summary>
        /// Coleção de itens que foram paginados
        /// </summary>
        [DataMember]
        public IList<T> Items { get; set; } = new List<T>(0);

        /// <summary>
        /// Total de registros incluindo todos registros.
        /// </summary>
        [DataMember]
        public int TotalCount { get; internal set; }

        /// <summary>
        /// Total paginas incluindo todos registros.
        /// </summary>
        [DataMember]
        public int TotalPages { get; internal set; }

        /// <summary>
        /// Página atual.
        /// </summary>
        [DataMember]
        public int CurrentPage { get; internal set; }

        /// <summary>
        /// Total de itens por página.
        /// </summary>
        [DataMember]
        public int ItemsPerPage { get; internal set; }

        /// <summary>
        /// Ordenação crescente ou não.
        /// </summary>
        [DataMember]
        public bool OrderedAsc { get; internal set; }

    }
}
