namespace MinhChat.Hepper
{
    public class BasicEmojis
    {
        private static string Img(string src)
        {
            return "<img src='/images/emojis/" + src + "'/>";
        }
        public static string ParseEmojis(string message)
        {
            message = message.Replace(":)", Img("emojis1.png"));
            message = message.Replace(":(", Img("emojis2.png"));
            message = message.Replace(":D", Img("emojis3.png"));
            message = message.Replace(":P", Img("emojis4.png"));
            message = message.Replace(":O", Img("emojis5.png"));
            message = message.Replace(":|", Img("emojis6.png"));
            message = message.Replace(":*", Img("emojis7.png"));
            return message;
        }
    }
}
