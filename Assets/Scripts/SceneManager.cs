using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SceneManager : MonoBehaviour
{
   
    public TextMeshProUGUI wawe;
    public int currentWawe;
    public static SceneManager Instance;

    public Player Player;
    public List<Enemie> Enemies;
    public List<SmallEnemy> smallEnemies;
    public List<SpacialEnemy> SpecialEnemy;
    public GameObject Lose;
    public GameObject Win;

    private int currWave = 0;
    [SerializeField] private LevelConfig Config;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       
        SpawnWave();
    }

    public void AddEnemie(Enemie enemie)
    {
        Enemies.Add(enemie);
       
    }
    public void AddSpecialEnemie(SpacialEnemy spacialEnemy)
    {
        SpecialEnemy.Add(spacialEnemy);
       
    }
    public void AddsmallEnemie(SmallEnemy smallenemie)
    {
        smallEnemies.Add(smallenemie);
       
    }

    public void RemoveEnemie(Enemie enemie)
    {
        Enemies.Remove(enemie);
        if(Enemies.Count == 0)
        {
            SpawnWave();
        }
    }
    public void RemovesmallEnemie(SmallEnemy smallEnemy)
    {
        smallEnemies.Remove(smallEnemy);
        
    }
    public void RemoveSpecialEnemie(SpacialEnemy spacialEnemy)
    {
        SpecialEnemy.Remove(spacialEnemy);
        
    }

    public void GameOver()
    {
        Lose.SetActive(true);
    }

    private void SpawnWave()
    {
        if (currWave >= Config.Waves.Length)
        {
            Win.SetActive(true);
            return;
        }

        var wave = Config.Waves[currWave];
        foreach (var character in wave.Characters)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(character, pos, Quaternion.identity);
           

        }

        currentWawe++;

    }

   
    private void Update()
    {
        wawe.text = "Wawe:" + currentWawe;
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    

}
