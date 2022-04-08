using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    // 3x3 : 4 colors
    public class LevelConfig_3_4 : ILevelConfig
    {
        public int[,,] Setups
        {
            get
            {
                return new int[,,]
                {
                    //LEVEL ONE
			        {
                        {3, 3, 0, 3, 3, 0, 3, 3, 0},
                        {3, 3, 3, 3, 3, 3, 3, 3, 3}
                    },
			
			        //LEVEL TWO
			        {
                        {3, 0, 3, 0, 3, 0, 3, 0, 3},
                        {0, 3, 0, 3, 0, 3, 0, 3, 0}
                    },

			        //LEVEL THREE
			        {
                        {3, 0, 3, 3, 0, 3, 0, 3, 0},
                        {0, 3, 0, 0, 3, 0, 3, 0, 3}
                    },

			        //LEVEL FOUR
			        {
                        {3, 3, 2, 3, 3, 3, 3, 3, 3},
                        {3, 3, 3, 3, 3, 3, 3, 3, 3}
                    },

			        //LEVEL FIVE
			        {
                        {3, 3, 3, 3, 3, 3, 3, 2, 3},
                        {3, 3, 3, 3, 3, 3, 3, 3, 3}
                    },

			        //LEVEL SIX
			        {
                        {3, 3, 3, 3, 2, 3, 3, 3, 3},
                        {3, 3, 3, 3, 3, 3, 3, 3, 3}
                    },
                };

            }
        }
    }
}
