using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpacialEnemy : MonoBehaviour
{
    public float Hp;
    public float Damage;
    public float AtackSpeed;
    public float AttackRange = 2;
    private UnityEngine.Object smallEnemy;


    public Animator AnimatorController;
    public NavMeshAgent Agent;

    private float lastAttackTime = 0;
    private bool isDead = false;


    private void Start()
    {
        smallEnemy = Resources.Load("SmallGoblin");
        SceneManager.Instance.AddSpecialEnemie(this);
        Agent.SetDestination(SceneManager.Instance.Player.transform.position);
    }

    private void Update()
    {
        if(isDead)
        {
            return;
        }

        if (Hp <= 0)
        {
            Die();
            Agent.isStopped = true;
            return;
        }

        var distance = Vector3.Distance(transform.position, SceneManager.Instance.Player.transform.position);
     
        if (distance <= AttackRange)
        {
            Agent.isStopped = true;
            if (Time.time - lastAttackTime > AtackSpeed)
            {
                lastAttackTime = Time.time;
                SceneManager.Instance.Player.Hp -= Damage;
                AnimatorController.SetTrigger("Attack");
            }
        }
        else
        {
            Agent.SetDestination(SceneManager.Instance.Player.transform.position);
        }
        AnimatorController.SetFloat("Speed", Agent.speed); 
        Debug.Log(Agent.speed);

    }



    private void Die()
    {
        SceneManager.Instance.RemoveSpecialEnemie(this);
        isDead = true;
        AnimatorController.SetTrigger("Die");
        Respawn();
        SceneManager.Instance.Player.Hp++;


    }
    void Respawn()
    {
        GameObject enemyCopy = (GameObject)Instantiate(smallEnemy);
        GameObject enemyCopy1 = (GameObject)Instantiate(smallEnemy);
        enemyCopy.transform.position = transform.position;
        enemyCopy1.transform.position = transform.position;
    }    


}
