using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    [SerializeField] private GameObject spell;
    [SerializeField] private GameObject player;
    private GameObject spawnedSpell;
    [SerializeField] private bool isSpawned = false;
    private DamageDealer damageDealer;
    [SerializeField] private float existingTime = 2f;
    private float time;

    public void Start()
    {
        time = existingTime;
    }

    public void Enter()
    {
        if (!isSpawned) {
            Vector3 SpawnLocation = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            spawnedSpell = Instantiate(spell, SpawnLocation, Quaternion.identity);
            isSpawned = true;
            FindObjectOfType<AudioManager>().Play("Magic");
        }
    }
    void Update()
    {

        if (existingTime > 0 && isSpawned)
        {
            existingTime -= Time.deltaTime;
            damageDealer = spawnedSpell.GetComponent<DamageDealer>();
            Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            spawnedSpell.transform.LookAt(playerPosition);

            float distance = Vector3.Distance(player.transform.position, spawnedSpell.transform.position);

            if (distance > 2f)
            {
                spawnedSpell.transform.Translate(Vector3.forward * 30f * Time.deltaTime);
            }
            else
            {
                existingTime = time;
                HitPlayer();
            }
        }
        else
        {
            Destroy(spawnedSpell);
            existingTime = time;
            isSpawned = false;
        }

    }

    private void HitPlayer()
    {
        isSpawned = false;
        damageDealer.Attack();
        Debug.Log("spell hit player");
        Destroy(spawnedSpell);
    }
}
