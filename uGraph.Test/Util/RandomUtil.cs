using System;
using System.Collections.Generic;

namespace uGraph.Test.Util
{
    public static class RandomUtil
    {
        private static Random rng = new Random((int)DateTime.Now.Ticks);

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<Tuple<int, int, int>> RandomTrio(int minValue, int maxValue, int count)
        {
            List<Tuple<int, int, int>> randomTrio = new List<Tuple<int, int, int>>();
            for (int i = 0; i < count; i++)
            {
                randomTrio.Add(new Tuple<int, int, int>(rng.Next(minValue, maxValue), rng.Next(minValue, maxValue), rng.Next(minValue, maxValue)));
            }

            return randomTrio;
        }

    }
}
