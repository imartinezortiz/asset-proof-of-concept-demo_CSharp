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
    public class Asset : IAsset
    {
        /// <summary>
        /// Gets the class.
        /// </summary>
        ///
        /// <value>
        /// The class.
        /// </value>
        public String Class
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        ///
        /// <value>
        /// The identifier.
        /// </value>
        public String Id
        {
            get;
            private set;
        }

        private String testSubscription;

        /// <summary>
        /// Initializes a new instance of the asset_proof_of_concept_demo_CSharp.Asset class.
        /// </summary>
        public Asset()
        {
            this.Id = AssetManager.Instance.registerAssetInstance(this, this.Class);

            //! NOTE Unlike the JavaScript and Typescript versions (using a setTimeout) registration will not get triggered during publish in the AssetManager constructor.
            // 
            testSubscription = pubsubz.subscribe("EventSystem.Init", (topics, data) =>
            {
                //This code fails in TypeScript (coded there as 'this.Id') as this points to the method and not the Asset. 
                Console.WriteLine("[{0}].{1}: {2}", this.Id, topics, data);
            });
        }

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
    }
}
