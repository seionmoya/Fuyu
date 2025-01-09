using System;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Profiles.Health;

namespace Fuyu.Backend.EFT.Services;
public class HealthService
{
    public static HealthService Instance => instance.Value;
    private static readonly Lazy<HealthService> instance = new(() => new HealthService());

    private HealthService()
    {

    }

    public BodyPart GetBodyPart(HealthInfo health, EBodyPart bodyPart)
    {
        return health.GetBodyPart(bodyPart);
    }
}
