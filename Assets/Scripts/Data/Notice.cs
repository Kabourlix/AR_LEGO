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
        private DataModel _model;
        
        /// <summary>
        /// This function is aimed at extracting the whole notice : containing each subnotices
        /// </summary>
        /// <param name="filename"> the name of the json file to parse</param>
        public void ExtractMainNotice(string filename)
        {
            _path = Application.streamingAssetsPath + "/NoticesData/" + filename + ".json";
            _jsonString = File.ReadAllText(_path);
            _model = JsonConvert.DeserializeObject<DataModel>(_jsonString);
        }
        
        /// <summary>
        /// Extract the subnotice from a json file.
        /// </summary>
        /// <param name="filename"> the name of the json file.</param>
        public void ExtractCommonNotice(string filename)
        {
            
        }
        
    }
    
    



}
public class Step
{
    public string piece { get; set; }
    public string origin { get; set; }
    public string pos { get; set; }
    public string orientation { get; set; }
    public string resultName { get; set; }
    public string amount { get; set; }
}

public class DataModel
{
    public string id { get; set; }
    public string name { get; set; }
    public string origin { get; set; }
    public List<Step> steps { get; set; }
}