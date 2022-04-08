using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    // 3x3 : 2 colors
    public class LevelConfig_3_2 : ILevelConfig
    {
        public int[,,] Setups
        {
            get
            {
                return new int[,,]
                {
                    //LEVEL ONE
			        {
                        {1, 1, 0, 1, 1, 0, 1, 1, 0},	//starting
				        {1, 1, 1, 1, 1, 1, 1, 1, 1}		//solution
			        },

			        //LEVEL TWO
			        {
                        {1, 0, 1, 0, 1, 0, 1, 0, 1},
                        {0, 1, 0, 1, 0, 1, 0, 1, 0}
                    },

			        //LEVEL THREE
			        {
                        {1, 0, 1, 1, 0, 1, 0, 1, 0},
                        {0, 1, 0, 0, 1, 0, 1, 0, 1}
                    },

			        //LEVEL FOUR
			        {
                        {1, 1, 0, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1}
                    },

			        //LEVEL FIVE
			        {
                        {1, 1, 1, 1, 1, 1, 1, 0, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1}
                    },

			        //LEVEL SIX
			        {
                        {1, 1, 1, 1, 0, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1}
                    },

                    //LEVEL SEVEN
			        {                        
                        {1, 0, 1, 1, 1, 0, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1}
                    },

                    //LEVEL EIGHT
			        {
                        {0, 1, 1, 1, 0, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1}
                    },
                };
            }
        }
    }
}
