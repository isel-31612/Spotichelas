using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public abstract class Identity
    {
        [Key]
        public int id { get; set; }
        public abstract bool match(object o);
    }

}