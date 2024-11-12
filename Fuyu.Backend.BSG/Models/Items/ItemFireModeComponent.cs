using Fuyu.Backend.BSG.Models.Common;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemFireModeComponent
    {
        [DataMember]
        public EFireMode FireMode;
    }
}