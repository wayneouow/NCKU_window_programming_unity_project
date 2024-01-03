using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Ability2 : MonoBehaviour
{
    // Ability 2 : slow circle
    public GameObject fakePrefab;
    public GameObject realPrefab;  // real prefab
    private GameObject fakePreview; // preview 
    private Material fakeMaterial;  // preview material
    public float prefabLifetime = 4f; // duration

    public bool show2 = false;
    public bool cancle2 = false;
    // Start is called before the first frame update
    void Start()
    {
        // instance for prefab
        fakePreview = Instantiate(fakePrefab, Vector3.zero, Quaternion.identity);
        // preview is not active
        fakePreview.SetActive(false);
        // get preview material
        fakeMaterial = fakePreview.GetComponent<Renderer>().material;
        // 透明度0.5
        SetObjectAlpha(fakeMaterial, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // update preview position
        if (Input.GetKeyDown(KeyCode.Q))
        {
            show2 = true;
            cancle2 = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            show2 = false;
            cancle2 = true;
            fakePreview.SetActive(false);
        }
        if (show2 && !(cancle2))
        {
            UpdateFakePreviewPosition();
        }
        // click to make real prefab
        if (Input.GetMouseButtonDown(0) && show2)
        {
            //Debug.Log("產生緩速圈");
            GenerateRealPrefab();
            show2 = false;
        }
        
    }

    void UpdateFakePreviewPosition()
    {
        // get mouse position
        Vector3 mousePosition = Input.mousePosition;

        // mouse position into world position
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // if not create, then create
            if (!fakePreview.activeSelf)
            {
                fakePreview.SetActive(true);
            }

            // update position
            fakePreview.transform.position = hit.point;
        }
        else
        {
            // if preview is not on ground, do not use
            fakePreview.SetActive(false);
        }
    }

    void GenerateRealPrefab()
    {
        // make sure do not use preview prefab
        if (fakePreview.activeSelf)
        {
            // do not use preview prefab
            fakePreview.SetActive(false);

            // instance for real prefab
            GameObject realInstance = Instantiate(realPrefab, fakePreview.transform.position, Quaternion.identity);

            // destroy after duration 
            Destroy(realInstance, prefabLifetime);
            //EnemyControl.navAgent.speed = 3.5f;
        }
    }

    void SetObjectAlpha(Material material, float alpha)
    {
        // 材質
        Material newMaterial = new Material(material);
        // 透明度
        newMaterial.color = new Color(newMaterial.color.r, newMaterial.color.g, newMaterial.color.b, alpha);
        // 套用新材質
        fakePreview.GetComponent<Renderer>().material = newMaterial;
    }

}
