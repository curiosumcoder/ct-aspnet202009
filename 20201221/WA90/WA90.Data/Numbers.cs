using System;
using System.Collections.Generic;

namespace WA90.Data
{
    public class Numbers : IGetList<int>
    {
        public IEnumerable<int> GetList()
        {
            //throw new NotImplementedException();

            //return new int[] { };

            for (int i = 0; i < 15; i++)
            {
                yield return new Random().Next(1, 1000);
            }
        }

        public bool IsOdd(int valor)
        {
            return valor % 2 == 1;
        }
    }
}
