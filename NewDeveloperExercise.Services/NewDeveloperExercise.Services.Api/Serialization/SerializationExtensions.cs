using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NewDeveloperExercise.Services.Api.Serialization;

public static class SerializationExtensions
{
    public static JsonSerializerOptions ConfigureSerializerOptions(this JsonSerializerOptions options)
    {
        options.Converters.Add(new JsonStringEnumConverter());
        //options.Converters.Add(new StringValueObjectJsonConverter());
        //options.Converters.Add(new JsonMergePatchConverter(options));
        //options.Converters.Add(new IntValueObjectJsonConverter());
        //options.Converters.Add(new DoubleValueObjectJsonConverter());
        //options.Converters.Add(new DateTimeOffsetConverter());
        options.PropertyNameCaseInsensitive = true;
        return options;
    }
}