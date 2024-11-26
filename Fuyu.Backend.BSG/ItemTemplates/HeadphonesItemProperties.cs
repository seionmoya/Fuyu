using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class HeadphonesItemProperties : EquipmentItemProperties
    {
        [DataMember(Name = "RolloffMultiplier")]
        public float RolloffMultiplier = 1f;

        [DataMember(Name = "GunsCompressorSendLevel")]
        public float GunsCompressorSendLevel = -80f;

        [DataMember(Name = "ClientPlayerCompressorSendLevel")]
        public float ClientPlayerCompressorSendLevel = -80f;

        [DataMember(Name = "ObservedPlayerCompressorSendLevel")]
        public float ObservedPlayerCompressorSendLevel = -80f;

        [DataMember(Name = "NpcCompressorSendLevel")]
        public float NpcCompressorSendLevel = -80f;

        [DataMember(Name = "EnvTechnicalCompressorSendLevel")]
        public float EnvTechnicalCompressorSendLevel = -80f;

        [DataMember(Name = "EnvNatureCompressorSendLevel")]
        public float EnvNatureCompressorSendLevel = -80f;

        [DataMember(Name = "EnvCommonCompressorSendLevel")]
        public float EnvCommonCompressorSendLevel = -80f;

        [DataMember(Name = "AmbientCompressorSendLevel")]
        public float AmbientCompressorSendLevel = -80f;

        [DataMember(Name = "EffectsReturnsCompressorSendLevel")]
        public float EffectsReturnsCompressorSendLevel = -80f;

        [DataMember(Name = "HeadphonesMixerVolume")]
        public float HeadphonesMixerVolume;

        [DataMember(Name = "AmbientVolume")]
        public float AmbientVolume;

        [DataMember(Name = "DryVolume")]
        public float DryVolume;

        [DataMember(Name = "EffectsReturnsGroupVolume")]
        public float EffectsReturnsGroupVolume;

        [DataMember(Name = "Distortion")]
        public float Distortion;

        [DataMember(Name = "CompressorThreshold")]
        public float CompressorThreshold;

        [DataMember(Name = "CompressorAttack")]
        public float CompressorAttack;

        [DataMember(Name = "CompressorRelease")]
        public float CompressorRelease;

        [DataMember(Name = "CompressorGain")]
        public float CompressorGain;

        [DataMember(Name = "HighpassFreq")]
        public int HighpassFreq = 100;

        [DataMember(Name = "HighpassResonance")]
        public float HighpassResonance;

        [DataMember(Name = "LowpassFreq")]
        public int LowpassFreq = 22000;

        [DataMember(Name = "EQBand1Frequency")]
        public float EQBand1Frequency = 200f;

        [DataMember(Name = "EQBand1Gain")]
        public float EQBand1Gain = 1f;

        [DataMember(Name = "EQBand1Q")]
        public float EQBand1Q = 1f;

        [DataMember(Name = "EQBand2Frequency")]
        public float EQBand2Frequency = 1000f;

        [DataMember(Name = "EQBand2Gain")]
        public float EQBand2Gain = 1f;

        [DataMember(Name = "EQBand2Q")]
        public float EQBand2Q = 1f;

        [DataMember(Name = "EQBand3Frequency")]
        public float EQBand3Frequency = 8000f;

        [DataMember(Name = "EQBand3Gain")]
        public float EQBand3Gain = 1f;

        [DataMember(Name = "EQBand3Q")]
        public float EQBand3Q = 1f;
    }
}
