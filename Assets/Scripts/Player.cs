using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public float Hp;
    public float Damage;
    public float DoubleDamage;
    public float AtackSpeed;
    public float AttackRange = 2;
    public Enemie closestEnemie = null;

    private float lastAttackTime = 0;
    private bool isDead = false;
    public Animator AnimatorController;
    public GameObject _buttonDoubleAttack;

    private void Start()
    {
        StartCoroutine("DoubleAttackVision");
    }

    private IEnumerator DoubleAttackVision()
    {
        var distance = Vector3.Distance(transform.position, closestEnemie.transform.position);
        if (distance <= AttackRange)
        {
            _buttonDoubleAttack.SetActive(true);
            yield return new WaitForSeconds(1f);
            _buttonDoubleAttack.SetActive(false);
        }
        StopCoroutine("DoubleAttackVision");
    }

    private void Update()
    {

        
        if (isDead)
        {
            return;
        }

        if (Hp <= 0)
        {
            Die();
            return;
        }


        var enemies = SceneManager.Instance.Enemies;
       

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemie = enemies[i];
            if (enemie == null)
            {
               
                continue;
            }

            if (closestEnemie == null)
            {
                
                closestEnemie = enemie;
                continue;
            }

            var distance = Vector3.Distance(transform.position, enemie.transform.position);
            var closestDistance = Vector3.Distance(transform.position, closestEnemie.transform.position);
            if (distance <= AttackRange)
            {
                _buttonDoubleAttack.SetActive(true);
            }
            

            if (distance < closestDistance)
            {
                
                closestEnemie = enemie;
            }

        }
       
        
        
      
    }

    public void Attack()
    {
        var distance = Vector3.Distance(transform.position, closestEnemie.transform.position);
        if (distance <= AttackRange)
        {
            if (closestEnemie != null)
            {

               


                if (Time.time - lastAttackTime > AtackSpeed)
                {

                    transform.LookAt(closestEnemie.transform);
                    transform.transform.rotation =
                        Quaternion.LookRotation(closestEnemie.transform.position - transform.position);

                    lastAttackTime = Time.time;
                    closestEnemie.Hp -= Damage;
                    AnimatorController.SetTrigger("Attack");
                }
            }
        }
        else
        {


            if (Time.time - lastAttackTime > AtackSpeed)
            {

                lastAttackTime = Time.time;
                AnimatorController.SetTrigger("Attack");
                
            }
        }
    }

    public void DoubleAttack()
    {
        var distance = Vector3.Distance(transform.position, closestEnemie.transform.position);
        if (distance <= AttackRange)
        {
            if (closestEnemie != null)
            {

               


                if (Time.time - lastAttackTime > AtackSpeed)
                {

                    transform.LookAt(closestEnemie.transform);
                    transform.transform.rotation =
                        Quaternion.LookRotation(closestEnemie.transform.position - transform.position);

                    
                    
                    lastAttackTime = Time.time;
                    closestEnemie.Hp -= DoubleDamage;
                    AnimatorController.SetTrigger("SuperAttack");
                    
                }
            }
        }
        
    }

        
    
   

    private void Die()
    {
        isDead = true;
        AnimatorController.SetTrigger("Die");

        SceneManager.Instance.GameOver();
    }


}
