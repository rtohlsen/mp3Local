/*!**********************************************
 * XMLSerializer.cs
 * 18 April 2013
 * V 1.0
 * Cahman Games
 * www.cahman.com
 *
 *
 * Provides static functions for
 * 1) serializing to XML then saving playerprefs
 * 2) serializing to XML, then saving to a local file
 * 3) serializing to XML, then sending to a web address
 *
 *
 *************************************************/
/***************************************************
 * LICENSE:
 * Copyright (c) 2013 CahmanGames www.cahman.com
 *
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 *
   **************************************************************/

using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;

public static class UnityXMLSerializer {

    private static System.Type[] extraSerializeTypes;

    //public static void SetupSerializer(System.Type[] serializeTypes)
    //{
    //  serializer = new XmlSerializer(


    /// <summary>
    /// Serialize an object to an xml file.
    /// </summary>
    /// <returns>
    /// True if written, false if file exists and overWrite is false
    /// </returns>
    /// <param name='writePath'>
    /// Where to write the file.  Consider using Application.PersistentData
    /// </param>
    /// <param name='serializableObject'>
    /// The Object to be serialized.  The generic needs to be the type of the object to be serialized
    /// </param>
    /// <param name='overWrite'>
    /// If set to <c>true</c> over write the file if it exists.
    /// </param>
    public static bool SerializeToXMLFile<T>(string writePath, object serializableObject, bool overWrite = true) {
        if (File.Exists(writePath) && overWrite == false)
            return false;
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (var writeFile = File.Create(writePath)) {
            serializer.Serialize(writeFile, serializableObject);
        }
        return true;
    }

    /// <summary>
    /// Deserialize an object from an XML file.
    /// </summary>
    /// <returns>
    /// The deserialized list.  If the file doesn't exist, returns the default for 'T'
    /// </returns>
    /// <param name='readPath'>
    /// Where to read the file from.
    /// </param>
    /// <typeparam name='T'>
    /// Type of object being loaded from the file
    /// </typeparam>
    public static T DeserializeFromXMLFile<T>(string readPath) {
        if (!File.Exists(readPath))
            return default(T);

        XmlSerializer serializer = new XmlSerializer(typeof(T));

        using (var readFile = File.OpenRead(readPath))
            return (T)serializer.Deserialize(readFile);


    }




}
