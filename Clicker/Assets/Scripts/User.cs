using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    private Gold _gold;
    public Gold Gold => _gold;

    private void Awake()
    {
        GameManager.Instance.SetUser(this);
        _gold = new Gold();
    }
}
