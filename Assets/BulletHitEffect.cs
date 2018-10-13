using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitEffect : MonoBehaviour {

    //Fields for maximum speed and rotation. Can be changed in the editor
    [SerializeField]
    float minHorSpeed = 0;
    [SerializeField]
    float maxHorSpeed = 0;
    [SerializeField]
    float maxVerSpeed = 0;
    [SerializeField]
    float maxRot = 0;
    [SerializeField]
    float maxLifeTime = 0;

    float horSpeed, verSpeed, rot, lifeTime;

	void Start () {
        lifeTime = 0;
	}
	
	void Update () {
        //Moves effect around
        transform.Translate(horSpeed, verSpeed, 0, Space.World);
        transform.Rotate(0,0,rot);
        lifeTime += Time.deltaTime;

        //Destroys effect at end of intended lifetime
        if (lifeTime >= maxLifeTime)
        {
            Destroy(gameObject);
        }
	}

    //The bool here takes true as facing right, false as facing left
    public void GenerateMovement(bool facing)
    {
        horSpeed = Random.Range(minHorSpeed, maxHorSpeed);
        verSpeed = Random.Range(-maxVerSpeed, maxVerSpeed);
        rot = Random.Range(-maxRot, maxRot);

        if (!facing)
        {
            horSpeed = horSpeed * (-1);
        }
    }
}
