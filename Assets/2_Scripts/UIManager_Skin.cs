using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
public class UIManager_Skin : MonoBehaviour
{







    public List<GameObject> characters;



    public void OnClickSelectCharacter()
    {
        characters.ForEach(go =>
        {
            go.SetActive(false);
            if (go == EventSystem.current.currentSelectedGameObject.GetComponent<UIManager_Skin_skinObj>().myObjRef)
                go.SetActive(true);

        });


    }


}
