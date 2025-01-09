using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class LauncherItemProperties : GearModItemProperties
    {
        [DataMember(Name = "UseAmmoWithoutShell")]
        public bool UseAmmoWithoutShell;

        [DataMember(Name = "LinkedWeapon")]
        public string LinkedWeapon;

        [DataMember(Name = "Chambers")]
        public object[] Chambers = [];
    }
}