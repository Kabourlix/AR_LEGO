using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// This class contains the data related to a composed bricks
    /// </summary>
    public class Brick
    {
        private string _id;
        private List<GameObject> _gameObjects;

        public Brick(string id)
        {
            _id = id;
        }

        public void AddPart(GameObject gameObject, Vector3 position, Quaternion orientation)
        {
            gameObject.transform.position = position;
            gameObject.transform.rotation = orientation;
            _gameObjects.Add(gameObject);
        }
        
        /// <summary>
        /// Instantiate all the gameObject needed for the brick with their correct position.
        /// </summary>
        /// <param name="parent"> Parent for all the GameObjects</param>
        public void Instantiate(GameObject parent)
        {
            foreach (var gameObject in _gameObjects)
            {
                GameObject.Instantiate(gameObject,parent.transform);
            }
        }
    }
}