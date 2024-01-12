using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpecialEnemyFight : MonoBehaviour
{
    public float Damage;
    public float DoubleDamage;
    public float AtackSpeed;
    public float AttackRange = 2;
     public SpacialEnemy closestSpecialEnemie = null;
     
     public GameObject _buttonDoubleAttack;

    private float lastAttackTime = 0;
    private bool isDead = false;
    public Animator AnimatorController;
   
    private IEnumerator DoubleAttackVision()
    {
       
        var distance = Vector3.Distance(transform.position, closestSpecialEnemie.transform.position);
        _buttonDoubleAttack.SetActive(true);
        if (distance <= AttackRange)
        {
            yield return new WaitForSeconds(3f);
            _buttonDoubleAttack.SetActive(false);

        }

        
       StopCoroutine("DoubleAttackVision");
    }
    private void Update()
    {

        
       


        var specialenmeies = SceneManager.Instance.SpecialEnemy;
       

        for (int i = 0; i < specialenmeies.Count; i++)
        {
            
            var spacialEnemy = specialenmeies[i];
            if (spacialEnemy == null)
            {
               
                continue;
            }

            if (closestSpecialEnemie == null)
            {
                
                closestSpecialEnemie = spacialEnemy;
                
                continue;
            }

            var distance = Vector3.Distance(transform.position, spacialEnemy.transform.position);
            var closestDistance = Vector3.Distance(transform.position, closestSpecialEnemie.transform.position);
            if (distance <= AttackRange)
            {
                StartCoroutine("DoubleAttackVision");
            }
            

            if (distance < closestDistance)
            {
                
                closestSpecialEnemie = spacialEnemy;
                
            }

        }
       
        
       
        
       
      
    }

   

    public void Attack()
    {
       
        var distance = Vector3.Distance(transform.position, closestSpecialEnemie.transform.position);
        
        
        if (distance <= AttackRange)
        {
            if (closestSpecialEnemie != null)
            {

               


                if (Time.time - lastAttackTime > AtackSpeed)
                {

                    transform.LookAt(closestSpecialEnemie.transform);
                   
                    transform.transform.rotation =
                        Quaternion.LookRotation(closestSpecialEnemie.transform.position - transform.position);
                   

                    lastAttackTime = Time.time;
                    closestSpecialEnemie.Hp -= Damage;
                   
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
        var distance = Vector3.Distance(transform.position, closestSpecialEnemie.transform.position);
        if (distance <= AttackRange)
        {
            if (closestSpecialEnemie != null)
            {

               


                if (Time.time - lastAttackTime > AtackSpeed)
                {

                    transform.LookAt(closestSpecialEnemie.transform);
                    transform.transform.rotation =
                        Quaternion.LookRotation(closestSpecialEnemie.transform.position - transform.position);

                    
                    
                    lastAttackTime = Time.time;
                    closestSpecialEnemie.Hp -= DoubleDamage;
                    AnimatorController.SetTrigger("SuperAttack");
                    
                }
            }
        }
        
    }
}
