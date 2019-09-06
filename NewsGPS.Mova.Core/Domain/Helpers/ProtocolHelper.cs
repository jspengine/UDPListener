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

                var rpm = ruvProtocol.Substring(73, 3);
                var ign = (int.Parse(rpm) == 0) ? "DE" : "DF";

                var rgpMessageOriginal = string.Format(">RGP{0}00{1}0000;{2};{3};", ruvProtocol.Substring(25, 36), ign, idEquipamento, numeroMessage);

                //string singPct = ">RGP" + ruvProtocol.Substring(25, 36) + "00" + ign + "00" + "00" + ";" + idEquipamento + ";" + numeroMessage + ";";

                var checkSum = CheckSumHelper.Calculate(rgpMessageOriginal);
                rgpMessage = string.Format("{0}*{1}<\n\r", rgpMessageOriginal, checkSum);
            }

            return rgpMessage;
        }
    }
}
