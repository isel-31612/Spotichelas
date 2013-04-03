﻿using SpotiChelas.DomainModel.Data.DBAttributes;
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
        [DBField]
        [DBPrimaryKey]
        public int getId()
        {
            return id;
        }
        public void setId(int id)
        {
            this.id = id;
        }

        public abstract bool match(object o);
    }


}