using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asset_proof_of_concept_demo_CSharp
{
    /// <summary>
    /// A bridge.
    /// </summary>
    class Bridge : ILogger
    {
        /// <summary>
        /// Initializes a new instance of the asset_proof_of_concept_demo_CSharp.Bridge class.
        /// </summary>
        public Bridge()
        {
            this.prefix = "";
        }

        /// <summary>
        /// Initializes a new instance of the asset_proof_of_concept_demo_CSharp.Bridge class.
        /// </summary>
        ///
        /// <param name="prefix"> The prefix. </param>
        public Bridge(String prefix)
            : base()
        {
            this.prefix = prefix;
        }

        #region ILogger Members

        /// <summary>
        /// Executes the log operation.
        /// 
        /// Implement this in Game Engine Code.
        /// </summary>
        ///
        /// <param name="msg"> The message. </param>
        public void doLog(string msg)
        {
            Console.WriteLine(prefix + msg);
        }

        #endregion

        #region ILogger Properties

        /// <summary>
        /// The prefix.
        /// </summary>
        public String prefix
        {
            get;
            private set;
        }

        #endregion

    }
}
