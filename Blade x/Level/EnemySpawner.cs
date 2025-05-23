using System.Collections.Generic;
using System.Collections;
using Swift_Blade.Enemy;
using Swift_Blade.Pool;
using UnityEngine;
using System;


[Serializable]
public struct SpawnInfo
{
    public BaseEnemy enemy;
    public float delay;
    public Transform spawnPosition;
}

[Serializable]
public struct SpawnInfos
{
    public SpawnInfo[] spawnInfos;
}

namespace Swift_Blade.Level
{
    public class EnemySpawner : MonoBehaviour
    {
        public SceneManagerSO sceneManager;
        public List<SpawnInfos> spawnEnemies;
        
        public PoolPrefabMonoBehaviourSO enemySpawnParticle; 
        
        public int waveCount;
        private int enemyCount;
        private int enemyCounter;
        
        [Header("Door info")]
        public PoolPrefabMonoBehaviourSO doorSpawnParticle;
        public NodeList nodeList;
        public Transform[] doorTrm;
        public float dustParticleDelay;
        
        
        private bool isSpawnSmall;
        
        private void Start()
        {
            if (enemySpawnParticle.GetMono as EnemySpawnParticle)
            {
                MonoGenericPool<EnemySpawnParticle>.Initialize(enemySpawnParticle);
                isSpawnSmall = false;
            }
            else if(enemySpawnParticle.GetMono as SmallSpawnParticle)
            {
                MonoGenericPool<SmallSpawnParticle>.Initialize(enemySpawnParticle);
                isSpawnSmall = true;
            }
            
            MonoGenericPool<DustUpParticle>.Initialize(doorSpawnParticle);
            
            StartCoroutine(Spawn());
        }
        
        private IEnumerator Spawn()
        {
            if (waveCount >= spawnEnemies.Count) 
                yield return null;
            
            enemyCount = 0; 
            enemyCounter = 0;
                        
            foreach (var spawnInfo in spawnEnemies[waveCount].spawnInfos)
            {
                yield return new WaitForSeconds(spawnInfo.delay);
                
                var newEnemy = Instantiate(spawnInfo.enemy,
                    spawnInfo.spawnPosition.position,Quaternion.identity);
                newEnemy.SetOwner(this);
                ++enemyCount;
                
                if (isSpawnSmall)
                {
                    SmallSpawnParticle spawnParticle = MonoGenericPool<SmallSpawnParticle>.Pop();
                    spawnParticle.transform.position = newEnemy.transform.position;
                }
                else
                {
                    EnemySpawnParticle spawnParticle = MonoGenericPool<EnemySpawnParticle>.Pop();
                    spawnParticle.transform.position = newEnemy.transform.position;
                }
            }
               
            ++waveCount;
        }

        public void TryNextEnemyCanSpawn(Vector3 lastEnemyPosition = default , Vector3 rotation = default)
        {
            ++enemyCounter;
            
            if (waveCount >= spawnEnemies.Count)
            {
                if (enemyCount == enemyCounter)
                {
                    StartCoroutine(LevelClear());
                    CreateChest(lastEnemyPosition , rotation);
                }
            }
            else
            {
                if (enemyCount == enemyCounter)
                    StartCoroutine(Spawn());
            }
        }

        private IEnumerator LevelClear()
        {
            sceneManager.LevelClear();
    
            Node[] newNode = nodeList.GetNode();
            
            yield return new WaitForSeconds(dustParticleDelay);
            
            for (int i = 0; i < newNode.Length; ++i)
            {
                var doorPosition = doorTrm[i].position;
                                
                DustUpParticle dustUpParticle = MonoGenericPool<DustUpParticle>.Pop();
                dustUpParticle.transform.position = doorPosition;
                                
                Door.Door newDoor = Instantiate(newNode[i].GetPortalPrefab(), doorPosition, Quaternion.identity);
                newDoor.SetScene(newNode[i].nodeName);
                newDoor.UpDoor();
            }
        }
        
        private void CreateChest(Vector3 lastPosition,Vector3 rotation)
        {
            Chest chest = Instantiate(nodeList.GetChest() , lastPosition , Quaternion.LookRotation(rotation));
        }
        
        
    }
}