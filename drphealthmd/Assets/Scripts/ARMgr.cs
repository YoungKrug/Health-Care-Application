using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARMgr : MonoBehaviour {
    public GameObject infoCanvas;
    public GameObject arCanvas;
    public GameObject infoIcon;
    public GameObject trashIcon;
    public AudioSource audio;
    // 3D Models
    public GameObject[] models;
    int currentModel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowInfo()
    {
        audio.Play();
        arCanvas.SetActive(false);
        infoCanvas.SetActive(true);
    }

    public void HideInfo()
    {
        audio.Play();
        infoCanvas.SetActive(false);
        arCanvas.SetActive(true);
    }

    public void TargetFoundFunc(int targetModel)
    {
        ShowAllModels();
        currentModel = targetModel;
        infoIcon.SetActive(true);
        trashIcon.SetActive(true);
    }

    public void TrashIcon()
    {
        audio.Play();
        SceneManager.LoadScene(6);
    }

    public void TargetLostFunc(int modelNumber)
    {
        
        //infoIcon.SetActive(false);
        //trashIcon.SetActive(false);
    }
    void HideAllModels()
    {
        foreach (GameObject model in models)
        {
            model.SetActive(false);
        }
        ShowAllModels();
    }

    void ShowAllModels()
    {
        foreach (GameObject model in models)
        {
            model.SetActive(true);
        }
    }
}
