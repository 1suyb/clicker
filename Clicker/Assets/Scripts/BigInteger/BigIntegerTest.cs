using System;
using UnityEngine;
public class BigIntegerTest : MonoBehaviour
{
    
    void Start()
    {
        BigIntegerUnit unit1 = new BigIntegerUnit("646984694698469846946469469469469446846944");
        Debug.Log(unit1.ToSimbolString());
        Debug.Log((unit1+unit1).ToSimbolString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
