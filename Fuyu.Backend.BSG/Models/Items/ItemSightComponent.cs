using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items;

[DataContract]
public class ItemSightComponent
{
    [DataMember]
    public int[] ScopesCurrentCalibPointIndexes { get; set; }

    [DataMember]
    public int[] ScopesSelectedModes { get; set; }

    [DataMember]
    public float ScopeZoomValue { get; set; }

    [DataMember]
    public int SelectedScope { get; set; }
}