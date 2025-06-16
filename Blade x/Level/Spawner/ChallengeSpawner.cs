using System.Collections.Generic;
using Swift_Blade.Combat.Health;
using System.Collections;
using Swift_Blade.Enemy;
using Swift_Blade.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Swift_Blade.Level
{
    public class ChallengeSpawner : Spawner
    {
        [SerializeField] private float wavePeriod;
        [SerializeField] private float endTimeSecond;
        private float endTimer;
        
        [SerializeField] private ChallengeStageUIView challengeStageUI;
        private ChallengeStageRemainTime challengeStageRemainTime;
        
        [Header("Spawn Position")]
        [SerializeField] private Transform[] spawnPosition;
        
        [Header("Chest info")]
        [SerializeField] private Chest chest;
        [SerializeField] private Transform[] chestPositionList;
        
        private readonly List<BaseEnemy> allEnemyList = new List<BaseEnemy>(40);
        
        private WaitForSeconds countdownWait;
        private WaitForSeconds wavePeriodWait;
        
        private int spawnPositionIndex;
        
        protected override void Start()
        {
            base.Start();
            
            challengeStageRemainTime = new ChallengeStageRemainTime();
            
            wavePeriodWait = new WaitForSeconds(wavePeriod);
            countdownWait = new WaitForSeconds(1f);
            
            challengeStageUI.SetText(Mathf.FloorToInt(endTimeSecond));
            challengeStageRemainTime.SetRemainTime(endTimeSecond);
            
            StartCoroutine(CountdownCoroutine());
        }
        
        protected override IEnumerator Spawn()
        {
            while (isClear == false)
            {
                waveCount %= spawnEnemies.Count;
                
                SpawnInfos waves = spawnEnemies[waveCount++];
            
                for (int j = 0; j < waves.spawnInfos.Length; j++)
                {
                    yield return new WaitForSeconds(waves.spawnInfos[j].delay);
                                        
                    var enemyPrefab = waves.spawnInfos[j].enemy;
                    var newEnemy = Instantiate(enemyPrefab, spawnPosition[spawnPositionIndex++ % spawnPosition.Length].position, Quaternion.identity);
                    
                    newEnemy.GetHealth().AddMaxHealth(CalculateHealthAdditional());
                    
                    allEnemyList.Add(newEnemy);
                    
                    PlaySpawnParticle(newEnemy.transform.position);
                }
                
                yield return wavePeriodWait;
            }
        }
        
        private IEnumerator CountdownCoroutine()
        {
            while (!isClear)
            {
                yield return countdownWait;
                endTimer++;
                                
                challengeStageRemainTime.DecreaseRemainTime();
                challengeStageUI.SetText(challengeStageRemainTime.GetRemainTime());
                
                if (endTimer >= endTimeSecond)
                {
                    TimeOut();
                    yield break; 
                }
                
            }
        }
        
        private void TimeOut()
        {
            isClear = true;
            
            StopAllCoroutines();
            
            CreateChest();
            ClearEnemies();
            challengeStageUI.SetText();
            
            StartCoroutine(LevelClear());
        }

        private void ClearEnemies()
        {
            foreach (var enemy in allEnemyList)
            {
                if (enemy != null)
                {
                    if (enemy.TryGetComponent(out BaseEnemyHealth health) && !health.isDead )
                    {
                        ActionData actionData = new ActionData { damageAmount = 9999 };
                        health.TakeDamage(actionData);
                    }
                }
            }
            
            allEnemyList.Clear();
        }
        
        private void CreateChest()
        {
            foreach(var chestTrm in chestPositionList)
            {
                Instantiate(chest , chestTrm.position, Quaternion.identity);
            }
           
        }
        
    }
}
