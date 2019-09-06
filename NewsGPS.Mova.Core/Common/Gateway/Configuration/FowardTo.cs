namespace NewsGPS.Mova.Core.Common.Gateway.Configuration
{
    public class FowardTo
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
    }

    public class FowardToMova : FowardTo
    {
    }

    public class FowardToSing : FowardTo
    {
    }

}
