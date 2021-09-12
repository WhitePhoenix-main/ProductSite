using System;

namespace ProductsSite
{
    public class Normalaizer
    {
        public string strToNorm;
        
        public Normalaizer(string str)
        {
            strToNorm = str;
        }
        public int GetNormStrRu()
        {
            string buffer = "";
            int normPrice = 0;
            const int symAfter = 2;
            int symAfterCount = 0;
            for (int counter1 = 0; counter1 < strToNorm.Length; counter1++)
            {
                if (strToNorm[counter1] == '.' || strToNorm[counter1] == ',')
                {
                    counter1++;
                    for (;symAfterCount < symAfter; symAfterCount++, counter1++)
                    {
                        buffer += strToNorm[counter1];
                    }
                    break;
                }
                buffer += strToNorm[counter1];
            }
            try
            {
                normPrice = Int32.Parse(buffer);
            }
            catch (FormatException e)
            {
                return -1;
            }
            return normPrice;
        }
    }
}