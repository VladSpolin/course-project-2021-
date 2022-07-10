using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Server" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
    public class Server : IServer
    {
        List<User> users=new List<User>();
        int nextID = 0;
        public int Connect(string name)
        {
            User user = new User()
            {
                Name = name,
                ID = nextID,
                operationContext = OperationContext.Current
            };
            nextID++;
            SendMsg(user.Name + " подключился к серверу", 0);
            users.Add(user);
            return user.ID;
        }

        public void DoWork()
        {
        }

        public void SendMsg(string msg, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user =users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += " " + user.Name + ": ";
                }
                answer += msg;
                item.operationContext.GetCallbackChannel<IServerCallBack>().MsgCallBack(answer);
            }
        }

        public void SendInfo(string msg, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += " Проведена оплата клиентом " + user.Name + Environment.NewLine;
                }
                answer += msg;
                item.operationContext.GetCallbackChannel<IServerCallBack>().InfoCallBack(answer);
            }
        }
    }
}
