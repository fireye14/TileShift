using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    // 3x3 : 10 colors
    public class LevelConfig_3_10 : ILevelConfig
    {
        public int[,,] Setups
        {
            get
            {
                return new int[,,]
                {
                    //LEVEL ONE
			        {
                        {9, 9, 0, 9, 9, 0, 9, 9, 0},
                        {9, 9, 9, 9, 9, 9, 9, 9, 9}
                    },
			
			        //LEVEL TWO
			        {
                        {9, 0, 9, 0, 9, 0, 9, 0, 9},
                        {0, 9, 0, 9, 0, 9, 0, 9, 0}
                    },

			        //LEVEL THREE
			        {
                        {9, 0, 9, 9, 0, 9, 0, 9, 0},
                        {0, 9, 0, 0, 9, 0, 9, 0, 6}
                    },

			        //LEVEL FOUR
			        {
                        {9, 9, 8, 9, 9, 9, 9, 9, 9},
                        {9, 9, 9, 9, 9, 9, 9, 9, 5}
                    },

			        //LEVEL FIVE
			        {
                        {9, 9, 9, 9, 9, 9, 9, 8, 9},
                        {9, 9, 9, 9, 9, 9, 9, 9, 0}
                    },

			        //LEVEL SIX
			        {
                        {9, 9, 9, 9, 8, 9, 9, 9, 9},
                        {9, 9, 9, 9, 9, 9, 9, 9, 0}
                    },
                };
            }
        }
    }
}
