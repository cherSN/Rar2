﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmRar.Rar
{
    public class RarSubdevision
    {
        #region - Public Properties -
        public string Name { set; get; }
        public string KPP { set; get; }
        public RarAdress Adress { set; get; }
        #endregion
        #region - Constructor -
        public RarSubdevision()
        {
            Adress = new RarAdress("");
        }
        #endregion
        public override string ToString()
        {
            return Name + " КПП: " + KPP + " " + Adress.ToString();
        }
    }
}
