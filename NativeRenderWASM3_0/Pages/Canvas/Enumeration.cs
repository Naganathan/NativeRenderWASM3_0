using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NativeRenderWASM3_0
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Shapes
    {
        [EnumMember(Value = "Basic")]
        Basic,
        [EnumMember(Value = "Path")]
        Path,
        [EnumMember(Value = "Text")]
        Text,
        [EnumMember(Value = "Image")]
        Image,
        [EnumMember(Value = "Flow")]
        Flow,
        [EnumMember(Value = "Bpmn")]
        Bpmn,
        [EnumMember(Value = "Native")]
        Native,
        [EnumMember(Value = "HTML")]
        HTML,
        [EnumMember(Value = "UmlActivity")]
        UmlActivity,
        [EnumMember(Value = "UmlClassifier")]
        UmlClassifier,
        [EnumMember(Value = "SwimLane")]
        SwimLane,
    }

    [Flags]
    public enum DiagramConstraints
    {
        ApiUpdate = 1 << 2,
        Default = 1 << 2 | 1 << 4,
        None = 1 << 0,
        PageEditable = 1 << 2 | 1 << 4,
        UserInteraction = 1 << 4,
    }
}
