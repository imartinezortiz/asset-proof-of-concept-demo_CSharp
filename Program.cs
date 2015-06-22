// <copyright file="Program.cs" company="RAGE">
// Copyright (c) 2015 RAGE. All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>10-4-2015</date>
// <summary>Implements the program class</summary>
namespace asset_proof_of_concept_demo_CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;
    using System.Xml.XPath;

    /// <summary>
    /// A program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The first bridge.
        /// </summary>
        static Bridge bridge1 = new Bridge("global bridge: ");

        /// <summary>
        /// The second bridge.
        /// </summary>
        static Bridge bridge2 = new Bridge();

        /// <summary>
        /// Handler, called when my event.
        /// </summary>
        ///
        /// <remarks>
        /// NOTE: Only static because the console programs Main is static too.
        /// </remarks>
        ///
        /// <param name="topic"> The topic. </param>
        /// <param name="args">  A variable-length parameters list containing arguments. </param>
        public static void MyEventHandler(String topic, params object[] args)
        {
            Console.WriteLine("[demo.html].{0}: [{1}]", topic, ArgsToString(args));
        }

        /// <summary>
        /// Arguments to String.
        /// </summary>
        ///
        /// <param name="args"> A variable-length parameters list containing arguments. </param>
        ///
        /// <returns>
        /// A String.
        /// </returns>
        public static String ArgsToString(params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                return String.Empty;
            }
            else
            {
                return String.Join(";", args.Select(p => p.ToString()).ToArray());
            }
        }

        /// <summary>
        /// Main entry-point for this application.
        /// </summary>
        ///
        /// <param name="args"> A variable-length parameters list containing arguments. </param>
        static void Main(string[] cargs)
        {
            //! See https://msdn.microsoft.com/en-us/library/system.version(v=vs.110).aspx

            //! major.minor[.build[.revision]]
            //!
            //! The components are used by convention as follows:
            //!
            //! Major:    Assemblies with the same name but different major versions are not interchangeable.
            //!           A higher version number might indicate a major rewrite of a product where backward
            //!           compatibility cannot be assumed.
            //! Minor:    If the name and major version number on two assemblies are the same,
            //!           but the minor version number is different, this indicates significant enhancement with
            //!           the intention of backward compatibility.
            //!           This higher minor version number might indicate a point
            //!           release of a product or a fully backward-compatible new version of a product.
            //! Build:    A difference in build number represents a recompilation of the same source.
            //!           Different build numbers might be used when the processor, platform, or compiler changes.
            //! Revision: Assemblies with the same name, major, and minor version numbers but different revisions
            //!           are intended to be fully interchangeable. A higher revision number might be used in a
            //!           build that fixes a security hole in a previously released assembly.
            //!
            //! For two versions to be equal, the major, minor, build, and revision numbers of the first Version
            //! object must be identical to those of the second Version object. If the build or revision number
            //! of a Version object is undefined, that Version object is considered to be earlier than a Version
            //! object whose build or revision number is equal to zero. The following example illustrates this
            //! by comparing three Version objects that have undefined version components.


            // 12.0.0.0
            // Major.Minor.Build.Revision.
            //Console.WriteLine("{0}", Assembly.GetCallingAssembly().GetName().Version);

            // CLR Version 4.0.30319.34209
            //Version ver = Environment.Version;
            //Debug.Print("CLR Version {0}", ver.ToString());

            //! Add assets and automatically create the Asset Manager. 
            // 
            Asset asset1 = new Asset();
            Asset asset2 = new Asset();

            Logger asset3 = new Logger();
            Logger asset4 = new Logger();

            DialogueAsset asset5 = new DialogueAsset();

            bridge2.Prefix = "private bridge: ";

            asset3.log("Asset1: " + asset1.Class + ", " + asset1.Id);
            asset3.log("Asset2: " + asset2.Class + ", " + asset2.Id);
            asset3.log("Asset3: " + asset3.Class + ", " + asset3.Id);
            asset3.log("Asset4: " + asset4.Class + ", " + asset4.Id);
            asset3.log("Asset5: " + asset5.Class + ", " + asset5.Id);

            //XDocument versionXml = asset1.VersionAndDependencies();
            //Console.WriteLine(versionXml.ToString());

            //IEnumerable<XElement> dependencies = versionXml.XPathSelectElements("version/dependencies/depends");
            //foreach (XElement dependency in dependencies)
            //{
            //    Console.WriteLine("Depends {0}", dependency.Value);
            //}

            Console.WriteLine(String.Empty);
            Console.WriteLine("Asset {0} v{1}", asset1.Class, asset1.Version);
            foreach (KeyValuePair<String, String> dependency in asset1.Dependencies)
            {
                Console.WriteLine("Depends on {0} v{1}", dependency.Key, dependency.Value);
            }
            Console.WriteLine(String.Empty);

            AssetManager.Instance.reportVersionAndDependencies();
            Console.WriteLine("Version: v{0}", asset1.Version);

            Console.WriteLine(String.Empty);

            // Use the new Logger directly. 
            // 
            asset3.log("LogByLogger: " + asset3.Class + ", " + asset3.Id);

            // Test if asset1 can find the Logger (asset3) thru the AssetManager. 
            // 
            asset1.publicMethod("Hello World (console.log)");

            //! TODO Implement replacing method behavior.
            //

            // Replace the both Logger's log method by a native version supplied by the Game Engine.
            // 
            AssetManager.Instance.Bridge = bridge1;

            // Check the results for both Loggers are alike. 
            // 
            asset1.publicMethod("Hello Different World (Game Engine Logging)");

            // Replace the 1st Logger's log method by a native version supplied by the Game Engine. 
            // 
            asset3.Bridge = bridge2;

            // Check the results for both Loggers differ (one message goes to the console, the other shows as an alert). 
            // 
            asset1.publicMethod("Hello Different World (Game Engine Logging)");

            #region IDataStorage and IDataArchive

            asset2.doStore();   // Create Hello1.txt and Hello2.txt
            asset2.doList();    // List
            asset2.doRemove();  // Remove Hello1.txt
            asset2.doList();    // List
            asset2.doArchive(); // Move Hello2.txt

            //! Reset/Remove Both Bridges
            // 
            asset3.Bridge = null;

            AssetManager.Instance.Bridge = null;

            asset2.doList();
            asset2.doStore();

            asset2.Bridge = bridge2;

            asset2.doStore();
            asset2.doList();

            asset2.Bridge = null;

            asset2.doList();

            #endregion IDataStorage and IDataArchive

            #region EventSubscription

            //! Event Subscription.
            // 
            // Define an event, subscribe to it and fire the event. 
            // 
            pubsubz.define("EventSystem.Msg");

            //! Using a method.
            // 
            {
                String eventId = pubsubz.subscribe("EventSystem.Msg", MyEventHandler);

                pubsubz.publish("EventSystem.Msg", "hello", "from", "demo.html!");

                pubsubz.unsubscribe(eventId);
            }

            //! Using delegate.
            // 
            {
                pubsubz.TopicEvent te = (topic, args) =>
                {
                    Console.WriteLine("[demo.html].{0}: [{1}] (delegate)", topic, ArgsToString(args));
                };

                String eventId = pubsubz.subscribe("EventSystem.Msg", te);

                pubsubz.publish("EventSystem.Msg", 1, 2, Math.PI);

                pubsubz.unsubscribe(eventId);
            }

            //! Using anonymous delegate.
            // 
            {
                String eventId = pubsubz.subscribe("EventSystem.Msg", (topic, args) =>
                {
                    Console.WriteLine("[demo.html].{0}: [{1}] (anonymous delegate)", topic, ArgsToString(args));
                });

                pubsubz.publish("EventSystem.Msg", "hello", "from", "demo.html!");

                pubsubz.unsubscribe(eventId);
            }

            #endregion EventSubscription

            //! Check if id and class can still be changed (shouldn't). 
            // 
            //asset4.Id = "XYY1Z"; 
            //asset4.Class = "test"; 
            //asset4.log("Asset4: " + asset4.Class + ", " + asset4.Id); 

            //! Test if we can re-register without creating new stuff in the register (i.e. get the existing unique id returned). 
            // 
            Console.WriteLine("Trying to re-register: {0}", AssetManager.Instance.registerAssetInstance(asset4, asset4.Class));

            #region DialogAsset

            //! DialogAsset.
            // 
            asset5.LoadScript("me", "script.txt");

            // Interacting using ask/tell 

            asset5.interact("me", "player", "banana");

            // Interacting using branches 
            // 
            asset5.interact("me", "player");
            asset5.interact("me", "player", 2); //Answer id 2 

            asset5.interact("me", "player");
            asset5.interact("me", "player", 6); //Answer id 6 

            asset5.interact("me", "player");

            #endregion DialogAsset

            Console.ReadKey();
        }
    }
}
