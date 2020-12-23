using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    public KeyCode forward = KeyCode.W;
    public KeyCode back = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public KeyCode shootUp = KeyCode.UpArrow;
    public KeyCode shootLeft = KeyCode.LeftArrow;
    public KeyCode shootRight = KeyCode.RightArrow;
    public KeyCode shootDown = KeyCode.DownArrow;

    public KeyCode shootUpAlt = KeyCode.I;
    public KeyCode shootLeftAlt = KeyCode.J;
    public KeyCode shootRightAlt = KeyCode.K;
    public KeyCode shootDownAlt = KeyCode.L;

    public KeyCode shoot = KeyCode.Space;
    public LivesUI livesUI;

    public int startingLives;
    public float immunityTime = 1.0f;
    bool isImmune;

    PlayerAudio playerAudio;

    private int lives;
    public int Lives
    {
        get { return lives; }
        set
        {
            lives = value;
            livesUI.Lives = lives;
            if (lives <= 0)
            {
                GameManager.Instance.Die();
            }
        }
    }

    public Transform leftGun, rightGun;
    public float fireRate;
    int whichGun;
    bool canShoot;

    public GameObject bullet;

    public float forwardSpeed;

    Rigidbody rb;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isImmune = false;
        Lives = startingLives;
        canShoot = true;
        rb = GetComponent<Rigidbody>();

        playerAudio = GetComponent<PlayerAudio>();

        if(HighscoreManager.Instance.alternateControls)
        {
            shootUp = shootUpAlt;
            shootDown = shootDownAlt;
            shootLeft = shootLeftAlt;
            shootRight = shootRightAlt;
        }
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        #region movement
        if (Input.GetKey(forward))
        {
            transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
        }
        if (Input.GetKey(back))
        {
            transform.position -= Vector3.forward * forwardSpeed * Time.deltaTime;
        }
        if (Input.GetKey(left) )
        {
            transform.position -= Vector3.right * forwardSpeed * Time.deltaTime;
        }
        if (Input.GetKey(right))
        {
            transform.position += Vector3.right * forwardSpeed * Time.deltaTime;
        }
        #endregion

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -19.0f, 19.0f), transform.position.y, Mathf.Clamp(transform.position.z, -9.0f, 9.0f));

        #region KeyDownRotation
        if (Input.GetKeyDown(shootUp))
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        if (Input.GetKeyDown(shootDown))
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        if (Input.GetKeyDown(shootLeft))
        {
            transform.rotation = Quaternion.AngleAxis(270, Vector3.up);
        }
        if (Input.GetKeyDown(shootRight))
        {
            transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
        }
        #endregion

        if (Input.GetKey(shootUp) || Input.GetKey(shootDown) || Input.GetKey(shootLeft) || Input.GetKey(shootRight))
        {
            Shoot();
        }

        #region KeyUpCompensation (yuck)
        if (Input.GetKeyUp(shootUp) || Input.GetKeyUp(shootDown) || Input.GetKeyUp(shootLeft) || Input.GetKeyUp(shootRight))
        {
            if (Input.GetKey(shootUp))
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
            if (Input.GetKey(shootDown))
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }
            if (Input.GetKey(shootLeft))
            {
                transform.rotation = Quaternion.AngleAxis(270, Vector3.up);
            }
            if (Input.GetKey(shootRight))
            {
                transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
            }
        }
        #endregion
    }

    void Shoot()
    {
        if (!canShoot) { return; }
        canShoot = false;

        playerAudio.laserSound.Play();

        if (whichGun == 0)
        {
            Instantiate(bullet, leftGun.position, leftGun.rotation);
        }
        else
        {
            Instantiate(bullet, rightGun.position, rightGun.rotation);
        }

        StartCoroutine(ShootDelay());
    }


    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1.0f / fireRate);
        canShoot = true;
        whichGun++; whichGun %= 2;
    }

    public void TakeDamage(int damage)
    {
        if (!isImmune)
        {
            GameManager.Instance.CameraShake(0.5f);
            isImmune = true;
            playerAudio.playerHitSound.Play();
            StartCoroutine(TakeDamageC());
            Lives -= damage;
        }
    }

    public void TakeTimeDamage(float damage)
    {
        if (!isImmune)
        {
            GameManager.Instance.CameraShake(0.5f, 0.75f);
            playerAudio.timeDrainedSound.Play();
            GameManager.Instance.CurrentTimePoints -= damage;
        }
    }

    IEnumerator TakeDamageC()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        
        for (int i = 0; i < 5; i++)
        {
            foreach(Renderer r in renderers)
            {
                r.enabled = false;
            }
            yield return new WaitForSeconds(immunityTime / 10);
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
            yield return new WaitForSeconds(immunityTime / 10);
        }

        isImmune = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.GetComponent<Pickup>().OnPickup();
        }
    }
}
