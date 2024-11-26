using System.Runtime.Serialization;
using Fuyu.Backend.BSG.DTO.Common;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class NightVisionItemProperties : SpecialScopeItemProperties
    {
        [DataMember(Name = "Intensity")]
        public float Intensity;

        [DataMember(Name = "Mask")]
        public EGoggleMask Mask;

        [DataMember(Name = "MaskSize")]
        public float MaskSize;

        [DataMember(Name = "NoiseIntensity")]
        public float NoiseIntensity;

        [DataMember(Name = "NoiseScale")]
        public float NoiseScale;

        [DataMember(Name = "Color")]
        public Color Color;

        [DataMember(Name = "DiffuseIntensity")]
        public float DiffuseIntensity;
    }

    public enum EGoggleMask
    {
        Thermal,
        Anvis,
        Binocular,
        GasMask,
        OldMonocular
    }
}
