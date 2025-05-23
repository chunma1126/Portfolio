using System.Collections;
using UnityEngine;


namespace Swift_Blade.Level
{
    public class EnemySpawner : Spawner
    {
        private int enemyCount;
        private int enemyCounter;
        
        protected override IEnumerator Spawn()
        {
            if (waveCount >= spawnEnemies.Count) 
                yield break;
            
            enemyCount = 0; 
            enemyCounter = 0;
            
            foreach (var spawnInfo in spawnEnemies[waveCount].spawnInfos)
            {
                yield return new WaitForSeconds(spawnInfo.delay);
                
                var newEnemy = Instantiate(spawnInfo.enemy,
                    spawnInfo.spawnPosition.position,Quaternion.identity);
                newEnemy.GetHealth().OnDeadEvent.AddListener(TryNextEnemyCanSpawn);
                                
                ++enemyCount;
                
                PlaySpawnParticle(newEnemy.transform.position);
            }
            
            ++waveCount;
        }
        
        private void TryNextEnemyCanSpawn()
        {
            ++enemyCounter;
            
            if (waveCount >= spawnEnemies.Count)
            {
                if (enemyCount == enemyCounter)
                {
                    StartCoroutine(LevelClear());
                }
            }
            else
            {
                if (enemyCount == enemyCounter)
                    StartCoroutine(Spawn());
            }
        }
        
    }
}