
namespace Core.Dominio.Entidades
{
    using System.Collections.Generic;

    public class GridFilter
    {
        /*
         * field: "ATR_VAR_NOMBRE"
         * operator: "contains"
         * value: "no le interesa"
         * */
        public string Operator { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class GridFilters
    {
        public List<GridFilter> Filters { get; set; }
        public string Logic { get; set; }//"and"

        public GridFilters()
        {
            Filters = new List<GridFilter>();
            Logic = string.Empty;
        }
    }

    public class GridSort
    {
        public string Field { get; set; }
        public string Dir { get; set; }
    }
}
