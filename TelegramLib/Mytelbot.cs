using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramLib;

public class Mytelbot
{
    #region Props

    #region PublicProps

    public const string Token = "2031108528:AAE9qpNIRCbpF23IufRJTO-0D2h7IyIf0gg";
    public TelegramUser? Mytelbol = new();

    #endregion

    #region _privateProps

    private TelegramBotClient? _client;

    #endregion

    #endregion

    public Mytelbot()
    {
        Init().Wait(); 
    }

    #region Methods 

    #region PublicMethods

    public async Task<TelegramUser> Init()
    {
        _client = new TelegramBotClient(Token);
        var temp = await _client.GetMeAsync();
        Mytelbol.Id = temp.Id;
        Mytelbol.FirstName = temp.FirstName;

        return Mytelbol;
    }

    #endregion 

    #endregion 
}
