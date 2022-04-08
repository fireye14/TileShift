using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    public interface ILevelConfig
    {

        /// <summary>
        /// every mode has a 3 dimensional array to define its levels:
        /// 1st dimension -> level #
        /// 2nd dimension -> starting & solution configuration
        /// 3rd dimension -> each tile
        /// </summary>
        int[,,] Setups { get; }
    }
}
