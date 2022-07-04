using System;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using TMPro;

namespace Racing
{
    public class PVE_UI_LoadingScreen : MonoBehaviour
    {

        public static PVE_UI_LoadingScreen ins;


        private void Awake()
        {
            ins = this;
        }


        AsyncOperation asyncLoad;
        public Text processText;
        public Slider slider;




        void Update()
        {
            if (asyncLoad == null) return;


            processText.text = "" + asyncLoad.progress + "%";
            slider.value = asyncLoad.progress;
        }


        bool isLoading;
        public void LoadLevel(string levelName)
        {
            StartCoroutine(LoadSceneAsync(levelName));
        }

        public GameObject loadingUI;
        public TMP_Text versionText;
        IEnumerator LoadSceneAsync(string levelName)
        {
            PVE_UI_TransitionScreen.ins.FadeIn();
            yield return new WaitForSeconds(1);
            versionText.text = "Version: " + Application.version;
            loadingUI.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            asyncLoad = SceneManager.LoadSceneAsync(levelName);

            while (!asyncLoad.isDone)
            {
                float progress = Mathf.Clamp01(asyncLoad.progress / .9f);
                Debug.Log(asyncLoad.progress);

                yield return null;
            }
        }




    }
}
