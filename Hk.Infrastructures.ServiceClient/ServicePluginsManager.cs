using System;
using Hk.Infrastructures.Plugins;
using Hk.Infrastructures.Core.ServicePlugins;
namespace Hk.Infrastructures.ServiceClient
{
    [PlugInApplication("ServicePlugins")]
    internal class ServicePluginsManager : PlugInBasedApplication<IServiceStepPlugIn>, IServiceApplicationPlugIn
    {

    }
}
