using UnityEngine;

namespace Lemurs.Enemies
{
    public interface IEnemyMovementSettings
    {
		float MovementSpeed { get; }
		float DistanceFromTree { get; }
		LayerMask TreeLayers { get; }	
    }

    public class EnemyMovement : MonoBehaviour
	{
		private IEnemyMovementSettings _settings;

		public void Init(IEnemyMovementSettings settings)
		{
			_settings = settings;
		}

		private void FixedUpdate()
		{
			float dt = Time.deltaTime;
			transform.Translate(0, dt * _settings.MovementSpeed, 0);

			float snapDistance = _settings.DistanceFromTree;
			float detectionDistance = snapDistance + 1;
			var ray = new Ray(
				transform.position - detectionDistance * transform.forward, 
				transform.forward);

			if (Physics.Raycast(ray, out var hitInfo, 2, _settings.TreeLayers))
			{

			}
		}
	}
}
