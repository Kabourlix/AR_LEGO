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
        public static Notice Instance;

        private void Awake()
        {
            if(Instance != null & Instance != this)
                Destroy(gameObject);

            Instance = this;
        }

        private string _path, _jsonString;
        private Root _model;
        
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

        public Step GetStep(int partIndex, int stepIndex)
        {
            if (partIndex > _model.parts.Count && partIndex < 0) return null;
            if (stepIndex > _model.parts[partIndex].steps.Count && stepIndex < 0) return null;
            return _model.parts[partIndex].steps[stepIndex];
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
    public string main { get; set; }
    public string result { get; set; }
    public List<Step> steps { get; set; }
}

public class Root
{
    public string nom { get; set; }
    public List<Part> parts { get; set; }
}

#endregion


