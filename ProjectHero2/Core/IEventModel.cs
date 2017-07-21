using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    internal interface IEventModel
    {
        String SubscriberName { get; }

        /// <summary>
        /// The event message procedure is called for all objects that need to receive
        /// a specialized message.
        /// </summary>
        /// <param name="e">The type of event that was raised.</param>
        /// <param name="isHandled">
        /// The state object passed for the event. Typically this is in the form of an event arg.
        /// </param>
        void EventMessageProcedure(VSEvents e, object state, out bool callNextProcedure);
    }
}
