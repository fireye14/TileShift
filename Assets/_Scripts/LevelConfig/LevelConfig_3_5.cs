using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    // 3x3 : 5 colors
    public class LevelConfig_3_5 : ILevelConfig
    {
        public int[,,] Setups
        {
            get
            {
                return new int[,,]
                {
                    //LEVEL ONE
			        {
                        {4, 4, 0, 4, 4, 0, 4, 4, 0},
                        {4, 4, 4, 4, 4, 4, 4, 4, 4}
                    },
			
			        //LEVEL TWO
			        {
                        {4, 0, 4, 0, 4, 0, 4, 0, 4},
                        {0, 4, 0, 4, 0, 4, 0, 4, 0}
                    },

			        //LEVEL THREE
			        {
                        {4, 0, 4, 4, 0, 4, 0, 4, 0},
                        {0, 4, 0, 0, 4, 0, 4, 0, 1}
                    },

			        //LEVEL FOUR
			        {
                        {4, 4, 3, 4, 4, 4, 4, 4, 4},
                        {4, 4, 4, 4, 4, 4, 4, 4, 0}
                    },

			        //LEVEL FIVE
			        {
                        {4, 4, 4, 4, 4, 4, 4, 3, 4},
                        {4, 4, 4, 4, 4, 4, 4, 4, 0}
                    },

			        //LEVEL SIX
			        {
                        {4, 4, 4, 4, 3, 4, 4, 4, 4},
                        {4, 4, 4, 4, 4, 4, 4, 4, 0}
                    },
                };

            }
        }
    }
}
