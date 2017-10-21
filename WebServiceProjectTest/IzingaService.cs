using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace WebServiceProjectTest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class IzingaService : IIzingaService
    {
        Parser Parser = new Parser();
        string filePath = @"C:\Users\Admin\Desktop\logs.txt";
        ICallbackService ServiceCallBack = null;
        private static List<ICallbackService> Subscribers = null; // list of subscribers
        public IzingaService()
        {
            Subscribers = new List<ICallbackService>();
            Thread Publisher = new Thread(PublishResult);
            Publisher.Start();
        }
        public void PublishResult() // list passed as parameter is the list returned by the parsing method
        {
            while (true)
            {
                List<Log> result = new List<Log>(Parser.RunParser(filePath));
                foreach (ICallbackService ICS in Subscribers)//foreach connection
                {
                    try
                    {
                        lock (result)//lock the list in case of new one arrives
                        {
                            if ((((IChannel)ICS).State == CommunicationState.Opened))

                                ICS.CallbackPublishResult(result);// pass the list to the callback contract
                            else
                                Subscribers.Remove(ICS);//remove the instance in case of losing connection
                        }
                    }

                    catch (Exception)
                    {
                    }
                }
                Thread.Sleep(5000);
            }

        }

        public void Subscribe()
        {
            ServiceCallBack = OperationContext.Current.GetCallbackChannel<ICallbackService>();
            Subscribers.Add(ServiceCallBack);
        }

        public void Unsubscribe()
        {
            ServiceCallBack = OperationContext.Current.GetCallbackChannel<ICallbackService>();
            lock (Subscribers)
            {
                Subscribers.Remove(ServiceCallBack);
            }
        }
    }
}
