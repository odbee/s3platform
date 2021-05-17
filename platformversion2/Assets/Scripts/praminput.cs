using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Photon.Pun.Demo.PunBasics
{
    public class praminput : MonoBehaviour
    {


        public void SetvarOne(float value)
        {
            PlayerPrefs.SetFloat("varone", value);
        }
        public void SetvarTwo(float value)
        {
            PlayerPrefs.SetFloat("vartwo", value);
        }
        public void SetvarThree(float value)
        {
            PlayerPrefs.SetFloat("varthree", value);
        }
        public void SetvarFour(float value)
        {
            PlayerPrefs.SetFloat("varfour", value);
        }
    }
}