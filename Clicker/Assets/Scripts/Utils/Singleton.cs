using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    GameObject obj = new GameObject(GameConfig.MANAGERNAME);
                    _instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(_instance);
                }
            }
            return _instance;
        }
    }
}
