// <copyright file="Logger.cs" company="RAGE"> Copyright (c) 2015 RAGE. All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>13-4-2015</date>
// <summary>Implements the logger class</summary>
namespace asset_proof_of_concept_demo_CSharp
{
    using System;

    /// <summary>
    /// A logger.
    /// </summary>
    public class Logger : BaseAsset
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the asset_proof_of_concept_demo_CSharp.Logger class.
        /// </summary>
        public Logger()
            : base()
        {
            //! Subscribe once (and never unsubscribe) to implement default behavior.
            //
            OnLog += doLog;
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// Logevents.
        /// </summary>
        ///
        /// <param name="msg"> The message. </param>
        public delegate void logevent(String msg);

        #endregion Delegates

        #region Events

        /// <summary>
        /// Occurs when log is called.
        /// </summary>
        public event logevent OnLog;

        #endregion Events

        #region Methods

        /// <summary>
        /// Logs.
        /// </summary>
        ///
        /// <param name="msg"> The message. </param>
        public void log(String msg)
        {
            if (OnLog != null)
            {
                OnLog(msg);
            }
        }

        /// <summary>
        /// Logs only if there is only one subscriber (so default behavior is needed).
        /// </summary>
        ///
        /// <param name="msg"> The message. </param>
        private void doLog(String msg)
        {
            //! If we're the only subscriber, we expose default behavior.
            //
            if (OnLog.GetInvocationList().Length == 1)
            {
                Console.WriteLine(msg);
            }
        }

        #endregion Methods
    }
}