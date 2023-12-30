using System.Text;
using System.Web;

namespace FUploader.Core
{
    public class Notifocator
    {
        private HttpClient _client = new HttpClient();
        private string _botId;
        private string _chatId;
        private string _filial;

        public Notifocator(IConfiguration configuration)
        {
            _botId  = configuration.GetSection("Telegramm").GetValue<string>("botid") ?? "";
            _chatId = configuration.GetSection("Telegramm").GetValue<string>("chatid") ?? "";
            _filial = configuration.GetValue<string>("filialName") ?? "";
        }
    

        private string GetMarkUpMessege(bool resoltness, string message) 
        {
            var builder = new StringBuilder();
            if (resoltness)
            {
                builder.Append($"<b>{_filial} {DateTime.Now.ToString("G")}</b>")
                    .Append("\n")
                    .Append($"<a href=\"{message}\"> upload file</a> ");

            } else {

                builder.Append($"<b>{_filial} generate error {DateTime.Now.ToString("G")}</b>")
                    .Append("\n")
                    .Append($"<code>{message}</code>");
            }

            
            return builder.ToString();
        }

        public async void SendTelegramNotification(bool resoltness, string message) 
        {
            var builder = new UriBuilder($"https://api.telegram.org/bot{_botId}/sendMessage");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["chat_id"] = _chatId;
            query["parse_mode"] = "HTML";
            query["text"] = GetMarkUpMessege(resoltness, message);
            
            builder.Query = query.ToString();
            string url = builder.ToString();
            await _client.GetAsync(url);
        }
    }
}
