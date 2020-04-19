using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    public struct CanvasCell
    {
        public Tex2DCanvas owner;
        public int x;
        public int y;

        public bool dirty;

        //============================================================================================================
        // public function
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
            cell.dirty = false;
            return cell;
        }
    }
}
