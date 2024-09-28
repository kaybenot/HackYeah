using UnityEngine;

namespace Lemurs.Enemies
{
	public interface IEnemyStatsSettings
	{
		int MaxHealth { get; }
	}

	public class EnemyStats : MonoBehaviour
	{
		[SerializeField]
		private int health;
		
		private IEnemyStatsSettings _settings;

		public void Init(IEnemyStatsSettings settings)
		{
			_settings = settings;
		}
	}
}
