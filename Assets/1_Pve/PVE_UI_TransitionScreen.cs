using System;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;


namespace Racing
{
    public class PVE_UI_TransitionScreen : MonoBehaviour
    {

        public static PVE_UI_TransitionScreen ins;
        public Image transition;
        private void Awake()
        {
            ins = this;
        }


        [Button]
        public void FadeIn()
        {
            var timeFade = 1;
            transition.gameObject.SetActive(true);
            transition.color = new Color(0, 0, 0, 0);
            transition.DOColor(new Color(0, 0, 0, 1), timeFade).Play().OnComplete(() =>
            {
                transition.DOColor(new Color(0, 0, 0, 0), timeFade).Play();
                Debug.LogError("OK");
            });

        }


        [Button]
        public void FadeOut()
        {

        }





    }












}
