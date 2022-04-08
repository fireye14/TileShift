using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    // 3x3 : 8 colors
    public class LevelConfig_3_8 : ILevelConfig
    {
        public int[,,] Setups
        {
            get
            {
                return new int[,,]
                {
                    //LEVEL ONE
			        {
                        {7, 7, 0, 7, 7, 0, 7, 7, 0},
                        {7, 7, 7, 7, 7, 7, 7, 7, 7}
                    },
			
			        //LEVEL TWO
			        {
                        {7, 0, 7, 0, 7, 0, 7, 0, 7},
                        {0, 7, 0, 7, 0, 7, 0, 7, 0}
                    },

			        //LEVEL THREE
			        {
                        {7, 0, 7, 7, 0, 7, 0, 7, 0},
                        {0, 7, 0, 0, 7, 0, 7, 0, 7}
                    },

			        //LEVEL FOUR
			        {
                        {7, 7, 6, 7, 7, 7, 7, 7, 7},
                        {7, 7, 7, 7, 7, 7, 7, 7, 7}
                    },

			        //LEVEL FIVE
			        {
                        {7, 7, 7, 7, 7, 7, 7, 6, 7},
                        {7, 7, 7, 7, 7, 7, 7, 7, 7}
                    },

			        //LEVEL SIX
			        {
                        {7, 7, 7, 7, 6, 7, 7, 7, 7},
                        {7, 7, 7, 7, 7, 7, 7, 7, 7}
                    },
                };
            }
        }
    }
}
