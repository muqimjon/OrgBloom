﻿namespace EcoLink.ApiService.Helpers;

public class ConvertHelper
{
    public static StringContent ConvertToStringContent<T>(T dto) where T : class
        => new (JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

    //public static MultipartFormDataContent ConvertToMultipartFormContent<T>(T dto)
    //{
    //    var properties = typeof(T).GetProperties();
    //    var multipartFormContent = new MultipartFormDataContent();

    //    foreach (var property in properties)
    //    {
    //        var value = property.GetValue(dto);
    //        if (value != null)
    //            if (property.PropertyType == typeof(IFormFile))
    //            {
    //                var formFile = (IFormFile)value;
    //                StreamContent streamContent = new(formFile.OpenReadStream());
    //                multipartFormContent.Add(streamContent, property.Name, formFile.FileName);
    //            }
    //            else
    //                multipartFormContent.Add(new StringContent(value.ToString()!), property.Name);
    //    }

    //    return multipartFormContent;
    //}
}
