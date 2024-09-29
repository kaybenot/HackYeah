using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Lemurs.Enemies
{
	public interface IEnemyStatsSettings
	{
		int MaxHealth { get; }
	}

	public class EnemyBattler : MonoBehaviour
	{
		[SerializeField]
		private int health;

		[SerializeField]
		private EnemyMovement movement;

		public int Health => health;
		
		private IEnemyStatsSettings _settings;

		public void Init(IEnemyStatsSettings settings)
		{
			_settings = settings;
			ResetHealth();
        }

		public void Damage(int damage)
		{
			health -= damage;
		}

		public void Slowdown()
		{
			movement.Slowdown();
		}

		public void ResetHealth()
		{
            health = _settings.MaxHealth;
        }
    }
}
