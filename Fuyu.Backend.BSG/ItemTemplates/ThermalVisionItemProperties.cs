using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class ThermalVisionItemProperties : SpecialScopeItemProperties
    {
        [DataMember(Name = "RampPalette")]
        public EThermalPalette RampPalette;

        [DataMember(Name = "DepthFade")]
        public float DepthFade;

        [DataMember(Name = "MainTexColorCoef")]
        public float MainTexColorCoef;

        [DataMember(Name = "MinimumTemperatureValue")]
        public float MinimumTemperatureValue;

        [DataMember(Name = "RampShift")]
        public float RampShift;

        [DataMember(Name = "HeatMin")]
        public float HeatMin;

        [DataMember(Name = "ColdMax")]
        public float ColdMax;

        [DataMember(Name = "IsNoisy")]
        public bool IsNoisy;

        [DataMember(Name = "NoiseIntensity")]
        public float NoiseIntensity;

        [DataMember(Name = "IsFpsStuck")]
        public bool IsFpsStuck;

        [DataMember(Name = "IsGlitch")]
        public bool IsGlitch;

        [DataMember(Name = "IsMotionBlurred")]
        public bool IsMotionBlurred;

        [DataMember(Name = "Mask")]
        public EGoggleMask Mask;

        [DataMember(Name = "MaskSize")]
        public float MaskSize;

        [DataMember(Name = "IsPixelated")]
        public bool IsPixelated;

        [DataMember(Name = "PixelationBlockCount")]
        public int PixelationBlockCount;
    }

    public enum EThermalPalette
    {
        Fusion,
        Rainbow,
        WhiteHot,
        BlackHot
    }
}
