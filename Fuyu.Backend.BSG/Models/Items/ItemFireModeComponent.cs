using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemFireModeComponent
    {
        [DataMember]
        public EFireMode FireMode;
    }
}