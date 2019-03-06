using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SevenWondersGUI
{
    class WB2 : Board
    {
        //private int[] resources = new int[7] { 0, 0, 0, 0, 1, 0, 0 };

        public WB2()
            : base("WB2", 3, new int[3, 7] { { 0, 0, 2, 0, 0, 0, 0 }, { 2, 0, 0, 0, 0, 0, 0 }, { 0, 3, 0, 0, 0, 0, 0 } }, new int[7] { 0, 0, 0, 0, 1, 0, 0 })
        { }

        //public int[] getResources() { return resources; }

        public override void incrementWonderLevel(PlayerState p)
        {
            if (notMaxYet())
            {
                currentWonderLevel++;
                switch (currentWonderLevel)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        victoryPoints = 7;
                        break;

                }
            }
        }

    }
}
