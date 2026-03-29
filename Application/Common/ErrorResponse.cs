using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common;

public record ErrorResponse(
    int StatusCode,
    string Message,
    string? Details = null);
