﻿using SQLite;

using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignExchange.Models
{
    public class Rate
    {
        [PrimaryKey, AutoIncrement]
        public int RateId { get; set; }
        public string Code { get; set; }
        public double TaxRate { get; set; }
        public string Name { get; set; }

     
    }
}
