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
    public class PVE_UI_SelectMap : MonoBehaviour
    {




        [Button]
        public void OnClickStartPVEMap1()
        {
            PVE_UI_LoadingScreen.ins.LoadLevel("PVECity");
        }





    }
}
