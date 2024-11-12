using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemKeyComponent
    {
        [DataMember]
        public int NumberOfUsages;
    }
}