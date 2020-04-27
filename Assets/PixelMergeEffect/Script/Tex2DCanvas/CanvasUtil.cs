using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    public static class CanvasUtil
    {
        private static Vector2Int[] _DirByNeighborIDList;
        private static NeighborID[] _NeighborIDToRightDownCornerNeighborIDList;

        static CanvasUtil()
        {
            _DirByNeighborIDList = new Vector2Int[(int)NeighborID.TOTAL];
            _DirByNeighborIDList[(int)NeighborID.NEIGHBOR_1] = new Vector2Int(-1, -1);
            _DirByNeighborIDList[(int)NeighborID.NEIGHBOR_2] = new Vector2Int(0, -1);
            _DirByNeighborIDList[(int)NeighborID.NEIGHBOR_3] = new Vector2Int(1, -1);
            _DirByNeighborIDList[(int)NeighborID.NEIGHBOR_4] = new Vector2Int(-1, 0);
            _DirByNeighborIDList[(int)NeighborID.NEIGHBOR_5] = new Vector2Int(0, 0);
            _DirByNeighborIDList[(int)NeighborID.NEIGHBOR_6] = new Vector2Int(1, 0);
            _DirByNeighborIDList[(int)NeighborID.NEIGHBOR_7] = new Vector2Int(-1, 1);
            _DirByNeighborIDList[(int)NeighborID.NEIGHBOR_8] = new Vector2Int(0, 1);
            _DirByNeighborIDList[(int)NeighborID.NEIGHBOR_9] = new Vector2Int(1, 1);

            _NeighborIDToRightDownCornerNeighborIDList = new NeighborID[(int)NeighborID.TOTAL];
            _NeighborIDToRightDownCornerNeighborIDList[(int)NeighborID.NEIGHBOR_3] = NeighborID.NEIGHBOR_1;
            _NeighborIDToRightDownCornerNeighborIDList[(int)NeighborID.NEIGHBOR_6] = NeighborID.NEIGHBOR_2;
            _NeighborIDToRightDownCornerNeighborIDList[(int)NeighborID.NEIGHBOR_9] = NeighborID.NEIGHBOR_3;
            _NeighborIDToRightDownCornerNeighborIDList[(int)NeighborID.NEIGHBOR_2] = NeighborID.NEIGHBOR_4;
            _NeighborIDToRightDownCornerNeighborIDList[(int)NeighborID.NEIGHBOR_5] = NeighborID.NEIGHBOR_5;
            _NeighborIDToRightDownCornerNeighborIDList[(int)NeighborID.NEIGHBOR_8] = NeighborID.NEIGHBOR_6;
            _NeighborIDToRightDownCornerNeighborIDList[(int)NeighborID.NEIGHBOR_1] = NeighborID.NEIGHBOR_7;
            _NeighborIDToRightDownCornerNeighborIDList[(int)NeighborID.NEIGHBOR_4] = NeighborID.NEIGHBOR_8;
            _NeighborIDToRightDownCornerNeighborIDList[(int)NeighborID.NEIGHBOR_7] = NeighborID.NEIGHBOR_9;
        }
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

        public static byte OverrideByteWithMask(byte oldData, byte newData, byte mask)
        {
            return (byte)((oldData & (mask ^ 0x00)) | oldData);
        }

        public static Vector2Int GetDirByNeighborID(NeighborID neighborID)
        {
            return _DirByNeighborIDList[(int)neighborID];
        }

        public static NeighborID GetRelativeNeighborID(CornerID cornerID, NeighborID neighborID)
        {
            switch (cornerID)
            {
                case CornerID.RIGHT_UP:
                    return neighborID;
                case CornerID.RIGHT_DOWN:
                    return _NeighborIDToRightDownCornerNeighborIDList[(int)neighborID];
                case CornerID.LEFT_DOWN:
                    return (NeighborID)((int)NeighborID.TOTAL - 1 - (int)neighborID);
                case CornerID.LEFT_UP:
                    return (NeighborID)((int)NeighborID.TOTAL - 1 - (int)_NeighborIDToRightDownCornerNeighborIDList[(int)neighborID]);
                default:
                    break;
            }
            return neighborID;
        }
    }
}
