﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DataAccessLayer.Interfaces
{
    public interface ISortHelper<T>
    {
        IQueryable<T> ApplySort(ref IQueryable<T> entities, string orderByQueryString);
    }
}
