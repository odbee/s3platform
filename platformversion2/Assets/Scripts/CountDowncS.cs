using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDowncS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMyObject", 5);

    }

    // Update is called once per frame
    void DestroyMyObject()
    {
        this.gameObject.SetActive(false);

    }
}
