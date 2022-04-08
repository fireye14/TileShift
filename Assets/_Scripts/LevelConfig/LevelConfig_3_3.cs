using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    // 3x3 : 3 colors
    public class LevelConfig_3_3 : ILevelConfig
    {
        public int[,,] Setups
        {
            get
            {
                return new int[,,]
                {
                    //LEVEL ONE
			        {
                        {2, 2, 0, 2, 2, 0, 2, 2, 0},
                        {2, 2, 2, 2, 2, 2, 2, 2, 2}
                    },

			        //LEVEL TWO
			        {
                        {2, 0, 2, 0, 2, 0, 2, 0, 2},
                        {0, 2, 0, 2, 0, 2, 0, 2, 0}
                    },

			        //LEVEL THREE
			        {
                        {2, 0, 2, 2, 0, 2, 0, 2, 0},
                        {0, 2, 0, 0, 2, 0, 2, 0, 2}
                    },

			        //LEVEL FOUR
			        {
                        {2, 2, 1, 2, 2, 2, 2, 2, 2},
                        {2, 2, 2, 2, 2, 2, 2, 2, 2}
                    },

			        //LEVEL FIVE
			        {
                        {2, 2, 2, 2, 2, 2, 2, 1, 2},
                        {2, 2, 2, 2, 2, 2, 2, 2, 2}
                    },

			        //LEVEL SIX
			        {
                        {2, 2, 2, 2, 1, 2, 2, 2, 2},
                        {2, 2, 2, 2, 2, 2, 2, 2, 2}
                    }
                };
            }
        }
    }
}
