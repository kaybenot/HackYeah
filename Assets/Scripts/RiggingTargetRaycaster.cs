using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RiggingTargetRaycaster : MonoBehaviour
{
	[SerializeField]
	private TwoBoneIKConstraint constraint;
	[SerializeField]
	private Vector3 initialTargetOffset; 

	[Header("Surface Detection")]
	[SerializeField]
	private LayerMask detectedLayers;
	[SerializeField]
	private float detectionRange = 1;

	[Header("Movement")]
	[SerializeField]
	private float changeTargetDistance = 0.5f;
	[SerializeField]
	private float movementSpeed = 1;
	[SerializeField]
	private bool useDirection;

	private Transform target, hint;
	private Ray ray;
	private RaycastHit hitInfo;

	[Header("States")]
	[SerializeField]
	private Vector3 targetPosition;

	private void OnEnable()
	{
		target = new GameObject("Target").transform;
		hint = new GameObject("Hint").transform;
		hint.parent = target;
		hint.localPosition = -0.1f * Vector3.up;

		constraint.data.target = target;
		if (useDirection)
			constraint.data.hint = hint;

		DoRaycast();
		targetPosition = hitInfo.point + initialTargetOffset;
		UpdateMovement(float.PositiveInfinity);
	}

	private void FixedUpdate()
	{
		DoRaycast();
		if ((targetPosition - hitInfo.point).sqrMagnitude > changeTargetDistance * changeTargetDistance)
			targetPosition = hitInfo.point;

		UpdateMovement(Time.deltaTime);
	}

	private void DoRaycast()
	{
		ray = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(ray, out hitInfo, detectionRange, detectedLayers) == false)
			return;
	}

	private void UpdateMovement(float dt)
	{
		float moveDistance = dt * movementSpeed;
		target.position = Vector3.MoveTowards(target.position, targetPosition, moveDistance);
	}

	private void OnDisable()
	{
		constraint.data.target = null;
		constraint.data.hint = null;
		if (target)
			Destroy(target.gameObject);
	}

	private void OnValidate()
	{
		if (constraint)
	 		constraint.data.hint = useDirection ? hint : null;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawRay(ray);
		if (hitInfo.collider)
			Gizmos.DrawSphere(hitInfo.point, 0.05f);

		Gizmos.color = Color.cyan;
		if (target)
			Gizmos.DrawSphere(target.position, 0.05f);
	}
}
