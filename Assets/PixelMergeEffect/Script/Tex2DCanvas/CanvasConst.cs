using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    // dir code
    // 7 8 9
    // 4 · 6
    // 1 2 3
    // 
    // area code
    // 6       0
    //   7   1
    //     8
    //   5   3
    // 4       2
    public enum AreaID
    {
        DIR_5 = 0b1000, // 8
        DIR_9_BIG = 0b0001, // 1
        DIR_3_BIG = 0b0011, // 3
        DIR_1_BIG = 0b0101, // 5
        DIR_7_BIG = 0b0111, // 7
        DIR_9_SMALL = 0b0000, // 0
        DIR_3_SMALL = 0b0010, // 2
        DIR_1_SMALL = 0b0100, // 4
        DIR_7_SMALL = 0b0110, // 6
    };

    public enum CornerStyleID
    {
        STYLE_NONE = 0,
        STYLE_SMALL = 1,
        SYTLE_BIG = 2,
        SYTLE_LONG_LEFT = 3,
        SYTLE_LONG_RIGHT = 4,
        SYTLE_ROUND = 5,
    }
}
