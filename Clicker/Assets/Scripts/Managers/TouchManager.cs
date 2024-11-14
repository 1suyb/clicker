using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : Singleton<TouchManager>
{
    private Gold _gold;
    [SerializeField] private float autoClickInterval;
    private WaitForSeconds _autoClick;
    [SerializeField] private ParticleSystem _particleSystem;


    private void Start()
    {
        _autoClick = new WaitForSeconds(autoClickInterval);
        _gold = GameManager.Instance.User.Gold;
        StartCoroutine(AutoTouch());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                _gold.AddGold();
                _particleSystem.transform.position = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
                _particleSystem.Play();
            }
        }
    }

    private IEnumerator AutoTouch()
    {
        while (true)
        {
            yield return _autoClick;
            _gold.AddGold();
        }
    }


}
