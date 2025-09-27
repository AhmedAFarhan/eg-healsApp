using BuildingBlocks.DataAccess.PlatformTargets;

namespace EGHeals.Web.PlatformTargets
{
    public class PlatformTarget : IPlatformTarget
    {
        public PlatformType Platform => PlatformType.WEB;
    }
}
