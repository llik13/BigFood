using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DataAccessLayer.Pagination.Parametrs
{
    public class ProductParametrs : QueryStringParameters
    {
        public uint MinCost { get; set; }
        public uint MaxCost { get; set; }

        public bool ValidCostRange => MaxCost > MinCost;

        public string? Name { get; set; }
    }
}
