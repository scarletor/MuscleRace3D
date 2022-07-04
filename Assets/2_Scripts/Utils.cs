using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{
    public static Utils ins;

    public void Awake()
    {
        if (ins == null)
        {
            ins = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void DelayCall(float dl, System.Action cd)
    {
        StartCoroutine(DelayCallIE(cd, dl));
        IEnumerator DelayCallIE(System.Action cd2, float dl2)
        {
            yield return new WaitForSeconds(dl);
            cd.Invoke();
        }
    }


    [Button]
    public void FadeInUI(GameObject go)
    {
        CanvasGroup cvg = new CanvasGroup();
        if (go.GetComponent<CanvasGroup>() == null)
        {
            cvg = go.AddComponent<CanvasGroup>();
        }
        else
        {
            cvg = go.GetComponent<CanvasGroup>();
        }
        go.SetActive(true);
        var duration = 2;
        cvg.alpha = 0;
        float tweenValue = 0;
        float endTweenValue = 1;
        DOTween.To(() => tweenValue, x => tweenValue = x, endTweenValue, duration)
            .OnUpdate(() =>
            {
                cvg.alpha = tweenValue;
            });
    }


    [Button]
    public void FadeOutUI(GameObject go)
    {
        CanvasGroup cvg = new CanvasGroup();
        if (go.GetComponent<CanvasGroup>() == null)
        {
            cvg = go.AddComponent<CanvasGroup>();
        }
        else
        {
            cvg = go.GetComponent<CanvasGroup>();
        }
        go.SetActive(true);
        var duration = 2;
        cvg.alpha = 0;
        float tweenValue = 0;
        float endTweenValue = 1;
        DOTween.To(() => tweenValue, x => tweenValue = x, endTweenValue, duration)
            .OnUpdate(() =>
            {
                cvg.alpha = tweenValue;
                Debug.LogError(cvg.alpha);
                Debug.LogError(tweenValue);
            });
    }


    [Button]
    public void ReloadCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }





}
