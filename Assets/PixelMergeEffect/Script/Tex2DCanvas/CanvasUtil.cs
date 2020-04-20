using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    public class CanvasUtil
    {
        public static int GetTextureFormatByteCount(TextureFormat format)
        {
            switch (format)
            {
                case TextureFormat.Alpha8:
                    return 1;
                case TextureFormat.ARGB4444:
                    return 2;
                case TextureFormat.RGB24:
                    return 3;
                case TextureFormat.RGBA32:
                    return 4;
                case TextureFormat.ARGB32:
                    return 4;
                case TextureFormat.RGB565:
                    return 2;
                case TextureFormat.R16:
                    return 2;
                case TextureFormat.RGBA4444:
                    return 2;
                case TextureFormat.BGRA32:
                    return 4;
                case TextureFormat.RHalf:
                    return 2;
                case TextureFormat.RGHalf:
                    return 4;
                case TextureFormat.RGBAHalf:
                    return 8;
                case TextureFormat.RFloat:
                    return 4;
                case TextureFormat.RGFloat:
                    return 8;
                case TextureFormat.RGBAFloat:
                    return 16;
                case TextureFormat.RG16:
                    return 2;
                case TextureFormat.R8:
                    return 1;
                default:
                    return 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaID"></param>
        /// <param name="flag">0 or 1</param>
        /// <returns></returns>
        public static byte SetBitAtArea(byte data, AreaID areaID, byte bit)
        {
            return SetBitAtPosition(data, (byte)areaID, bit);
        }

        public static byte SetBitAtPosition(byte data, byte position, byte bit)
        {
            byte mask = (byte)(1 << position);
            if (bit == 0)
            {
                mask ^= 0xFF;
                return data &= mask;
            }
            return data |= mask;
        }
    }
}
