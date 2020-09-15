namespace MrPP.Myth
{
    public interface IBridgeReceiver {
        string handle {
            get;
        }
        void broadcast(string evt, object data);

    }
}