using System;

namespace NewsGPS.Mova.Core.Domain.Helpers
{
    public static class CheckSumHelper
    {
        public static string Calculate(string message)
        {
            // Loop through all chars to get a checksum
            int checksum = 0;
            Char? old = null;
            foreach (char character in message)
            {
                if (character == '$')
                {
                    // Ignore the dollar sign
                }
                else if (character == '*' && old == ';')
                {
                    // Stop processing before the asterisk
                    break;
                }
                else
                {
                    // Is this the first value for the checksum?
                    if (checksum == 0)
                    {
                        // Yes. Set the checksum to the value
                        checksum = Convert.ToByte(character);
                    }
                    else
                    {
                        // No. XOR the checksum with this character's value
                        checksum = checksum ^ Convert.ToByte(character);
                    }
                }
                old = character;
            }
            // Return the checksum formatted as a two-character hexadecimal
            return checksum.ToString("X2");
        }
    }
}
