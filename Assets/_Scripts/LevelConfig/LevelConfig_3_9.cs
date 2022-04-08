using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    // 3x3 : 9 colors
    public class LevelConfig_3_9 : ILevelConfig
    {
        public int[,,] Setups
        {
            get
            {
                return new int[,,]
                {
                    //LEVEL ONE
			        {
                        {8, 8, 0, 8, 8, 0, 8, 8, 0},
                        {8, 8, 8, 8, 8, 8, 8, 8, 8}
                    },
			
			        //LEVEL TWO
			        {
                        {8, 0, 8, 0, 8, 0, 8, 0, 8},
                        {0, 8, 0, 8, 0, 8, 0, 8, 0}
                    },

			        //LEVEL THREE
			        {
                        {8, 0, 8, 8, 0, 8, 0, 8, 0},
                        {0, 8, 0, 0, 8, 0, 8, 0, 8}
                    },

			        //LEVEL FOUR
			        {
                        {8, 8, 7, 8, 8, 8, 8, 8, 8},
                        {8, 8, 8, 8, 8, 8, 8, 8, 8}
                    },

			        //LEVEL FIVE
			        {
                        {8, 8, 8, 8, 8, 8, 8, 7, 8},
                        {8, 8, 8, 8, 8, 8, 8, 8, 8}
                    },

			        //LEVEL SIX
			        {
                        {8, 8, 8, 8, 7, 8, 8, 8, 8},
                        {8, 8, 8, 8, 8, 8, 8, 8, 8}
                    },
                };
            }
        }
    }
}
