
namespace Core.Dominio.Entidades
{
    public class PagedItem
    {
        public FilterInfo[] filtros { get; set; }
        public OrderInfo[] orden { get; set; }
        public int startIndex  { get; set; }
        public int length { get; set; }
    }
}
