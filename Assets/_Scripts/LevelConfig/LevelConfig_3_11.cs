using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    // 3x3 : 11 colors
    public class LevelConfig_3_11 : ILevelConfig
    {
        public int[,,] Setups
        {
            get
            {
                return new int[,,]
                {
                    //LEVEL ONE
			        {
                        {10, 10, 0, 10, 10, 0, 10, 10, 0},
                        {10, 10, 10, 10, 10, 10, 10, 10, 10}
                    },
			
			        //LEVEL TWO
			        {
                        {10, 0, 10, 0, 10, 0, 10, 0, 10},
                        {0, 10, 0, 10, 0, 10, 0, 10, 0}
                    },

			        //LEVEL THREE
			        {
                        {10, 0, 10, 10, 0, 10, 0, 10, 0},
                        {0, 10, 0, 0, 10, 0, 10, 0, 10}
                    },

			        //LEVEL FOUR
			        {
                        {10, 10, 9, 10, 10, 10, 10, 10, 10},
                        {10, 10, 10, 10, 10, 10, 10, 10, 10}
                    },

			        //LEVEL FIVE
			        {
                        {10, 10, 10, 10, 10, 10, 10, 9, 10},
                        {10, 10, 10, 10, 10, 10, 10, 10, 10}
                    },

			        //LEVEL SIX
			        {
                        {10, 10, 10, 10, 9, 10, 10, 10, 10},
                        {10, 10, 10, 10, 10, 10, 10, 10, 10}
                    },
                };
            }
        }
    }
}
