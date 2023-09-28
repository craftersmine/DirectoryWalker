using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryWalker.Core
{
    public static class EventExtensions
    {
        public static object Raise(this MulticastDelegate multicastDelegate, object sender, EventArgs e)
        {
            object returnValue = null;

            MulticastDelegate threadSafeMulticastDelegate = multicastDelegate;
            if (!(threadSafeMulticastDelegate is null))
            {
                foreach (Delegate d in threadSafeMulticastDelegate.GetInvocationList())
                {
                    ISynchronizeInvoke synchronizeInvoke = d.Target as ISynchronizeInvoke;
                    try
                    {
                        if (!(synchronizeInvoke is null) && synchronizeInvoke.InvokeRequired)
                            returnValue =
                                synchronizeInvoke.EndInvoke(synchronizeInvoke.BeginInvoke(d, new object[] { sender, e }));
                        else
                            returnValue = d.DynamicInvoke(new object[] { sender, e });
                    }
                    catch (Exception exception)
                    {
                        if (!(exception is ObjectDisposedException))
                            throw;
                    }
                }
            }

            return returnValue;
        }
    }
}
