using System;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class LocationInGrid : IEquatable<LocationInGrid>
    {
        [DataMember]
        public int x { get; set; }

        [DataMember]
        public int y { get; set; }

        [DataMember]
        public EItemRotation r { get; set; }

        // emits when 'false'
        [DataMember(EmitDefaultValue = false)]
        public bool? isSearched { get; set; }

        public bool Equals(LocationInGrid other)
        {
            return other.x == x && other.y == y && other.r == r;
        }
    }
}