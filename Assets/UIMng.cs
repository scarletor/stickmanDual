using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Sirenix.OdinInspector;
public class UIMng : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        currentScene = homeUI;





    }

    // Update is called once per frame
    void Update()
    {

    }


    public GameObject homeUI, modeUI, play, playControl,topUI;
    public GameObject currentScene, nextScene;
    public bool canClickUI = true;
    enum sceneStateEnum
    {
        home,
        game,
        spin,
        shop,
        quest,
        mode
    }
    sceneStateEnum sceneState
    {
        get { return 0; }
        set
        {
            switch (value)
            {
                case sceneStateEnum.home:
                    Debug.LogError("home");

                    nextScene = homeUI;
                    FadeOutCurrentScene();
                    FadeInNextScene();
                    break;
                case sceneStateEnum.game:
                    Debug.LogError("game");

                    playControl.SetActive(true);
                    topUI.SetActive(false);





                    FadeOutCurrentScene();
                    

                    break;
                case sceneStateEnum.mode:
                    Debug.LogError("mode");


                    nextScene = modeUI;
                    FadeOutCurrentScene();
                    FadeInNextScene();
                    break;



            }
        }
    }

    CanvasGroup currentSceneCanvasGroup;
    void FadeOutCurrentScene()
    {
        currentSceneCanvasGroup = currentScene.GetComponent<CanvasGroup>();
        currentScene.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .3f);
        InvokeRepeating("FadeOutSceneSchedule", 0, Time.deltaTime);
    }

    void FadeOutSceneSchedule()
    {
        currentSceneCanvasGroup.alpha -= 0.06f;
        Debug.LogError("DROP ALPHA");
        if (currentSceneCanvasGroup.alpha <= 0)
        {
            Debug.LogError("DROP COMPLETE");
            CancelInvoke("FadeOutSceneSchedule");
            currentScene.SetActive(false);

            var temp = currentScene;
            currentScene = nextScene;
            nextScene = temp;

        }
    }




    CanvasGroup nextSceneCanvasGroup;
    void FadeInNextScene()
    {
        nextScene.SetActive(true);
        nextSceneCanvasGroup = nextScene.GetComponent<CanvasGroup>();
        nextScene.transform.localScale = new Vector3(0, 0, 0);
        nextScene.transform.DOScale(new Vector3(1, 1, 1), .3f);
        InvokeRepeating("FadeInSceneSchedule", 0, Time.deltaTime);
    }

    void FadeInSceneSchedule()
    {
        nextSceneCanvasGroup.alpha += 0.06f;
        Debug.LogError("INSCREASE ALPHA");
        if (nextSceneCanvasGroup.alpha >= 1)
        {
            Debug.LogError("COME COMPLETE");
            CancelInvoke("FadeInSceneSchedule");

           

        }
    }



    public void onClickMode()
    {
        sceneState = sceneStateEnum.mode;
    }

    public void onClickBack()
    {
        Debug.LogError("CLICK BACK");

        sceneState = sceneStateEnum.home;
        Debug.LogError("CLICK BACK");
    }


    public void onWWTFF()
    {
        Debug.LogError("CLICK onWWTFF");

        Debug.LogError("CLICK onWWTFF");
    }


    void GuiLine(int i_height = 1)

    {

        Rect rect = EditorGUILayout.GetControlRect(false, i_height);

        rect.height = i_height;

        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));

    }





    [Space(30)] // 10 pixels of spacing here.
    [TextArea]
    public string textArea1 = "Mode";


    public int modeSelecting;

    public void onClickSelectMode(int number)
    {
        modeSelecting = number;
    }
    public void PlayInModeScene()
    {
        if(modeSelecting==1)
        {
            sceneState = sceneStateEnum.game;
        }
        if (modeSelecting == 2)
        {

        }
        if (modeSelecting == 3)
        {

        }
        if (modeSelecting == 4)
        {

        }
    }

}
