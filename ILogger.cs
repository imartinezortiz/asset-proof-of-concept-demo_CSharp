using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asset_proof_of_concept_demo_CSharp
{
    /// <summary>
    /// Interface for logger.
    /// </summary>
    interface ILogger
    {
        /// <summary>
        /// The prefix.
        /// </summary>
        ///
        /// <value>
        /// The prefix.
        /// </value>
        String Prefix
        {
            get;
            set;
        }

        /// <summary>
        /// Executes the log operation.
        /// 
        /// Implement this in Game Engine Code.
        /// </summary>
        ///
        /// <param name="msg"> The message. </param>
        void doLog(String msg);
    }
}
