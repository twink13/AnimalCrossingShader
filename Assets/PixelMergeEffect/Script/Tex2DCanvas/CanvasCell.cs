using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    public class CanvasCell
    {
        private static CanvasCell _universal;

        public Tex2DCanvas owner;
        public int x;
        public int y;
        public byte data0 = 0x00;
        public byte data1 = 0x00;
        public byte data2 = 0x00;
        public byte data3 = 0x00;

        public bool dirty;

        //============================================================================================================
        // getter setter
        //============================================================================================================

        public uint color0
        {
            get { return (uint)(data0 >> 4); }
            set { data0 = CanvasUtil.OverrideByteWithMask(data0, (byte)(value << 4), 0b_1111_0000); }
        }

        public uint color1
        {
            get { return (uint)(data0 & 0b_0000_1111); }
            set { data0 = CanvasUtil.OverrideByteWithMask(data0, (byte)(value), 0b_0000_1111); }
        }

        public uint color2
        {
            get { return (uint)(data1 >> 4); }
            set { data1 = CanvasUtil.OverrideByteWithMask(data1, (byte)(value << 4), 0b_1111_0000); }
        }

        public uint color3
        {
            get { return (uint)(data1 & 0b_0000_1111); }
            set { data1 = CanvasUtil.OverrideByteWithMask(data1, (byte)(value), 0b_0000_1111); }
        }

        public uint mainColor
        {
            get { return (uint)(data2 >> 4); }
            set { data2 = CanvasUtil.OverrideByteWithMask(data2, (byte)(value << 4), 0b_1111_0000); }
        }

        public uint style0
        {
            get { return (uint)((data2 & 0b_0000_1110) >> 1); }
            set { data2 = CanvasUtil.OverrideByteWithMask(data2, (byte)(value << 1), 0b_0000_1110); }
        }

        public uint style1
        {
            get { return (uint)(((data2 & 0b_0000_0001) << 2) | (data3 >> 6)); }
            set
            {
                data2 = CanvasUtil.OverrideByteWithMask(data2, (byte)(value >> 2), 0b_0000_0001);
                data3 = CanvasUtil.OverrideByteWithMask(data3, (byte)(value << 6), 0b_1100_0000);
            }
        }

        public uint style2
        {
            get { return (uint)((data3 & 0b_0011_1000) >> 3); }
            set { data3 = CanvasUtil.OverrideByteWithMask(data3, (byte)(value << 3), 0b_0011_1000); }
        }

        public uint style3
        {
            get { return (uint)(data3 & 0b_0000_0111); }
            set { data3 = CanvasUtil.OverrideByteWithMask(data3, (byte)(value), 0b_0000_0111); }
        }

        public Vector2Int position
        {
            get { return new Vector2Int(x, y); }
        }

        //============================================================================================================
        // public function
        //============================================================================================================

        public void Resolve()
        {
            
        }
        public void UpdateData()
        {
            owner.SetData(x, y, data0, 0);
            owner.SetData(x, y, data1, 1);
            owner.SetData(x, y, data2, 2);
            owner.SetData(x, y, data3, 3);
            dirty = false;
        }

        //============================================================================================================
        // private function
        //============================================================================================================

        //============================================================================================================
        // static factory
        //============================================================================================================

        public static CanvasCell Create(Tex2DCanvas owner, Vector2Int pos)
        {
            return Create(owner, pos.x, pos.y);
        }
        public static CanvasCell Create(Tex2DCanvas owner, int x, int y)
        {
            CanvasCell cell = new CanvasCell();
            cell.owner = owner;
            cell.x = x;
            cell.y = y;
            cell.dirty = true;
            return cell;
        }

        public static CanvasCell Universal
        {
            get
            {
                if (_universal == null)
                {
                    _universal = Create(null, -1, -1);
                }
                return _universal;
            }
        }
    }
}
