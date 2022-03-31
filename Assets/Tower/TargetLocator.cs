using Enemy;
using UnityEngine;

namespace Tower
{
    public class TargetLocator : MonoBehaviour
    {
        [SerializeField] private Transform weapon;
        [SerializeField] private ParticleSystem projectileParticles;
        [SerializeField] private float range = 15f;
        private Transform _target;

        void Update()
        {
            FindClosestTarget();
            AimWeapon();
        }

        void FindClosestTarget()
        {
            EnemyRoot[] enemies = FindObjectsOfType<EnemyRoot>();
            Transform closestTarget = null;
            float maxDistance = Mathf.Infinity;

            foreach (EnemyRoot enemy in enemies)
            {
                float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

                if (targetDistance < maxDistance)
                {
                    closestTarget = enemy.transform;
                    maxDistance = targetDistance;
                }
            }

            _target = closestTarget;
        }
        
        void AimWeapon()
        {
            float targetDistance = Vector3.Distance(transform.position, _target.position);
            weapon.LookAt(_target);

            if (targetDistance < range)
            {
                Attack(true);
            }
            else
            {
                Attack(false);
            }
        }

        void Attack(bool isActive)
        {
            var emissionModule = projectileParticles.emission;
            emissionModule.enabled = isActive;
        }
    }
}
 