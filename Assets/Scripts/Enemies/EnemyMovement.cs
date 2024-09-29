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
		[SerializeField]
		private Vector3 raycastOffset;

		private IEnemyMovementSettings _settings;

		private Ray snapRay;
		
		public void Init(IEnemyMovementSettings settings)
		{
			_settings = settings;
		}

		private void FixedUpdate()
		{
			var position = transform.position;

			float dt = Time.deltaTime;
			position.y += dt * _settings.MovementSpeed;

			float snapDistance = _settings.DistanceFromTree;
			float detectionDistance = snapDistance + 1;
			snapRay = new Ray(
				position + raycastOffset - detectionDistance * transform.forward, 
				transform.forward);

			transform.position = position;	
		}

        private void OnDrawGizmosSelected()
        {
			if (Application.isPlaying == false) 
				return;
            
			float snapDistance = _settings.DistanceFromTree;
            float detectionDistance = snapDistance + 1;
            Gizmos.color = Color.red;
			Gizmos.DrawRay(snapRay.origin, 2 * detectionDistance * snapRay.direction);
			Gizmos.DrawSphere(snapRay.origin, 0.1f);
        }
    }
}
