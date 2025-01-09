using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class GetMetricsConfigResponse
{
    [DataMember]
    public int[] Keys { get; set; }

    [DataMember]
    public int[] NetProcessingBins { get; set; }

    [DataMember]
    public int[] RenderBins { get; set; }

    [DataMember]
    public int[] GameUpdateBins { get; set; }

    [DataMember]
    public int MemoryMeasureInterval { get; set; }

    [DataMember]
    public int[] PauseReasons { get; set; }
}