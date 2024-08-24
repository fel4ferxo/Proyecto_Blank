
namespace Core.Dominio.Entidades
{
    public class FilterInfo
    {
        public Logical Logical { get; set; }   // and , or
        public string PropertyName { get; set; } // nombre de la columna    
        public string Value { get; set; } //valor de la columna
        private Operator _op = Operator.Contains;
        public Operator Operator
        {
            get { return _op; }
            set { _op = value; }
        }
    }

    public enum Logical
    {
        OR,
        AND
    }
}
