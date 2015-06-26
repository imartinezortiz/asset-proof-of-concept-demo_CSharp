using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asset_proof_of_concept_demo_CSharp
{
    public interface IVersion
    {
        /// <summary>
        /// Gets the version.
        /// </summary>
        ///
        /// <value>
        /// The version.
        /// </value>
        String Version
        {
            get;
        }

        /// <summary>
        /// Gets the maturity.
        /// </summary>
        ///
        /// <value>
        /// The maturity.
        /// </value>
        String Maturity
        {
            get;
        }

        /// <summary>
        /// Gets the dependencies.
        /// </summary>
        ///
        /// <value>
        /// The dependencies (A Dictionary of class=version pairs).
        /// </value>
        Dictionary<String, String> Dependencies
        {
            get;
        }
    }
}
