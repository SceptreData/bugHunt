using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //SerializeField keeps the variables private, but lets you edit them in the Unity editor
    [SerializeField]
    float speed = 0;
    [SerializeField]
    int health = 0;
    [SerializeField]
    float jumpForce = 0;
    [SerializeField]
    Bullet playerShot = null;

    public Text scoreText, gameOverText1, gameOverText2;

    bool movingRight = true;
    bool inAir = false;
    bool firing = false;
    int score = 0;
    bool gameOver = false;
    float timer = 0;

	void Start () {
        scoreText.text = score.ToString();
        gameOverText1.enabled = false;
        gameOverText2.enabled = false;
	}
	
	void Update () {
        if (!gameOver)
        {
            //Checks to see if the player is in the air to prevent air-jumping
            if (Input.GetAxis("Vertical") > 0 && !inAir)
            {
                Jump();
            }

            //Fires one shot per press of button
            if (Input.GetAxis("Fire1") != 0 && !firing)
            {
                firing = true;
                Shoot();
            }
            else if (Input.GetAxis("Fire1") == 0)
            {
                firing = false;
            }
        }

        if (gameOver)
        {
            timer += Time.deltaTime;

            //Prevents player from slamming through game over text accidentally
            if (timer > 1 && Input.GetAxis("Fire1") != 0)
            {
                //Go to score entry
            }
        }
	}

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            //Movement is in FixedUpdate to prevent clipping into walls
            //Grabs horizontal control value. Theoretically works with controllers.
            float horValue = Input.GetAxis("Horizontal");

            //Moves in proper direction. Updates sprite based on direction
            if (horValue > 0)
            {
                transform.Translate(speed, 0, 0);
                movingRight = true;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (horValue < 0)
            {
                transform.Translate(-speed, 0, 0);
                movingRight = false;
                GetComponent<SpriteRenderer>().flipX = false;
            }

            //Air movement
            if (inAir)
            {
                //Moves player upwards based on jumpForce. Can change value in editor
                transform.Translate(0, jumpForce, 0);

                //Raycast to see if player is back on the ground
                int layerMask = 1 << 8;
                if (Physics2D.Raycast(transform.position, Vector2.down, 0.5f, layerMask))
                {
                    inAir = false;
                }
            }
        }

        if (gameOver)
        {
            //Returns player to ground if in air during game over
            if (inAir)
            {
                transform.Translate(0, jumpForce, 0);

                int layerMask = 1 << 8;
                if (Physics2D.Raycast(transform.position, Vector2.down, 0.5f, layerMask))
                {
                    inAir = false;
                }
            }
        }
    }

    void Shoot()
    {
        //Instantiates a new shot, changes direction based on player facing
        Bullet newBullet = Instantiate(playerShot, transform.position, Quaternion.identity);
        newBullet.SetFacingRight(movingRight);
    }

    void Jump()
    {
        //Jumps
        inAir = true;
    }

    //Take damage function. Destroys self when health is 0
    public void TakeDamage (int amount)
    {
        if (!gameOver)
        {
            health -= amount;

            if (health <= 0)
            {
                //Sets game over on all necessary objects
                Camera.main.transform.SetParent(null);
                FindObjectOfType<Hive>().GameOver();
                FindObjectOfType<WaveManager>().GameOver();
                Spawner[] spawners = FindObjectsOfType<Spawner>();
                Enemy[] enemies = FindObjectsOfType<Enemy>();

                foreach (Enemy enemy in enemies)
                {
                    enemy.GameOver();
                }

                foreach (Spawner spawner in spawners)
                {
                    spawner.GameOver();
                }

                Destroy(gameObject);
            }
        }
    }

    public void AddPoints (int amount)
    {
        if (!gameOver)
        {
            score += amount;
            scoreText.text = score.ToString();
        }
    }

    //Sets game over
    public void GameOver ()
    {
        gameOver = true;
        gameOverText1.enabled = true;
        gameOverText2.enabled = true;
    }
}
