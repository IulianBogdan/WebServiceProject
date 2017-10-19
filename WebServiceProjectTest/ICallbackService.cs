using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceProjectTest
{
    interface ICallbackService
    {
        // the contract that needs to be defined on the client side. This is how the server comunicates with the client
        [OperationContract(IsOneWay = true)]
        void CallbackPublishResult(List<Log> list);
    }
}
