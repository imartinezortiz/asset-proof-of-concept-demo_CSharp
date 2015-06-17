// <copyright file="ILogger.cs" company="RAGE"> Copyright (c) 2015 RAGE. All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>13-4-2015</date>
// <summary>defines the logger interface</summary>
namespace asset_proof_of_concept_demo_CSharp
{
    using System;
    
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
