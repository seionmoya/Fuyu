using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Templates;

public interface IProfileBuild
{
    public MongoId Id { get; set; }
    public string Name { get; set; }
}
