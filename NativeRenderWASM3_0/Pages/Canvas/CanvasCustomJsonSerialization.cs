using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NativeRenderWASM3_0
{
    public class NodeRuntimeIgnoreConverter : JsonConverter<Node>
    {
        public override Node Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var wf = new Node();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return wf;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "X":
                            float x = float.Parse(reader.GetString());
                            wf.X = x;
                            break;
                        case "Y":
                            float y = float.Parse(reader.GetString());
                            wf.Y = y;
                            break;
                        case "Width":
                            float w = float.Parse(reader.GetString());
                            wf.Width = w;
                            break;
                        case "Height":
                            float h = float.Parse(reader.GetString());
                            wf.Height = h;
                            break;
                        case "Annotations":
                            JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
                            {
                                IgnoreNullValues = true,
                            };
                            List<Annotation> labels = JsonSerializer.Deserialize<List<Annotation>>(reader.GetString());
                            wf.Annotations = labels;
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Node wf, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("X", wf.X.ToString());
            writer.WriteString("Y", wf.Y.ToString());
            writer.WriteString("Width", wf.Width.ToString());
            writer.WriteString("Height", wf.Height.ToString());
            writer.WriteString("Annotations", JsonSerializer.Serialize(wf.Annotations));

            writer.WriteEndObject();
        }
    }
}
