using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class OnSelected : MonoBehaviour
{
    public GameObject selectedText;
    public GameObject Ui;
    public GameObject cam1;
    public GameObject cam2;
    public PlayableAsset select;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SallySelectd()
    {
        selectedText.GetComponent<Text>().text = "Sally Is Selected";
    }
    public void AJSelected()
    {
        selectedText.GetComponent<Text>().text = "AJ Is Selected";
    }
    public void IsSelected()
    {
        if (selectedText.GetComponent<Text>().text != "")
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            Ui.SetActive(false);

        }

    }
}
