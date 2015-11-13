using System;
using Hk.Infrastructures.Plugins;

namespace Hk.Infrastructures.Core.ServicePlugins
{
    public interface IServiceStepPlugIn : IPlugIn
    {
        void Process(PropertyBag propertyBag);
    }
}
