﻿using System.Text;
using System.Text.Json;

namespace MedX.ApiService.Helpers;

public class ConvertHelper
{
    public static StringContent ConvertToStringContent(dynamic model)
        => new(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
}
