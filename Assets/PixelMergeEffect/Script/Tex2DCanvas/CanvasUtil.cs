using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

namespace Twink.AnimalCrossing
{
    public static class CanvasUtil
    {
        private static Vector2Int[] _DirByNeighborIDList;
        private static uint[] _NeighborIDToRightDownCornerNeighborIDList;

        static CanvasUtil()
        {
            _DirByNeighborIDList = new Vector2Int[NeighborID.TOTAL];
            _DirByNeighborIDList[NeighborID.NEIGHBOR_1] = new Vector2Int(-1, -1);
            _DirByNeighborIDList[NeighborID.NEIGHBOR_2] = new Vector2Int(0, -1);
            _DirByNeighborIDList[NeighborID.NEIGHBOR_3] = new Vector2Int(1, -1);
            _DirByNeighborIDList[NeighborID.NEIGHBOR_4] = new Vector2Int(-1, 0);
            _DirByNeighborIDList[NeighborID.NEIGHBOR_5] = new Vector2Int(0, 0);
            _DirByNeighborIDList[NeighborID.NEIGHBOR_6] = new Vector2Int(1, 0);
            _DirByNeighborIDList[NeighborID.NEIGHBOR_7] = new Vector2Int(-1, 1);
            _DirByNeighborIDList[NeighborID.NEIGHBOR_8] = new Vector2Int(0, 1);
            _DirByNeighborIDList[NeighborID.NEIGHBOR_9] = new Vector2Int(1, 1);

            _NeighborIDToRightDownCornerNeighborIDList = new uint[NeighborID.TOTAL];
            _NeighborIDToRightDownCornerNeighborIDList[NeighborID.NEIGHBOR_3] = NeighborID.NEIGHBOR_1;
            _NeighborIDToRightDownCornerNeighborIDList[NeighborID.NEIGHBOR_6] = NeighborID.NEIGHBOR_2;
            _NeighborIDToRightDownCornerNeighborIDList[NeighborID.NEIGHBOR_9] = NeighborID.NEIGHBOR_3;
            _NeighborIDToRightDownCornerNeighborIDList[NeighborID.NEIGHBOR_2] = NeighborID.NEIGHBOR_4;
            _NeighborIDToRightDownCornerNeighborIDList[NeighborID.NEIGHBOR_5] = NeighborID.NEIGHBOR_5;
            _NeighborIDToRightDownCornerNeighborIDList[NeighborID.NEIGHBOR_8] = NeighborID.NEIGHBOR_6;
            _NeighborIDToRightDownCornerNeighborIDList[NeighborID.NEIGHBOR_1] = NeighborID.NEIGHBOR_7;
            _NeighborIDToRightDownCornerNeighborIDList[NeighborID.NEIGHBOR_4] = NeighborID.NEIGHBOR_8;
            _NeighborIDToRightDownCornerNeighborIDList[NeighborID.NEIGHBOR_7] = NeighborID.NEIGHBOR_9;
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
        public static byte SetBitAtArea(byte data, uint areaID, byte bit)
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
            //Debug.Log("oldData = " + oldData + ", newData = " + newData + ", mask = " + mask);
            return (byte)((oldData & (mask ^ 0xFF)) | newData);
        }

        // rightShift: [0, 7]
        public static byte ShiftRepeat(byte data, int rightShift)
        {
            byte mask = (byte)(int)(Mathf.Pow(2, rightShift) - 1);
            byte rightSection = (byte)(data & mask);
            byte result = (byte)((data >> rightShift) | (data << (8 - rightShift)));
            return result;
        }

        // position: [0, 7]
        public static byte GetBitAtRightPosition(byte data, int position)
        {
            return (byte)((data >> position) & 1);
        }

        // position: [0, 7]
        public static byte SetBitAtRightPosition(byte data, int position, byte bit)
        {
            byte mask = (byte)(int)(Mathf.Pow(2, position));
            byte newData = (byte)(bit << position);
            return OverrideByteWithMask(data, newData, mask);
        }

        public static string ByteToString(byte data)
        {
            return Convert.ToString(data, 2);
        }

        public static Vector2Int GetDirByNeighborID(uint neighborID)
        {
            return _DirByNeighborIDList[neighborID];
        }

        public static uint GetRelativeNeighborID(uint cornerID, uint neighborID)
        {
            switch (cornerID)
            {
                case CornerID.RIGHT_UP:
                    return neighborID;
                case CornerID.RIGHT_DOWN:
                    return _NeighborIDToRightDownCornerNeighborIDList[neighborID];
                case CornerID.LEFT_DOWN:
                    return (NeighborID.TOTAL - 1 - neighborID);
                case CornerID.LEFT_UP:
                    return _NeighborIDToRightDownCornerNeighborIDList[(NeighborID.TOTAL - 1 - neighborID)];
                default:
                    break;
            }
            return neighborID;
        }
    }
}
