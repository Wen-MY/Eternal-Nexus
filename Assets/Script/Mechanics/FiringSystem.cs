using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class FiringSystem : MonoBehaviour
{   //Gun Config
    [Header ("Gun Configuration")]
    public int damage;
    public float timeShooting,recoil, normalSpread,movingSpread, range, timeReload;
    public int magazineSize;
    public bool fullAutoWeapon;
    int bulletsInMagazine;
    float spread;

    //combat state
    public bool shooting;
    public bool ready;
    public bool reloading;

    //reference Setting
    public Camera cam;
    public Transform aimingPoint;
    public RaycastHit rayHit;
    public LayerMask enemies;
    public Rigidbody rb;
    public GameObject Crosshair;
    //Graphics
    //camshake (from camera shake class)
    //gun shooting fx
    public GameObject muzzleFire, impactMark;
    public Animator animator;

    private Vector3 accumulatedRecoil = Vector3.zero;
    public PlayerMovement movement;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    private void Awake() //Initialize the guns
    {
        bulletsInMagazine = magazineSize;
        ready = true;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isReloading", reloading);
        animator.SetBool("IsShooting", shooting);
        takeInput();
    }
   
    private void FixedUpdate()
    {
        debug();
    }

    private void takeInput()
    {
            //check is fullAuto enabled
            if (fullAutoWeapon)
                shooting = Input.GetButton("Fire1");
            else
                shooting = Input.GetButtonDown("Fire1");

            //check reload ability
            if (Input.GetButtonDown("Reload") && bulletsInMagazine < magazineSize && !reloading)
            {
                Reload();
            
            }

            //check shooting ability
            if ( ready && shooting && !reloading && bulletsInMagazine > 0)
            {
                Fire();
            }

    }

    // player action
    private void Fire()
    {
        ready = false; //already start shooting 

        //apply recoil and gun spreads
        Vector3 shootingDirection = cam.transform.forward;
        shootingDirection += applySpread(cam.transform.forward);
        shootingDirection += applyRecoil(shootingDirection);

        //Raycast
        if(Physics.Raycast(cam.transform.position, shootingDirection,out rayHit, range, enemies))
        {
            Debug.Log(rayHit.collider.name);
            Vector3 crosshairScreenPos = cam.WorldToViewportPoint(rayHit.point);
            crosshairScreenPos.z = 0f; // Set Z position to zero so that it's on the canvas plane
            if (rayHit.collider.CompareTag("Enemy"))
            {
                
                rayHit.collider.GetComponent<Bot>().TakeDamage(damage); //this line use to make damage to the enemy who being shotting and aiming by player

            }
            else
            {
                Debug.Log("Raycast hit nothing.");
            }
        }
         else
    {
        Debug.Log("Raycast hit nothing.");
    }
        //shakeCameraHere

        //Graphics 
        //impact marks effect
        GameObject impactObj = Instantiate(impactMark, rayHit.point, Quaternion.Euler(0, 0, 0));
        impactObj.transform.localScale = Vector3.one *0.1f; // Reset scale to 1 before scaling down
        Destroy(impactObj,5f);

        //muzzle fire effect
        GameObject muzzleFlash = Instantiate(muzzleFire, aimingPoint.position, Quaternion.identity);
        Destroy(muzzleFlash, 0.2f);
        bulletsInMagazine--;
        Invoke("resetFire", timeShooting); //make the time gaps between each bullet shot from gun

    }
    private void resetFire()
    {
        ready = true;
        if (!shooting)
        {
            accumulatedRecoil = Vector3.zero;
        }
    }
    private void Reload()
    {
        Debug.Log("Reloading...");
        accumulatedRecoil = Vector3.zero;
        reloading = true;
        Invoke("resetReload", timeReload);
    }
    private void resetReload()
    {
        bulletsInMagazine = magazineSize;
        reloading = false;
    }

    //gun mechanism
    //recoil still have problem
    private Vector3 applyRecoil(Vector3 shootingDirection)
    {

        //accumulatedRecoil += recoil * cam.transform.up;
        if (movement.currentState == PlayerMovement.MovementState.crouching)
            accumulatedRecoil += recoil * cam.transform.up * 0.5f;
        else
            accumulatedRecoil += recoil * cam.transform.up;
        shootingDirection += accumulatedRecoil;
        return shootingDirection;
    }
    private Vector3 applySpread(Vector3 shootingDirection)
    {
        // if player is moving then spread become more higher else is normal when standing
        switch (movement.currentState)
        {

            case PlayerMovement.MovementState.crouching:
                spread = normalSpread * 0.5f;
                break;
            case PlayerMovement.MovementState.sprinting:
                spread = movingSpread;
                break;
            default:
                spread = normalSpread;
                break;

        }
        shootingDirection += Random.Range(-spread, spread) * cam.transform.right;
        return shootingDirection;
    }




    public void debug()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.forward * range, Color.red, 0.1f);
    }
}
