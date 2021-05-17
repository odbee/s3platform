using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkfriendsinsphere : MonoBehaviour
{
    int playersInVicinty = 0;
    Collider[] colliders;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        colliders = Physics.OverlapSphere(transform.position, 10f);
        playersInVicinty = 0;
        foreach (var collider in colliders)
        {
            if (collider.tag == "Player")
                playersInVicinty +=1;
        }


        Debug.Log(playersInVicinty);
    }
    //void OnTriggerEnter(Collider Col)
    //{
    //    Debug.Log("collider"+ Col);
    //}
}