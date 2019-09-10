namespace NewsGPS.Mova.Core.Domain.Helpers
{
    public static class ProtocolHelper 
    {
        public static string ToRGPProtocol(this string ruvProtocol)
        {
            var rgpMessage = string.Empty;

            if (ruvProtocol.Contains(">RUV00"))
            {
                var messagesSplited = ruvProtocol.Split(';');
                var idEquipamento = messagesSplited[messagesSplited.Length - 3];
                var numeroMessage = messagesSplited[messagesSplited.Length - 2];

                var pm = messagesSplited[0].Split(',');
                var rpm = pm[8];
                var latlong = pm[3];

                
                var @event = string.Empty;
                var eventSize = pm[0].Length;

                if (eventSize == 8) @event = pm[0].Substring(eventSize - 2, 2);
                else if (eventSize == 9) @event = pm[0].Substring(eventSize - 3, 3);
                else @event = "00";

                var ign = (int.Parse(rpm) == 0) ? "DE" : "DF";
                var rgpMessageOriginal = string.Format(">RGP{0}00{1}{2}00;{3};{4};", latlong, ign, @event, idEquipamento, numeroMessage);
                var checkSum = CheckSumHelper.Calculate(rgpMessageOriginal);
                rgpMessage = string.Format("{0}*{1}<\n\r", rgpMessageOriginal, checkSum);
            }

            return rgpMessage;
        }
    }
}
