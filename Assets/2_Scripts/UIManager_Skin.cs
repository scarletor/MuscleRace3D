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
    public GameObject contents;


    public void OnClickSelectCharacter()
    {
        foreach (Transform child in contents.transform)
        {
            child.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().color = Color.green;

        characters.ForEach(go =>
        {
            go.SetActive(false);
            if (go == EventSystem.current.currentSelectedGameObject.GetComponent<UIManager_Skin_skinObj>().myObjRef)
            {
                go.SetActive(true);
                PlayerPrefs.SetString("curChar", go.name);
            }

        });
    }



    public void OnClickBack()
    {
        SceneManager.LoadScene("Play");
    }

}
