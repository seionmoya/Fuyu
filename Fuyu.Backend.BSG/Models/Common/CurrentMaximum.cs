using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Common
{
    [DataContract]
    public class CurrentMaximum<T>
    {
        [DataMember]
        public T Current { get; set; }

		[DataMember]
        public T Maximum { get; set; }
	}
}