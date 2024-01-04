using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRecoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotaion;
    [SerializeField] ProjectileGun pg;
    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    [SerializeField] private float returnRate;
    // Start is called before the first frame update
    void Start()
    {
        //pg = GameObject.Find("ItemHoldingR").GetComponent<ProjectileGun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("ItemHoldingR")) 
        {
            GameObject weapon = GameObject.Find("ItemHoldingR");
            int index = weapon.GetComponent<WeaponSwitching>().selectedWeapon;
            Debug.Log(weapon.transform);
            foreach (Transform child in weapon.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    Debug.Log($"The child {child.name} is active!");
                    if (index == 0)
                    {
                        recoilX = 0;
                        recoilY = 0;
                        recoilZ = 0;
                    }
                    else
                    {
                        recoilX = child.GetComponent<ProjectileGun>().recoilX;
                        recoilY = child.GetComponent<ProjectileGun>().recoilY;
                        recoilZ = child.GetComponent<ProjectileGun>().recoilZ;
                    }
                    
                }

            }
            //if(GameObject.Find("ItemHoldingR").transform.GetChild(index).GetComponent<ProjectileGun>())
            //{
            //    GameObject.Find("ItemHoldingR").transform.GetChild(index).GetComponent<ProjectileGun>().recoilX
            //    pg = GameObject.Find("ItemHoldingR").transform.GetChild(index).GetComponent<ProjectileGun>();
            //    recoilX = pg.recoilX;
            //    recoilY = pg.recoilY;
            //    recoilZ = pg.recoilZ;
            //}
            //else
            //{
            //    recoilX = 0;
            //    recoilY = 0;
            //    recoilZ = 0;
            //}

        }
            
        

        targetRotaion = Vector3.Lerp(targetRotaion, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotaion, snappiness* Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        targetRotaion += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
