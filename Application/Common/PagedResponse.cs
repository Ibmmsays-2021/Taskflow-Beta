using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common;

public record PagedResponse<T>(IEnumerable<T> Items, int TotalCount, int PageNumber, int PageSize);
