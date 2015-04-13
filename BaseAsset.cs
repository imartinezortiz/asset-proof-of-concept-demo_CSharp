﻿// <copyright file="BaseAsset.cs" company="RAGE">
// Copyright (c) 2015 RAGE All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>13-4-2015</date>
// <summary>Implements the base asset class</summary>
namespace asset_proof_of_concept_demo_CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseAsset : IAsset
    {
        #region Fields

        private String testSubscription;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the asset_proof_of_concept_demo_CSharp.Asset class.
        /// </summary>
        public BaseAsset()
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

        #endregion Constructors

        #region Properties

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

        #endregion Properties
    }
}
