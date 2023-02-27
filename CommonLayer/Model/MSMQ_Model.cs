using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using Experimental.System.Messaging;

namespace CommonLayer.Model
{
    public class MSMQ_Model
    {
        MessageQueue mq = new MessageQueue();
        public void sendData2Queue(string token)
        {
            mq.Path = @".\private$\token";
            if (!MessageQueue.Exists(mq.Path))
            {
                MessageQueue.Create(mq.Path);
            }
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            mq.ReceiveCompleted += Mq_ReceiveCompleted;
            mq.Send(token);
            mq.BeginReceive();
            mq.Close();

        }

        private void Mq_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = mq.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            string subject = "Receive Completed";
            string body = token;
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("akshata00rn@gmail.com", "mgyjfnsxtmwgvwnt"),
                EnableSsl = true,
            };
            smtp.Send("akshata00rn@gmail.com", "akshata00rn@gmail.com", subject, body);
            mq.BeginReceive();
        }
    }
}
