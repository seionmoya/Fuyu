using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class Note
    {
        [DataMember(Name = "Time")]
        public float Created { get; set; }

        [DataMember(Name = "Text")]
        public string Text { get; set; }
    }
}