using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class MobContainerItemProperties : SearchableItemItemProperties
    {
        [DataMember(Name = "containType")]
        public object[] ContainType;

        [DataMember(Name = "sizeWidth")]
        public int SizeWidth;

        [DataMember(Name = "sizeHeight")]
        public int SizeHeight;

        [DataMember(Name = "isSecured")]
        public bool IsSecured;

        [DataMember(Name = "spawnTypes")]
        public string SpawnTypes;

        [DataMember(Name = "lootFilter")]
        public object[] LootFilter;

        [DataMember(Name = "spawnRarity")]
        public string SpawnRarity;

        [DataMember(Name = "minCountSpawn")]
        public int MinCountSpawn;

        [DataMember(Name = "maxCountSpawn")]
        public int MaxCountSpawn;

        [DataMember(Name = "openedByKeyID")]
        public object[] OpenedByKeyID;
    }
}