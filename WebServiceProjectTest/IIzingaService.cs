using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebServiceProjectTest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode =SessionMode.Required,CallbackContract =typeof(ICallbackService))]
    public interface IIzingaService
    {
        [OperationContract(IsOneWay =false)]
        void Subscribe();
        [OperationContract(IsOneWay = false)]
        void Unsubscribe();
        [OperationContract(IsOneWay = false)]
        void PublishResult();
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WebServiceProject.ContractType".
    [DataContract]
    public class Log
    {
        DateTime tid;
        int id;
        string alarm;
        string navn;
        string afdeling;
        string bolig;
        DateTime afmeldt;

        [DataMember]
        public DateTime TidValue
        {
            get { return tid; }
            set { tid = value; }
        }

        [DataMember]
        public int IDValue
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string AlarmValue
        {
            get { return alarm; }
            set { alarm = value; }
        }

        [DataMember]
        public string NavnValue
        {
            get { return navn; }
            set { navn = value; }
        }

        [DataMember]
        public string AfdelingValue
        {
            get { return afdeling; }
            set { afdeling = value; }
        }

        [DataMember]
        public string BoligValue
        {
            get { return bolig; }
            set { bolig = value; }
        }

        [DataMember]
        public DateTime AfmeldtValue
        {
            get { return afmeldt; }
            set { afmeldt = value; }
        }
    }
}
