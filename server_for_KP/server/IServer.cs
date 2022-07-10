using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace server
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IServer" в коде и файле конфигурации.
    [ServiceContract(CallbackContract =typeof(IServerCallBack))]
    public interface IServer
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract(IsOneWay =true)]
        void SendMsg(string smg, int id);
        [OperationContract(IsOneWay = true)]
        void SendInfo(string smg, int id);
    }
    public interface IServerCallBack
    {
        [OperationContract(IsOneWay =true)]
        void MsgCallBack(string msg);
        [OperationContract(IsOneWay = true)]
        void InfoCallBack(string msg);
    }
}
