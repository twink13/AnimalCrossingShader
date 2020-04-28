using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    // area code
    // 6       0
    //   7   1
    //     8
    //   5   3
    // 4       2
    public class AreaID
    {
        public const uint DIR_5 = 0b1000; // 8
        public const uint DIR_9_BIG = 0b0001; // 1
        public const uint DIR_3_BIG = 0b0011; // 3
        public const uint DIR_1_BIG = 0b0101; // 5
        public const uint DIR_7_BIG = 0b0111; // 7
        public const uint DIR_9_SMALL = 0b0000; // 0
        public const uint DIR_3_SMALL = 0b0010; // 2
        public const uint DIR_1_SMALL = 0b0100; // 4
        public const uint DIR_7_SMALL = 0b0110; // 6
    };

    public class CornerStyleID
    {
        public const uint STYLE_NONE = 0;
        public const uint STYLE_SMALL = 1;
        public const uint STYLE_BIG = 2;
        public const uint STYLE_LONG_LEFT = 3;
        public const uint STYLE_LONG_RIGHT = 4;
        public const uint STYLE_ROUND = 5;
    }

    // neighbor id
    // 7 8 9
    // 4 5 6
    // 1 2 3
    public class NeighborID
    {
        public const uint NEIGHBOR_1 = 0;
        public const uint NEIGHBOR_2 = 1;
        public const uint NEIGHBOR_3 = 2;
        public const uint NEIGHBOR_4 = 3;
        public const uint NEIGHBOR_5 = 4;
        public const uint NEIGHBOR_6 = 5;
        public const uint NEIGHBOR_7 = 6;
        public const uint NEIGHBOR_8 = 7;
        public const uint NEIGHBOR_9 = 8;

        public const uint TOTAL = 9;
    }

    // 3 0
    // 2 1
    public class CornerID
    {
        public const uint RIGHT_UP = 0;
        public const uint RIGHT_DOWN = 1;
        public const uint LEFT_DOWN = 2;
        public const uint LEFT_UP = 3;
    }
}
