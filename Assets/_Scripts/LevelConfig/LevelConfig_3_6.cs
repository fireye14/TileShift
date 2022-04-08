using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    // 3x3 : 6 colors
    public class LevelConfig_3_6 : ILevelConfig
    {
        public int[,,] Setups
        {
            get
            {
                return new int[,,]
                {
                    //LEVEL ONE
			        {
                        {5, 5, 0, 5, 5, 0, 5, 5, 0},
                        {5, 5, 5, 5, 5, 5, 5, 5, 5}
                    },
			
			        //LEVEL TWO
			        {
                        {5, 0, 5, 0, 5, 0, 5, 0, 5},
                        {0, 5, 0, 5, 0, 5, 0, 5, 0}
                    },

			        //LEVEL THREE
			        {
                        {5, 0, 5, 5, 0, 5, 0, 5, 0},
                        {0, 5, 0, 0, 5, 0, 5, 0, 5}
                    },

			        //LEVEL FOUR
			        {
                        {5, 5, 4, 5, 5, 5, 5, 5, 5},
                        {5, 5, 5, 5, 5, 5, 5, 5, 5}
                    },

			        //LEVEL FIVE
			        {
                        {5, 5, 5, 5, 5, 5, 5, 4, 5},
                        {5, 5, 5, 5, 5, 5, 5, 5, 5}
                    },

			        //LEVEL SIX
			        {
                        {5, 5, 5, 5, 4, 5, 5, 5, 5},
                        {5, 5, 5, 5, 5, 5, 5, 5, 5}
                    },
                };
            }
        }
    }
}
