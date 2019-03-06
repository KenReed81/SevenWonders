using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SevenWondersGUI
{
    class WB12 : Board
    {
        //private int[] resources = new int[7]{0,0,0,0,0,1,0};
        private bool freeBuild = false;

        public WB12()
            : base("WB12", 3, new int[3, 7] { { 0, 0, 0, 2, 0, 0, 0 }, { 0, 0, 3, 0, 0, 0, 0 }, { 0, 0, 0, 0, 1, 1, 1 } }, new int[7] { 0, 0, 0, 0, 0, 1, 0 })
        {}

        //public int[] getResources() { return resources; }

        public override void incrementWonderLevel(PlayerState p)
        { 
            if(notMaxYet()){
                currentWonderLevel+=1;
                switch (currentWonderLevel)
                {
                    case 1:
                        victoryPoints = 2;
                        freeBuild = true;
                        break;
                    case 2:
                        victoryPoints = 3;
                        freeBuild = true;
                        break;
                    case 3:
                        freeBuild = true;
                        break;
                }
            }
        }

        public bool getFreeBuild()
        {
            return freeBuild;
        }
        
        public void setFreeBuild(bool b)
        {
            freeBuild = b;
        }
    }
}