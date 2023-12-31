using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class maps_select : MonoBehaviour
{
    public Button LoadSceneBtn;
    public Image hoverImg;
    public string sceneName;
    public Image BGImg;
    public Image DefalutImg;
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        BGImg.sprite = DefalutImg.sprite;
    }
    void Start()
    {      
        LoadSceneBtn.onClick.AddListener(LoadScene);
    }
    public void BGImg_PointerEnter()
    {
        BGImg.sprite = hoverImg.sprite;
    }
    public void BGImg_PointerExit()
    {
        BGImg.sprite = DefalutImg.sprite;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

}
