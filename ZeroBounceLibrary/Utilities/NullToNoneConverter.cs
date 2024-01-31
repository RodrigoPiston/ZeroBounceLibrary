﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZeroBounceLibrary.Utilities
{
  
    public class NullToNoneConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return "None";
            }
            return reader.GetString();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value ?? "None");
        }
    }

}
