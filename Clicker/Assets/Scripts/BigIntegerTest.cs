using System;
using UnityEngine;
public class BigIntegerTest : MonoBehaviour
{
    
    void Start()
    {
        BigInteger unit1 = new BigInteger("123456789123646854654987987896489489561231654984896456416516847804894098409874045132132813279167514917485247621972975378431245");
        Debug.Log(unit1.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
