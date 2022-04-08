using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    // 3x3 : 7 colors
    public class LevelConfig_3_7 : ILevelConfig
    {
        public int[,,] Setups
        {
            get
            {
                return new int[,,]
                {
                    //LEVEL ONE
			        {
                        {6, 6, 0, 6, 6, 0, 6, 6, 0},
                        {6, 6, 6, 6, 6, 6, 6, 6, 6}
                    },
			
			        //LEVEL TWO
			        {
                        {6, 0, 6, 0, 6, 0, 6, 0, 6},
                        {0, 6, 0, 6, 0, 6, 0, 6, 0}
                    },

			        //LEVEL THREE
			        {
                        {6, 0, 6, 6, 0, 6, 0, 6, 0},
                        {0, 6, 0, 0, 6, 0, 6, 0, 6}
                    },

			        //LEVEL FOUR
			        {
                        {6, 6, 5, 6, 6, 6, 6, 6, 6},
                        {6, 6, 6, 6, 6, 6, 6, 6, 6}
                    },

			        //LEVEL FIVE
			        {
                        {6, 6, 6, 6, 6, 6, 6, 5, 6},
                        {6, 6, 6, 6, 6, 6, 6, 6, 6}
                    },

			        //LEVEL SIX
			        {
                        {6, 6, 6, 6, 5, 6, 6, 6, 6},
                        {6, 6, 6, 6, 6, 6, 6, 6, 6}
                    },
                };
            }
        }
    }
}
