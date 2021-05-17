using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics
{
    public class teleptoreter1 : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider Col)
        {
            if (Col.tag == "Player")
            {
                Debug.Log(Col.transform.position);
                Col.transform.position = new Vector3(0, 0, 0);

            }
        }
    }
}