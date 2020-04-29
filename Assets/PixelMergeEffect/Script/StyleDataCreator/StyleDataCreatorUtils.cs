using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;

namespace Twink.AnimalCrossing
{
    public static class StyleDataCreatorUtils
    {
        public static StyleConfigData[] CreateStyleConfigData()
        {
            StyleConfigData[] cachedData = new StyleConfigData[256];

            cachedData[0b_0000_0000] = new StyleConfigData(1, 1, 1, 1);
            cachedData[0b_0000_0001] = new StyleConfigData(0, 1, 1, 0);
            cachedData[0b_0000_0010] = new StyleConfigData(2, 1, 2, 0);
            cachedData[0b_0000_0011] = new StyleConfigData(0, 1, 2, 0);
            cachedData[0b_0000_0101] = new StyleConfigData(0, 1, 0, 0);
            cachedData[0b_0000_0111] = new StyleConfigData(0, 1, 0, 0);
            cachedData[0b_0000_1001] = new StyleConfigData(0, 3, 0, 0);
            cachedData[0b_0000_1010] = new StyleConfigData(2, 2, 0, 0);
            cachedData[0b_0000_1011] = new StyleConfigData(0, 3, 0, 0);
            cachedData[0b_0000_1101] = new StyleConfigData(0, 3, 0, 0);
            cachedData[0b_0000_1110] = new StyleConfigData(2, 2, 0, 0);
            cachedData[0b_0000_1111] = new StyleConfigData(0, 3, 0, 0);
            cachedData[0b_0001_0001] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0001_0011] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0001_0101] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0001_0111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0001_1011] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0001_1111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0010_0010] = new StyleConfigData(2, 0, 2, 0);
            cachedData[0b_0010_0011] = new StyleConfigData(0, 0, 2, 0);
            cachedData[0b_0010_0101] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0010_0111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0010_1001] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0010_1010] = new StyleConfigData(2, 0, 0, 0);
            cachedData[0b_0010_1011] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0010_1101] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0010_1110] = new StyleConfigData(2, 0, 0, 0);
            cachedData[0b_0010_1111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0011_0011] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0011_0101] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0011_0110] = new StyleConfigData(2, 0, 0, 0);
            cachedData[0b_0011_0111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0011_1001] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0011_1011] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0011_1101] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0011_1110] = new StyleConfigData(2, 0, 0, 0);
            cachedData[0b_0011_1111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0101_0101] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0101_0111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0101_1011] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0101_1111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0110_1011] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0110_1111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0111_0111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_0111_1111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_1010_1010] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_1010_1011] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_1010_1111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_1011_1011] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_1011_1111] = new StyleConfigData(0, 0, 0, 0);
            cachedData[0b_1111_1111] = new StyleConfigData(0, 0, 0, 0);
            
            for (int i = 0; i < 256; i++)
            {
                byte data = (byte)i;

                StyleConfigData currentConfigData = cachedData[data];

                // check
                if (currentConfigData != null)
                {
                    for (uint j = 0; j < 4; j++)
                    {
                        byte rotatedData = StaticDataConfig.Rotate(data, j);
                        if (cachedData[rotatedData] == null)
                        {
                            cachedData[rotatedData] = currentConfigData.CreateRotated(j);
                        }
                    }

                    byte mirroredData = StaticDataConfig.Mirror(data);
                    for (uint j = 0; j < 4; j++)
                    {
                        byte rotatedData = StaticDataConfig.Rotate(mirroredData, j);
                        if (cachedData[rotatedData] == null)
                        {
                            cachedData[rotatedData] = currentConfigData.CreateMirrored();
                        }
                    }
                }

            }

            // debug log
            // StringBuilder sb = new StringBuilder();
            // for (uint i = 0; i < 256; i++)
            // {
            //     sb.Append(Convert.ToString(i, 2).PadLeft(8, '0') + " -> ");
                
            //     StyleConfigData currentConfigData = cachedData[i];
            //     if (currentConfigData != null)
            //     {
            //         sb.AppendLine(currentConfigData.ToString());
            //     }
            //     else
            //     {
            //         sb.AppendLine("None");
            //     }
            // }
            // Debug.Log("CreateStyleConfigData output = \n" + sb.ToString());
            
            return cachedData;
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