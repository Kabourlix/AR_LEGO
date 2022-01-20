using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// This class is aimed at extracting the notice data 
    /// </summary>
    public class Notice : MonoBehaviour
    {
        #region Make the class scriptable object
        public static Notice Instance;

        private void Awake()
        {
            if(Instance != null & Instance != this)
                Destroy(gameObject);

            Instance = this;
        }
        #endregion
        
        private string _path, _jsonString;
        private Root _model;

        private int _partIndex;

        /// <summary>
        /// This function is aimed at extracting the whole notice : containing each subnotices
        /// </summary>
        /// <param name="filename"> the name of the json file to parse</param>
        public void ExtractMainNotice(string filename)
        {
            _path = Application.streamingAssetsPath + "/NoticesData/" + filename + ".json";
            _jsonString = File.ReadAllText(_path);
            _model = JsonConvert.DeserializeObject<Root>(_jsonString);
        }

        public Part GetPart()
        {
            if (_partIndex > _model.parts.Count && _partIndex < 0) return null;
            Part p = _model.parts[_partIndex];
            _partIndex++; //We update the index for next call
            return p;
        }


    }
}

#region Classes for JSON Extraction
public class Step
{
    public string piece { get; set; }
    public string pos { get; set; }
    public string or { get; set; }
}

public class Part
{
    public string id { get; set; }
    public bool main { get; set; }
    public string result { get; set; }
    public List<Step> steps { get; set; }
}

public class Root
{
    public string nom { get; set; }
    public List<Part> parts { get; set; }
}

#endregion


