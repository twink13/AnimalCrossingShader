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

    // neighbor id
    // 7 8 9
    // 4 5 6
    // 1 2 3
    public enum NeighborID
    {
        NEIGHBOR_1 = 0,
        NEIGHBOR_2 = 1,
        NEIGHBOR_3 = 2,
        NEIGHBOR_4 = 3,
        NEIGHBOR_5 = 4,
        NEIGHBOR_6 = 5,
        NEIGHBOR_7 = 6,
        NEIGHBOR_8 = 7,
        NEIGHBOR_9 = 8,

        TOTAL = 9,
    }

    // relative neighbor id (right down corner)
    // 1 4 7
    // 2 5 8
    // 3 6 9
    public enum RelativeCornerNeighborID
    {
        NEIGHBOR_1 = NeighborID.NEIGHBOR_3,
        NEIGHBOR_2 = NeighborID.NEIGHBOR_6,
        NEIGHBOR_3 = NeighborID.NEIGHBOR_9,
        NEIGHBOR_4 = NeighborID.NEIGHBOR_2,
        NEIGHBOR_5 = NeighborID.NEIGHBOR_5,
        NEIGHBOR_6 = NeighborID.NEIGHBOR_8,
        NEIGHBOR_7 = NeighborID.NEIGHBOR_1,
        NEIGHBOR_8 = NeighborID.NEIGHBOR_4,
        NEIGHBOR_9 = NeighborID.NEIGHBOR_7,
    }

    // 3 0
    // 2 1
    public enum CornerID
    {
        RIGHT_UP = 0,
        RIGHT_DOWN = 1,
        LEFT_DOWN = 2,
        LEFT_UP = 3,
    }
}
