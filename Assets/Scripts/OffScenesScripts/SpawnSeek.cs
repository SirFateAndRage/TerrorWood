using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace checker.spawn
{
    public class SpawnSeek
    {
        EnemySpawner _enemySpawner;
        PlayerEntity _player;
        
        Vector3 desired;

        public SpawnSeek(EnemySpawner e, PlayerEntity p)
        {
            _enemySpawner = e;
            _player = p;
        }

        public Vector3 DesiredVector(int minradius)
        {
            Vector3 _desireMagnitude = Vector3.zero;

            int x = Random.Range(-_enemySpawner.MaxRadius(), _enemySpawner.MaxRadius());
            int z = Random.Range(-_enemySpawner.MaxRadius(), _enemySpawner.MaxRadius());

            _desireMagnitude = new Vector3(x, 0, z) - new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
            

            if (_desireMagnitude.magnitude <= minradius) return Vector3.zero;

            desired += new Vector3(x, 0, z);
            
            
            return desired;
        }

        public void Onupdate()
        {
            desired = _player.transform.position;
        }
    }
}

