// <copyright file="Logger.cs" company="RAGE"> Copyright (c) 2015 RAGE. All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>10-4-2015</date>
// <summary>Implements the logger class</summary> 
namespace asset_proof_of_concept_demo_CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A logger.
    /// </summary>
    public class Logger : Asset
    {
        /// <summary>
        /// Logs.
        /// </summary>
        ///
        /// <param name="msg"> The message. </param>
        public void log(String msg)
        {
            Console.WriteLine(msg);
        }
    }
}
