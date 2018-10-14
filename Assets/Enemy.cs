using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    protected float speed = 0;
    [SerializeField]
    protected float jumpPower = 0;
    [SerializeField]
    protected int hp = 0;
    [SerializeField]
    protected Bullet shot = null;
    [SerializeField]
    protected int shotDamage = 0;
    [SerializeField]
    protected int shotInterval = 0;
    [SerializeField]
    protected int shotChance = 0;
    [SerializeField]
    protected int score = 0;

    protected float timer = 0;
    protected bool attacking = false;
    public bool inAir = false;
    protected bool gameOver = false;

	void Start () {
		
	}
	
	void Update () {
        
	}

    //Take damage function. Destroys self when health is 0
    public void TakeDamage (int amount)
    {
        if (!gameOver)
        {
            hp -= amount;

            if (hp <= 0)
            {
                FindObjectOfType<Player>().AddPoints(score);
                Destroy(gameObject);
            }
        }
    }

    //Sets whether the enemy is attacking or not
    public void SetAttacking(bool value)
    {
        if (!gameOver)
        {
            timer = 0;
            attacking = value;
        }
    }

    //Jumps
    public void Jump ()
    {
        inAir = true;
    }

    //Sets game over
    public void GameOver ()
    {
        gameOver = true;
    }
}
