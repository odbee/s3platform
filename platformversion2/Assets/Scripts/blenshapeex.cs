using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blenshapeex : MonoBehaviour

{
    [Tooltip("blender for blendshape control")]
    [SerializeField]
    private GameObject blender;


    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
    public float blendOne;
    public float blendTwo = 0f;
    float blendThree = 0f;
    float blendFour = 0f;

    float blendSpeed = 1f;
    bool blendOneFinished = false;

    void Awake()
    {
        skinnedMeshRenderer = blender.GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = blender.GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    void Start()
    {
    }

    void Update()
    {
        //if (blendShapeCount > 2)
        //{
        //    if (blendOne < 100f)
        //    {
        //        skinnedMeshRenderer.SetBlendShapeWeight(0, blendOne);
        //        blendOne += blendSpeed;
        //    }
        //    else
        //    {
        //        blendOneFinished = true;
        //    }

        //    if (blendOneFinished == true && blendTwo < 100f)
        //    {
        //        skinnedMeshRenderer.SetBlendShapeWeight(1, blendTwo);
        //        blendTwo += blendSpeed;
        //    }
        //}
        skinnedMeshRenderer.SetBlendShapeWeight(0, PlayerPrefs.GetFloat("varone", 1));
        skinnedMeshRenderer.SetBlendShapeWeight(1, PlayerPrefs.GetFloat("vartwo", 1));
        skinnedMeshRenderer.SetBlendShapeWeight(2, PlayerPrefs.GetFloat("varthree", 1));
        skinnedMeshRenderer.SetBlendShapeWeight(3, PlayerPrefs.GetFloat("varfour", 1));

    }
}