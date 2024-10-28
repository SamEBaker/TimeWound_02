using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    public Rigidbody rb;
    //public float playerSpeed;
    public float rotSpeed;
    public float currSpeed = 5f;
    public float sprintSpeed = 5f;
    public StaminaController staminaController;
    public Vector2 movement;
    public bool sprint = false;
    float horiz;
    float vert;
    public InputSystemUIInputModule m;
    //public InputSystem uiMod;
    [Header("Player Index")]
    [SerializeField]
    private int playerindex = 0;



    public Rect Twoplayer;
    public Rect Threeplayer;
    public Rect fourPlayer;
    //public HUDManager hud;
    //public GameManager gm;
    [Header("Player Info")]
    public int playerHealth = 100;
    public int playerAmmo = 10;
    public int playerGold = 0;
    public int PadKey;
    public  bool IsDead;
    public Camera myCam;

    [Header("Player Stuff")]

    public GameObject RespawnPoint;
    public GameObject GearObj;
    public GameObject MarkerObj;
    public GameObject spawnPt;
    public GameObject bulletObj;
    public Transform barrel;
    public HUDManager hud;
    private Vector2 moveInput = Vector2.zero;
    private Vector2 RotInput = Vector2.zero;
    public GameManager gm;



    [Header("PlayerInventory")]
    public int Gear = 0;
    public List<bool> HasKey;

    [Header("UI Stuff")]
    public GameObject DeathScreen;
    public GameObject DeathUI;
    public GameObject ExitUI;

    [Header("Bullet Stuff")]
    public float shootCooldown = 0.5f; // Time in seconds between shots
    private float lastShootTime = 0f;
    //If player has gears setactive sprite in inventory with child of text displaying amount
    //same for sword
    //equipscript to player and target to object to drop but set drop to invenmtory  button not keybind





    void Start()
    {
        staminaController = GetComponent<StaminaController>();
        rb = GetComponent<Rigidbody>();
        
       // rb.freezeRotation = true;
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        //OnMoveInput(moveInput.x, moveInput.y);
    }
    public void OnRotate(InputAction.CallbackContext ctx)
    {
        RotInput = ctx.ReadValue<Vector2>();
    }
    public void Drop()
    {
        if (Gear >= 1)
        {
            Instantiate(GearObj, spawnPt.transform.position, Quaternion.identity);
            UseItem();
        }
        else
        {
            hud.FlashGearError();
        }

    }

    public void OnPlaceMarker()
    {
            Instantiate(MarkerObj, spawnPt.transform.position, Quaternion.identity);
    }
    public void OnNoButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        if (gm.totalPlayers == 2)
        {
            myCam.rect = Twoplayer;
        }
        else if (gm.totalPlayers == 3)
        {
            myCam.rect = Threeplayer;
        }
        else if(gm.totalPlayers == 4)
        {
            myCam.rect = fourPlayer;
        }

        if (IsDead)
        {
            playerHealth = 100;
            playerGold = 0;
            IsDead = false;

        }

        if (sprint == true)
        {
            if (staminaController.playerStamina > 5f)
            {
                staminaController.isSprinting = true;
                staminaController.Sprinting(); ;
                staminaController.UpdateStamina(1);
                currSpeed = 10f;
            }
            else if (staminaController.playerStamina <= 5f)
            {
                sprint = false;
                staminaController.isSprinting = false;
                staminaController.UpdateStamina(0);
                currSpeed = 5f;
            }
        }
        else
        {
            staminaController.isSprinting = false;
            staminaController.UpdateStamina(0);
            currSpeed = 5f;
        }

        Vector3 forward = transform.forward; // This gives you the direction the player is facing
        Vector3 right = transform.right; // Right direction for left and right movement

        // Use the input to determine the movement direction
        Vector3 moveDir = forward * moveInput.y + right * moveInput.x; // Adjust movement direction based on input

        // Normalize the movement vector and apply speed
        if (moveDir.magnitude > 1)
        {
            moveDir.Normalize();
        }

        // Update the player's velocity
        rb.velocity = moveDir * currSpeed;

        // Rotate the player based on input
        if (RotInput != Vector2.zero) // If there's rotation input
        {
            float rotationY = RotInput.x; // Assume this is the yaw rotation input
            transform.Rotate(Vector3.up * rotationY * rotSpeed * Time.deltaTime);
        }
        /*
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);
       // Vector3 moveDir = Vector3.forward * vert + Vector3.right * horiz;

        if (moveDir.magnitude > 0.1f)
        {
            rb.velocity = moveDir * currSpeed;

        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        */
    }
    public int GetPlayerIndex()
    {
        return playerindex;
    }

    public void OnSprint()
    {
        sprint = !sprint;

    }
    public void OnMoveInput(float horiz, float vert)
    {
        this.vert = vert;
        this.horiz = horiz;

    }

    public void OnShoot()
    {

        if (Time.time >= lastShootTime + shootCooldown)
        {
            if (playerAmmo > 0)
            {
                GameObject bullet = GameObject.Instantiate(bulletObj, barrel.position, Quaternion.identity);
                BulletController bulletController = bullet.GetComponent<BulletController>();
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
                bulletRB.velocity = this.transform.forward * bulletController.speed;
                playerAmmo -= 1;

                // Update the last shoot time
                lastShootTime = Time.time;
            }
            else
            {
                //Debug.Log("Out of ammo!");
                hud.FlashAmmoError();
            }
        }
        else
        {
            Debug.Log("Shot is on cooldown.");
        } 
        /*
        //RaycastHit hit;
        if (playerAmmo != 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletObj, barrel.position, Quaternion.identity); 
            BulletController bulletController = bullet.GetComponent<BulletController>();
            //GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletController.speed;
            playerAmmo -= 1;
        }
        //code for putting decal on hit object
     
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = Camera.main.transform.position + Camera.main.transform.forward * 25f;
            bulletController.hit = false;
        }
        */
    }
    IEnumerator DeathPopUp()
    {
        DeathUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        DeathUI.SetActive(false);
    }
    public void TakeDamage()
    {
        if(playerHealth <= 0)
        {
            transform.position = RespawnPoint.transform.position;
            StartCoroutine(DeathPopUp());

            IsDead = true;

        }
        else
        {
            playerHealth -=5;
            //flahs red
            //GetComponent<HUDManager>().DisplayplayerHealth();
        }

    }
    public void InteractPopUp(string popUpMessage)
    {
        hud.PopUp.text = popUpMessage; ;
    }


    public void AddGear()
    {
        Gear += 1;
        hud.DisplayGears();
    }
    public void GetKey(int index)
    {
        HasKey[index] = true;
        hud.DisplayKey(index);
    }
    public void LoseKey(int index)
    {
        HasKey[index] = false;
        hud.SetKeyOff(index);
    }

    public void AddAmmo(int AmmoValue)
    {
        playerAmmo += AmmoValue;
        hud.DisplayAmmo();
    }
    public void AddGold(int goldValue)
    {
        playerGold += goldValue;
        gm.TotalAddGold(goldValue);
    }
    public void UseItem()
    {
        //update ui for inventory
        if(Gear != 0)
        {
            Gear -= 1;
            hud.DisplayGears();
        }
        else
        {
            hud.FlashGearError();
        }
    }
}
