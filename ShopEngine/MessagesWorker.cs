using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopEngine
{
    public class MessagesWorker
    {
        public List<MessageModel> GetMessages(int userID)
        {
            List<MessageModel> answer = new List<MessageModel>();
            DBAccess.DB_Telemetry_ShopAppsEntities db = new DBAccess.DB_Telemetry_ShopAppsEntities();
            IQueryable<DBAccess.Messages> msgs = from d in db.Messages where d.id_User == userID select d;
            foreach (var item in msgs)
            {
                answer.Add(
                    new MessageModel
                    {
                        id = item.id,
                        id_User = item.id_User,
                        Text = item.Text,
                        Name = item.Name,
                        Date = item.Date
                    });
            }
            return answer;
        }
    }

    public class MessageModel
    {
        public int id { get; set; }

        public int id_User { get; set; }

        public string Text { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

    }
}
