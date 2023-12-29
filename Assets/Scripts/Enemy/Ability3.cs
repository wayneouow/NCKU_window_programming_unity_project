using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability3 : MonoBehaviour
{
    // Ability 3 : heal circle
    public GameObject fakePrefab;
    public GameObject realPrefab;  // real prefab
    private GameObject fakePreview; // preview 
    private Material fakeMaterial;  // preview material
    public float prefabLifetime = 4f; // duration
    public bool show = false;
    public bool cancle = false;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            show = true;
            cancle = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            show = false;
            cancle = true;
            //好像要摧毀prefab
            fakePreview.SetActive(false);
        }
        if (show && !(cancle))
        {
            UpdateFakePreviewPosition();
        }
        // click to make real prefab
        if (Input.GetMouseButtonDown(0) && show )
        {
            Debug.Log("產生治療圈");
            GenerateRealPrefab();
            show = false;
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
