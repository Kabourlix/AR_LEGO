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
        private string _path, _jsonString;
        private DataModel _model;

        public void ExtractData(string filename)
        {
            _path = Application.streamingAssetsPath + "/NoticesData/" + filename;
            _jsonString = File.ReadAllText(_path);
            _model = JsonConvert.DeserializeObject<DataModel>(_jsonString);
        }
        
        
    }

    public class DataModel
    {
        public int id;
        public string name;
        public List<Step> steps;
    }

    public class Step
    {
        public string piece;
        public int x;
        public int y;
    }
}