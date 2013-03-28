using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Data
{
    public abstract class Identity
    {
        private int id;
        public int getId()
        {
            return id;
        }
        public void setId(int id)
        {
            this.id = id;
        }
    }


}
