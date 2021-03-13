using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using DragonBones;

public class Performance : MonoBehaviour
{
    public UnityDragonBonesData dragonBoneData;

    public UnityEngine.UI.Text text;
    void Start()
    {
        UnityFactory.factory.LoadData(dragonBoneData);

        //StartCoroutine(BuildArmatureComponent());
        Application.targetFrameRate = 60;
    }

    public List<GameObject> pos = new List<GameObject>();
    int index = 0;
   // IEnumerator BuildArmatureComponent()
   // {


        //foreach (GameObject POS in pos)
        //{




        //    var gameObject = new GameObject("mecha_1406");
        //    var armatureComponent = UnityFactory.factory.BuildArmatureComponent("mecha_1406", "", "", "", gameObject);
        //    // armatureComponent.gameObject.AddComponent<CombineMesh>();
        //    armatureComponent.armature.cacheFrameRate = 60; // Cache animation.
        //    armatureComponent.animation.Play("walk");
        //    armatureComponent.transform.localPosition = POS.transform.position;
        //    armatureComponent.transform.localScale = Vector3.one * 0.2f;

        //    // armatureComponent.combineMesh = true;

        //    yield return new WaitForSecondsRealtime(0.02f);
        //    text.text = "Count:" + (++index);

        //}

  //  }
    
}
