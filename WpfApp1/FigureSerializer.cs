using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using GameOOTP.GameElements.Figures;

namespace GameOOTP
{
    public class FigureSerializer
    {
        //Figure figure;

        public void Serialize(Figure figure)
        {
            
        }
    }
    /*
        public abstract class BaseClass
        {
            public string BaseField { get; set; }
        }

        public class DerivedClass : BaseClass
        {
            public int DerivedField { get; set; }

            public DerivedClass(string baseField, int derivedField)
            {
                BaseField = baseField;
                DerivedField = derivedField;
            }
        }

        public class DerivedClassConverter : JsonConverter<DerivedClass>
        {
            public override DerivedClass Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                string baseField = null;
                int derivedField = 0;

                while (reader.Read())

                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                        break;

                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string propertyName = reader.GetString();
                        reader.Read();

                        if (propertyName == nameof(BaseClass.BaseField))
                            baseField = reader.GetString();
                        else if (propertyName == nameof(DerivedClass.DerivedField))
                            derivedField = reader.GetInt32();
                    }
                }

                return new DerivedClass(baseField, derivedField);
            }

            public override void Write(Utf8JsonWriter writer, DerivedClass value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                writer.WriteString(nameof(BaseClass.BaseField), value.BaseField);
                writer.WriteNumber(nameof(DerivedClass.DerivedField), value.DerivedField);
                writer.WriteEndObject();
            }
        }
    */

}
