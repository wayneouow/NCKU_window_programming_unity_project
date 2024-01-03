
using UnityEngine;
using TMPro;

public class Melee : MonoBehaviour
{
    public GameObject attackBox;
    //Beam
    public GameObject beam;

    //Beam force
    public float shootForce, upwardForce;

    //Sword stats
    public float timeBetweenSlashing, timeBetweenSlashs;
    public int bulletsPerTap;

    int  bulletsShot;

    //bools
    bool slashing, readyToSlash;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    public Animator animator;

    //bug fixing 
    public bool allowInvoke = true;


    private void Start()
    {

    }

    private void Awake()
    {
        readyToSlash = true;
    }

    private void Update()
    {
        MyInput();

        //animator = transform.parent.GetComponent<Animator>();

    }
    private void MyInput()
    {
        slashing = Input.GetKeyDown(KeyCode.Mouse0);

        //Shooting
        if (readyToSlash && slashing )
        {
            //Set bullets shot to 0
            bulletsShot = 0;
            //animator.SetBool("Fire", true);
            Slash();

        }
    }

    private void Slash()
    {
        readyToSlash = false;

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 direction = targetPoint - attackPoint.position;

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(beam, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = direction.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenSlashing
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenSlashing);
            allowInvoke = false;
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap)
            Invoke("Shoot", timeBetweenSlashs);



    }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToSlash = true;
        allowInvoke = true;
        //animator.SetBool("Fire", false);
    }

}
