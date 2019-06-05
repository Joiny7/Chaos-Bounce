using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseModels : MonoBehaviour {

    [Serializable]
    public class BoardList
    {
        public List<RootObject> rootObjects;

    }

    [Serializable]
    public class RootObject
    {
        public Guid? userId;
        public string name;
        public int score;

        public RootObject(Guid? _userId, string _name, int _score)
        {
            userId = _userId.GetValueOrDefault();
            name = _name;
            score = _score;
        }
    }

    [Serializable]
    public class RootObject2
    {
        public string userId;
        public string name;
        public int score;

        public RootObject2(string _userId, string _name, int _score)
        {
            userId = _userId;
            name = _name;
            score = _score;
        }
    }

    [Serializable]
    public class FirstTimeRootObject
    {
        public string name;
        public int score;

        public FirstTimeRootObject(string _name, int _score)
        {
            name = _name;
            score = _score;
        }
    }
}