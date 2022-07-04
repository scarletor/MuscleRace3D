using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PVEGamemanager : MonoBehaviour
{



    private void Start()
    {
        Application.targetFrameRate = 60;
        Physics.IgnoreLayerCollision(13, 13);
        Physics.IgnoreLayerCollision(13, 14);

    }



    public Text fpsText;
    public float deltaTime;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }







}

