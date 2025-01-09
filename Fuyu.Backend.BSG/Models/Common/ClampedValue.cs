using System.Numerics;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Common
{
    /// <summary>
    /// Class used to automatically clamp <see cref="Minimum"/> and <see cref="Maximum"/> values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class ClampedValue<T> where T : INumber<T>
    {
        [DataMember]
        public T Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = T.Clamp(value, Minimum, Maximum);
            }
        }

        private T _current;

        [DataMember]
        public T Minimum { get; set; }

        [DataMember]
        public T Maximum { get; set; }

        [DataMember]
        public T OverDamageReceivedMultiplier { get; set; }

        [DataMember]
        public T EnvironmentDamageMultiplier { get; set; }
    }
}