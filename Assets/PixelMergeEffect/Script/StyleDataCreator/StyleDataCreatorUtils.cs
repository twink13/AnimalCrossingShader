using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;

namespace Twink.AnimalCrossing
{
    public static class StyleDataCreatorUtils
    {
        public static MainStyleData[] CreateMainStyleData()
        {
            MainStyleData[] cacheData = new MainStyleData[256];
            
            for (int i = 0; i < 256; i++)
            {
                
            }
            
            return cacheData;
        }

        public static string CreateCachedMainStyleList()
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<byte, bool> _cachedData = new Dictionary<byte, bool>();

            for (uint i = 0; i < 256; i++)
            {
                byte data = (byte)i;

                // check
                if (_cachedData.ContainsKey(data))
                {
                    continue;
                }

                // add
                sb.AppendLine(Convert.ToString(data, 2).PadLeft(8, '0'));

                // cache
                for (uint j = 0; j < 4; j++)
                {
                    byte rotatedData = StaticDataConfig.Rotate(data, j);
                    if (!_cachedData.ContainsKey(rotatedData))
                    {
                        _cachedData.Add(rotatedData, true);
                    }
                }
                byte mirroredData = StaticDataConfig.Mirror(data);
                for (uint j = 0; j < 4; j++)
                {
                    byte rotatedData = StaticDataConfig.Rotate(mirroredData, j);
                    if (!_cachedData.ContainsKey(rotatedData))
                    {
                        _cachedData.Add(rotatedData, true);
                    }
                }
            }

            return sb.ToString();
        }
    }

}