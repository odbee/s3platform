using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSelection : MonoBehaviour
{
    [SerializeField]
    private List<ToggleIcon> _toggleIcons;

    private void Start()
    {
        foreach (ToggleIcon toggleIcon in _toggleIcons)
        {
            toggleIcon.toggle.onValueChanged.AddListener(delegate { UpdateParameter(toggleIcon.toggle, toggleIcon.val1, toggleIcon.val2, toggleIcon.val3, toggleIcon.val4); });
            PlayerPrefs.SetFloat("varone", 0);
            PlayerPrefs.SetFloat("vartwo", 0);
            PlayerPrefs.SetFloat("varthree", 0);
            PlayerPrefs.SetFloat("varfour", 0);

        }
    }

    private void UpdateParameter(Toggle toggle, float val1, float val2, float val3, float val4)
    {
        float newv1 = PlayerPrefs.GetFloat("varone", 0);
        float newv2 = PlayerPrefs.GetFloat("vartwo", 0);
        float newv3 = PlayerPrefs.GetFloat("varthree", 0);
        float newv4 = PlayerPrefs.GetFloat("varfour", 0);


        if (toggle.isOn)
        {
            newv1 += val1;
            newv2 += val2;
            newv3 += val3;
            newv4 += val4;
        }
        else
        {
            newv1 -= val1;
            newv2 -= val2;
            newv3 -= val3;
            newv4 -= val4;
        }
        PlayerPrefs.SetFloat("varone", newv1);
        PlayerPrefs.SetFloat("vartwo", newv2);
        PlayerPrefs.SetFloat("varthree", newv3);
        PlayerPrefs.SetFloat("varfour", newv4);

        Debug.Log("Updated parameter varone" + ": " + newv1);
    }
}

[System.Serializable]
public class ToggleIcon
{
    public Toggle toggle;
    public float val1;
    public float val2;
    public float val3;
    public float val4;


    public ToggleIcon( Toggle _toggle, float _val1,float _val2, float _val3, float _val4)
    {
        toggle = _toggle;
        
        val1 = _val1;
        val2 = _val2;
        val3 = _val3;
        val4 = _val4;

    }
}
