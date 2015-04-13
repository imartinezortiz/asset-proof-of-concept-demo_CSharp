// <copyright file="Asset.cs" company="RAGE">
// Copyright (c) 2015 RAGE All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>10-4-2015</date>
// <summary>Implements the asset class</summary>
namespace asset_proof_of_concept_demo_CSharp
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An asset.
    /// </summary>
    public class Asset : BaseAsset
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the asset_proof_of_concept_demo_CSharp.Asset class.
        /// </summary>
        public Asset()
            : base()
        {
            // Nothing yet
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Test if asset1 can find the Logger (asset3) thru the AssetManager.
        /// </summary>
        ///
        /// <remarks>
        /// This method does not belong here in a base class. So for testing purposes only.
        /// </remarks>
        ///
        /// <param name="msg"> The message. </param>
        public void publicMethod(String msg)
        {
            //! TODO Nicer would be to return the correct type of Asset.
            //
            List<IAsset> loggers = AssetManager.Instance.findAssetsByClass("Logger");

            if (loggers.Count > 0)
            {
                foreach (IAsset l in loggers)
                {
                    (l as Logger).log(l.Id + " - " + msg);
                }
            }
        }

        #endregion Methods
    }
}