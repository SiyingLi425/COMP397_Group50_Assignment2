using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Author: Group 50
Vincent Tse - 301050515
Siying Li - 301054781
Derek Chan - 301021992
Last Modified: April 14, 2021
Description: Manages player controls and properties such as hp.
 */
public enum PaladinState
{
    IDLE,
    RUN,
    JUMP,
    SLASH,
    DEATH
}

public class PlayerBehaviour : MonoBehaviour
{
    public int animState;
    [Header("Controllers")]
    public Joystick joystick;
    public float horizontalSensitivity;
    public float verticalSensitivity;
    public CharacterController controller;

    [Header("Movement Settings")]
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundRadius = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    public GameController gameController;

    [Header("Audio Sources")]
    public AudioSource jump;
    private AudioSource _jump;
    public AudioSource slash;
    public AudioSource deathScream;
    public AudioSource hurt;


    [Header("UI")]
    public GameObject inventoryUI;
    private bool inventoryActive = false;
    public bool isTalking = false;
    public GameObject questUI;
    public bool questActive = false;


    [Header("Armor")]
    public bool gotShield = false;
    public GameObject shield;

    public bool isNearSword = false;
    public bool gotSword = false;
    public GameObject sword;
    public GameObject swordItem;

    [Header("Animation")]
    public Animator animator;

    [Header("HealthBar")]
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currentHealth;
    public int healAmount = 50;

    public Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    private bool isAttacking;
    private bool isDead;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        _jump = gameController.audioSources[(int)SoundClip.JUMP];
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;

        questUI.SetActive(questActive);

        inventoryUI.SetActive(inventoryActive);
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);


    }


    void FixedUpdate()
    {
        animState = animator.GetInteger("AnimState");
        if(animState == 2)
        {
            Debug.Log("jump animation");
        }
        if (isTalking)
        {
            animator.SetInteger("AnimState", (int)PaladinState.IDLE);
            return;
        }

        //if (isAttacking) return;

        //if (gotSword && Input.GetMouseButton(0) && !isAttacking && isGrounded)
        //{
        //    StartCoroutine(Slash());
        //    return;
        //}


        if (!isAttacking && !isDead)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

            if (isGrounded && velocity.y < 0)
                velocity.y = -2.0f;

            float x = joystick.Horizontal;
            float z = joystick.Vertical;

            if (x == 0 && z == 0 && isGrounded)
            {
                animator.SetInteger("AnimState", (int)PaladinState.IDLE);
            }

            if ((x != 0 || z != 0))
            {
                Vector3 move = transform.right * x + transform.forward * z;
                controller.Move(move * maxSpeed * Time.deltaTime);
                if (isGrounded)
                    animator.SetInteger("AnimState", (int)PaladinState.RUN);
            }


            //if (Input.GetButton("Jump") && isGrounded)
            //{
            //    Debug.Log(animator.GetInteger("AnimState"));
            //    Jump();
            //    Debug.Log(animator.GetInteger("AnimState"));
            //}
        }

        velocity.y += gravity * Time.deltaTime;

        // not on platform
        if (transform.parent == null)
            controller.Move(velocity * Time.deltaTime);




        //// open or close inventory UI
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    inventoryActive = !inventoryActive;
        //    inventory.SetActive(inventoryActive);
        //}

        //if(isNearSword && Input.GetKeyDown(KeyCode.F) && gotSword == false)
        //{
        //    sword.SetActive(true);
        //    swordItem.SetActive(false);
        //    gotSword = true;
        //}



        ////slash
        //if (gotSword && Input.GetMouseButton(0) && isAttacking == false && isGrounded)
        //{
        //    StartCoroutine(Slash());
        //}

        //temp key to use potion before implementing inventory
        //if ((Input.GetKeyDown(KeyCode.H)))
        //{
        //    if (gameController.potionCount > 0)
        //    {
        //        currentHealth += healAmount;
        //        gameController.usePotion();
        //        healthBar.SetHealth(currentHealth);
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    knockBack(70);
        //}


    }
    void Jump()
    {
        if (!isAttacking && !isDead)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            animator.SetInteger("AnimState", (int)PaladinState.JUMP);
            waitJump();

            //play jump audio
            jump.Play();
        }
    }
    void SlashAttack()
    {

        if (gotSword && isAttacking == false && isGrounded)
        {
            StartCoroutine(Slash());
        }
    }

    void pickItem()
    {
        if (isNearSword  && gotSword == false)
        {
            sword.SetActive(true);
            swordItem.SetActive(false);
            gotSword = true;
            inventory.AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1 });
            gameController.questController.toggleWeapon();
        }
    }

    void toggleInventory()
    {

        inventoryActive = !inventoryActive;
        inventoryUI.SetActive(inventoryActive);

    }


    void usePotion()
    {
        if (gameController.potionCount > 0)
        {
            currentHealth += healAmount;
            gameController.usePotion();
            healthBar.SetHealth(currentHealth);
        }
    }
    IEnumerator Slash()
    {
        isAttacking = true;
        animator.SetInteger("AnimState", (int)PaladinState.SLASH);
        slash.PlayDelayed(0.5f);
        yield return new WaitForSeconds(1.0f);
 
        isAttacking = false;
    }

    IEnumerator waitJump()
    {
        animator.SetInteger("AnimState", (int)PaladinState.JUMP);
        yield return new WaitForSeconds(1.0f);
    }

    public void TakeDamange(int damage)
    {
        knockBack(50);
        hurt.Play();
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        //StartCoroutine(Hurt());

        if (currentHealth <= 0 && !isDead )
        {
            StartCoroutine(Death());
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Platform")
            transform.parent = other.gameObject.transform;

        if (other.gameObject.CompareTag("Enemy") && isAttacking == true){
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamange(10);
        }
        if (other.gameObject.CompareTag("Sword"))
        {
            swordItem = other.gameObject;
            isNearSword = true;
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            TakeDamange(5);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Platform")
            transform.parent = null;

        if (other.gameObject.CompareTag("Sword"))
        {
            isNearSword = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy") && isAttacking == true)
        {
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamange(10);
        }

        if (other.gameObject.tag == "Shield")
        {

            //pick up shield
            if (Input.GetKeyDown(KeyCode.F) && gotShield == false)
            {
                Debug.Log("Picking up Shield");
                shield.SetActive(true);
                other.gameObject.SetActive(false);
                gotShield = true;

            }


        }

    }

    public void knockBack(int force)
    {
        Vector3 move = -transform.forward * force;
        //this.GetComponent<Rigidbody>().AddForce(0, 0, -force, ForceMode.Impulse);
        controller.Move(move * Time.deltaTime);

    }
    IEnumerator Death()
    {
        isDead = true;
        animator.SetInteger("AnimState", (int)PaladinState.DEATH);
        deathScream.Play();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void usePotionItem()
    {
        if(gameController.potionCount > 0)
        {
            currentHealth += healAmount;
            usePotion();
        }
    }

    public void SavePlayer()
    {
        Debug.Log("saving");

        
        SaveSystem.SavePlayer(this);

    }
    public GameObject pauseMenuUI;
    public void LoadPlayer()
    {

        

        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;

        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];


        transform.position = position;

        controller.enabled = false;
        controller.transform.position = position;
        controller.enabled = true;




        currentHealth = data.health;
        healthBar.SetHealth(currentHealth);

        if (data.sword)
        {
            
            sword.SetActive(true);            
            gotSword = true;
        }
        gotShield = data.shield;
        if (gotShield)
        {
            shield.SetActive(true);
            
        }


        
        
    }

    public void onJumpButtonPressed()
    {
        if (isGrounded)
        {
            Jump();
        }
    }

    public void onAttackButtonPressed()
    {
        SlashAttack();
    }

    public void onPickItemButtonPressed()
    {
        pickItem();
    }

    public void onInventoryButtonPressed()
    {
        toggleInventory();
    }

    public void onPotionButtonPressed()
    {
        usePotionItem();
    }

    public void toggleQuests()
    {
        questActive = !questActive;
        questUI.SetActive(questActive);
    }
}
