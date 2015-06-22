// <copyright file="BaseAsset.cs" company="RAGE">
// Copyright (c) 2015 RAGE All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>13-4-2015</date>
// <summary>Implements the base asset class</summary>
namespace asset_proof_of_concept_demo_CSharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public class BaseAsset : IAsset
    {
        #region Fields

        /// <summary>
        /// The test subscription.
        /// </summary>
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

            //! List Embedded Resources.
            //foreach (String name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            //{
            //    Console.WriteLine("{0}", name);
            //}
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

        /// <summary>
        /// Gets or sets the bridge.
        /// </summary>
        ///
        /// <value>
        /// The bridge.
        /// </value>
        public object Bridge
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        ///
        /// <value>
        /// The version.
        /// </value>
        public String Version
        {
            get
            {
                XDocument versionXml = VersionAndDependencies();

                return String.Format("{0}.{1}.{2}.{3}",
                    XmlTagValue(versionXml, "version/major"),
                    XmlTagValue(versionXml, "version/minor"),
                    XmlTagValue(versionXml, "version/build"),
                    XmlTagValue(versionXml, "version/revision")).TrimEnd('.');
            }
        }

        /// <summary>
        /// Gets the maturity.
        /// </summary>
        ///
        /// <value>
        /// The maturity.
        /// </value>
        public String Maturity
        {
            get
            {
                return XmlTagValue(VersionAndDependencies(), "version/maturity");
            }
        }

        /// <summary>
        /// Gets the dependencies.
        /// </summary>
        ///
        /// <value>
        /// The dependencies.
        /// </value>
        public Dictionary<String, String> Dependencies
        {
            get
            {
                Dictionary<String, String> dependencies = new Dictionary<String, String>();

                foreach (XElement dependency in VersionAndDependencies().XPathSelectElements("version/dependencies/depends"))
                {
                    String minv = dependency.Attribute("minVersion") != null ? dependency.Attribute("minVersion").Value : "0.0";
                    String maxv = dependency.Attribute("maxVersion") != null ? dependency.Attribute("maxVersion").Value : "*";

                    dependencies.Add(dependency.Value, String.Format("{0}-{1},", minv, maxv));
                }

                return dependencies;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the interface.
        /// </summary>
        ///
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        ///
        /// <returns>
        /// The interface.
        /// </returns>
        internal T getInterface<T>()
        {
            if (Bridge != null && Bridge is T)
            {
                return (T)Bridge;
            }
            else if (AssetManager.Instance.Bridge != null && AssetManager.Instance.Bridge is T)
            {
                return (T)(AssetManager.Instance.Bridge);

            }

            return default(T);
        }

        /// <summary>
        /// XML tag value.
        /// </summary>
        ///
        /// <param name="doc">   The document. </param>
        /// <param name="xpath"> The xpath. </param>
        ///
        /// <returns>
        /// A String.
        /// </returns>
        private String XmlTagValue(XDocument doc, String xpath)
        {
            if (doc.XPathSelectElement(xpath) != null)
            {
                return doc.XPathSelectElement(xpath).Value;
            }
            return String.Empty;
        }

        /// <summary>
        /// Version and dependencies.
        /// </summary>
        ///
        /// <returns>
        /// An XDocument.
        /// </returns>
        internal XDocument VersionAndDependencies()
        {
            return XDocument.Parse(
                GetEmbeddedResource(GetType().Namespace, String.Format(String.Format("{0}.VersionAndDependencies.xml", GetType().Name))));
        }

        /// <summary>
        /// Gets embedded resource.
        /// </summary>
        ///
        /// <param name="ns">  The namespace. </param>
        /// <param name="res"> The resource name. </param>
        ///
        /// <returns>
        /// The embedded resource.
        /// </returns>
        protected String GetEmbeddedResource(String ns, String res)
        {
            using (StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(String.Format("{0}.{1}", ns, res))))
            {
                return reader.ReadToEnd();
            }
        }

        #endregion Methods
    }
}
