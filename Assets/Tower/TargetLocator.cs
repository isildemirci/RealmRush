using Enemy;
using UnityEngine;

namespace Tower
{
    public class TargetLocator : MonoBehaviour
    {
        [SerializeField] private Transform weapon;
        [SerializeField] private ParticleSystem projecttileParticles;
        [SerializeField] private float range = 15f;
        private Transform target;

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

            target = closestTarget;
        }
        
        void AimWeapon()
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            weapon.LookAt(target);

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
            var emissionModule = projecttileParticles.emission;
            emissionModule.enabled = isActive;
        }
    }
}
 