using UnityEngine;

namespace Lemurs.Enemies
{
    public interface IEnemyMovementSettings
    {
		float MovementSpeed { get; }
    }

    public class EnemyMovement : MonoBehaviour
	{
		private IEnemyMovementSettings _settings;

		public void Init(IEnemyMovementSettings settings)
		{
			_settings = settings;
		}

		private void Update()
		{
			float dt = Time.deltaTime;
			transform.Translate(0, dt * _settings.MovementSpeed, 0);
		}
	}
}
