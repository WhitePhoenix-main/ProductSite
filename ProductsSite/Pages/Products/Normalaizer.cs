using System;

namespace ProductsSite
{
    public interface INormalizer
    {
        int GetNormStrRu(string strToNorm);
    }
    public class Normalizer : INormalizer
    {
        // TODO: Выполнить проверку на числах с одним знаком после запятой/Переделать
         public int GetNormStrRu(string strToNorm)
        {
            string buffer = "";
            int normPrice = 0;
            const int symAfter = 2;
            int symAfterCount = 0;
            Boolean pointCheck = false;
            for (int counter1 = 0; counter1 < strToNorm.Length; counter1++)
            {
                if (strToNorm[counter1] == '.' || strToNorm[counter1] == ',')
                {
                    counter1++;
                    for (;symAfterCount < symAfter; symAfterCount++, counter1++)
                    {
                        buffer += strToNorm[counter1];
                    }

                    pointCheck = true;
                    break;
                }
                buffer += strToNorm[counter1];
            }

            if (!pointCheck)
            {
                buffer += "00";
            }
            try
            {
                normPrice = Int32.Parse(buffer);
            }
            catch (FormatException)
            {
                return -1;
            }
            return normPrice;
        }
    }
}