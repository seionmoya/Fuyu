using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemLightComponent
    {
        [DataMember]
        public bool IsActive;

        [DataMember]
        public int SelectedMode;
	}
}