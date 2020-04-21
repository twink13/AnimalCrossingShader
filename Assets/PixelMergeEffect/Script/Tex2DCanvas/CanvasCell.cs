using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    public class CanvasCell
    {
        public Tex2DCanvas owner;
        public int x;
        public int y;
        public byte data0 = 0x00;
        public byte data1 = 0x00;
        public byte data2 = 0x00;
        public byte data3 = 0x00;

        public bool dirty;

        //============================================================================================================
        // public function
        //============================================================================================================

        public void Test()
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
    }
}
