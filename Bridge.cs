// <copyright file="Bridge.cs" company="RAGE"> Copyright (c) 2015 RAGE. All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>13-4-2015</date>
// <summary>Implements a Bridge with 3 interfaces</summary>
namespace asset_proof_of_concept_demo_CSharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// A bridge.
    /// </summary>
    class Bridge : ILogger, IDataStorage, IDataArchive
    {
        const String StorageDir = @".\DataStorage";
        const String ArchiveDir = @".\Archive";

        /// <summary>
        /// Initializes a new instance of the asset_proof_of_concept_demo_CSharp.Bridge class.
        /// </summary>
        public Bridge()
        {
            this.Prefix = "";

            if (!Directory.Exists(StorageDir))
            {
                Directory.CreateDirectory(StorageDir);
            }

            if (!Directory.Exists(ArchiveDir))
            {
                Directory.CreateDirectory(ArchiveDir);
            }
        }

        /// <summary>
        /// Initializes a new instance of the asset_proof_of_concept_demo_CSharp.Bridge class.
        /// </summary>
        ///
        /// <param name="prefix"> The prefix. </param>
        public Bridge(String prefix)
            : base()
        {
            this.Prefix = prefix;
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
            //! Microsoft .Net Specific Code.
            // 
            Console.WriteLine(Prefix + msg);
        }

        #endregion

        #region ILogger Properties

        /// <summary>
        /// The prefix.
        /// </summary>
        public String Prefix
        {
            get;
            set;
        }

        #endregion

        #region IDataStorage Members

        /// <summary>
        /// Exists the given file.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to delete. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public bool Exists(string fileId)
        {
            return File.Exists(Path.Combine(StorageDir, fileId));
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        ///
        /// <returns>
        /// A List&lt;String&gt;
        /// </returns>
        public List<String> Files()
        {
            return Directory.GetFiles(StorageDir).ToList().ConvertAll(
    new Converter<String, String>(p => p.Replace(StorageDir + @"\", ""))).ToList();

            //! EnumerateFiles not supported in Unity3D.
            // 
            //return Directory.EnumerateFiles(StorageDir).ToList().ConvertAll(
            //    new Converter<String, String>(p => p.Replace(StorageDir + @"\", ""))).ToList();
        }

        /// <summary>
        /// Saves the given file.
        /// </summary>
        ///
        /// <param name="fileId">   The file identifier to delete. </param>
        /// <param name="fileData"> Information describing the file. </param>
        public void Save(string fileId, string fileData)
        {
            File.WriteAllText(Path.Combine(StorageDir, fileId), fileData);
        }

        /// <summary>
        /// Loads the given file.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to delete. </param>
        ///
        /// <returns>
        /// A String.
        /// </returns>
        public string Load(string fileId)
        {
            return File.ReadAllText(Path.Combine(StorageDir, fileId));
        }

        /// <summary>
        /// Deletes the given fileId.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to delete. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public bool Delete(string fileId)
        {
            if (Exists(fileId))
            {
                File.Delete(Path.Combine(StorageDir, fileId));

                return true;
            }

            return false;
        }

        #endregion

        #region IDataArchive Members

        /// <summary>
        /// Archives the given file.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to delete. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public bool Archive(string fileId)
        {
            if (File.Exists(Path.Combine(StorageDir, fileId)))
            {
                if (File.Exists(Path.Combine(ArchiveDir, fileId)))
                {
                    File.Delete(Path.Combine(ArchiveDir, fileId));
                }

                String stampName = String.Format("{0}-{1}{2}",
                    Path.GetFileNameWithoutExtension(fileId),
                    DateTime.Now.ToString("yyyy-MM-dd [HH mm ss fff]"),
                    Path.GetExtension(fileId));

                File.Move(Path.Combine(StorageDir, fileId), Path.Combine(ArchiveDir, stampName));

                return true;
            }

            return false;
        }

        #endregion

        //#region IVersionAndDependencies

        // This code is not elegant (embedded resources are not emitted in Unity so we would need the bridge to load dependencies if stored as xml.

        ///// <summary>
        ///// Version and dependencies XML.
        ///// </summary>
        /////
        ///// <returns>
        ///// A String.
        ///// </returns>
        //public String VersionAndDependenciesXml(Type Class)
        //{
        //    #region Unity3D

        //    //Object xml = Resources.Load(String.Format("{0}.VersionAndDependencies", Class.Name));
        //    //if (xml!=null && xml is TextAsset) {
        //    //	return (xml as TextAsset).text;
        //    //}

        //    #endregion

        //    #region .NET

        //    Assembly asm = Assembly.GetExecutingAssembly();

        //    String rn = String.Format("{0}.{1}.VersionAndDependencies.xml", Class.Namespace, Class.Name);

        //    if (asm != null && asm.GetManifestResourceNames().Contains(rn))
        //    {
        //        using (StreamReader reader = new StreamReader(asm.GetManifestResourceStream(rn)))
        //        {
        //            return reader.ReadToEnd();
        //        }
        //    }

        //    #endregion

        //    return String.Empty;
        //}

        //#endregion
    }
}
