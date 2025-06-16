using System.Collections.Generic;
using System.Collections;
using Swift_Blade.Enemy;
using Swift_Blade.Pool;
using UnityEngine;
using System;

namespace Swift_Blade.Level
{
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
    
    public abstract class Spawner : MonoBehaviour
    {
        public PoolPrefabMonoBehaviourSO enemySpawnParticle;
        public PoolPrefabMonoBehaviourSO doorSpawnParticle;
        public SceneManagerSO sceneManager;
        
        [Space]
        public List<SpawnInfos> spawnEnemies;
        
        [Header("Door info")]
        public float dustParticleDelay;
        public Transform[] doorTrm;

        protected WaitForSeconds doorSpawnDelay;
        private bool isSpawnSmall;
        
        [Header("Wave Count")]
        public int waveCount;

        protected bool isClear = false;
        protected bool hasSceneManagerEvent = false;
        protected virtual void Start()
        {
            InitializeParticle();
            
            MonoGenericPool<DustUpParticle>.Initialize(doorSpawnParticle);
            doorSpawnDelay = new WaitForSeconds(dustParticleDelay);
            
            StartCoroutine(Spawn());
        }

        protected void InitializeParticle()
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
        }
        
        protected void PlaySpawnParticle(Vector3 position)
        {
            if (isSpawnSmall)
            {
                SmallSpawnParticle spawnParticle = MonoGenericPool<SmallSpawnParticle>.Pop();
                spawnParticle.transform.position = position;
            }
            else
            {
                EnemySpawnParticle spawnParticle = MonoGenericPool<EnemySpawnParticle>.Pop();
                spawnParticle.transform.position = position;
            }
            
        }
        
        protected IEnumerator LevelClear()
        {
            if (hasSceneManagerEvent)
            {
                Debug.LogError("ERROR: Already Level Clear Event");
                yield break;
            }
            
            hasSceneManagerEvent = true;
            sceneManager.LevelClear();
            
            Node[] newNode = sceneManager.GetNodeList().GetNodes();
            yield return doorSpawnDelay;
                        
            for (int i = 0; i < newNode.Length; ++i)
            {
                var doorPosition = doorTrm[i].position;
                
                DustUpParticle dustUpParticle = MonoGenericPool<DustUpParticle>.Pop();
                dustUpParticle.transform.position = doorPosition;
                
                CreateDoor(newNode[i], doorPosition);
            }
        }

        protected virtual void CreateDoor(Node node, Vector3 doorPosition)
        {
            Door newDoor = Instantiate(node.GetPortalPrefab(), doorPosition, Quaternion.identity);
            newDoor.SetScene(node.nodeName);
            newDoor.UpDoor();
        }
        
        protected abstract IEnumerator Spawn();

        protected float CalculateHealthAdditional()
        {
            int stage = (int)sceneManager.GetNodeList().GetCurrentStageType(); 
            int nodeIndex = sceneManager.GetNodeList().GetCurrentNodeIndex(); 
            
            float baseMin = 1f;
            float baseMax = 2f;
    
            float stageIncrementMin = 3f + stage * 2f; 
            float stageIncrementMax = 5f + stage * 3f; 
    
            float perRoomIncrease = baseMin + ((baseMax - baseMin) / 5f) * nodeIndex; 
    
            float randomStageIncrement = UnityEngine.Random.Range(stageIncrementMin, stageIncrementMax);
            
            float healthAdditional = perRoomIncrease + randomStageIncrement;
            
            //Debug.Log(healthAdditional);
            
            return healthAdditional;
        }
        
    }
}
