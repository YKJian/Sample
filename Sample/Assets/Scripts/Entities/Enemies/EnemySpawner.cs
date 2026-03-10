using Entities.Enemies.Data;
using Infrastructure;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyData[] m_data;
        [SerializeField] private Enemy[] m_enemies;
        [SerializeField] private Transform[] m_spawnPoints;

        private List<Enemy> m_currentEnemies = new();

        public void Spawn()
        {
            var playerTransform = ServiceLocator
                .Resolve<IPlayerFactory>()
                .Create()
                .transform;

            foreach(var spawnPoint in m_spawnPoints)
            {
                var enemy = GetEnemy();
                var enemyData = GetEnemyData();

                var enemyInstance = Instantiate(enemy, spawnPoint);
                enemyInstance.Initialize(enemyData, playerTransform);

                enemyInstance.Died += OnDied;
                m_currentEnemies.Add(enemyInstance);
            }
        }

        public void DespawnAll()
        {
            foreach(var enemy in m_currentEnemies)
            {
                DestroyEnemy(enemy);
            }
            m_currentEnemies.Clear();
        }

        private void OnDied(Enemy enemy)
        {
            m_currentEnemies.Remove(enemy);
            DestroyEnemy(enemy);
        }

        private Enemy GetEnemy() =>
            m_enemies[Random.Range(0, m_enemies.Length)];
        
        private EnemyData GetEnemyData() =>
            m_data[Random.Range(0, m_data.Length)];

        private void DestroyEnemy(Enemy enemy)
        {
            enemy.Died -= OnDied;
            Destroy(enemy.gameObject);
        }
    }
}