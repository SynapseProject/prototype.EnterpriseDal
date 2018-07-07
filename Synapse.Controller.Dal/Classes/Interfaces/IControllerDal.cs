using System;
using System.Collections.Generic;


namespace Synapse.Services.Controller.Dal
{
    public interface IControllerDal : IPlanSecurityProvider, IPlanExecuteReader, IPlanHistoryWriter
    {
        object GetDefaultConfig();
        Dictionary<string, string> Configure(ISynapseDalConfig conifg);
    }
}