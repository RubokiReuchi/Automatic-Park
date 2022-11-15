using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
	[SerializeField] EventManager manager;

	[HideInInspector] public bool event_set;

	public Camera frustum;
	public LayerMask mask;
	Ray debug_ray;

    void Start()
    {
		event_set = false;
    }

    void Update()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

		foreach (Collider col in colliders)
		{
			if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
			{
				RaycastHit hit;
				Ray ray = new Ray();
				ray.origin = transform.position;
				ray.direction = (col.transform.position - transform.position).normalized;
				ray.origin = ray.GetPoint(frustum.nearClipPlane);

				if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                {
					if (hit.collider.gameObject.CompareTag("Oldman") && !event_set)
					{
                        if (this.gameObject.GetComponent<BehaviorExecutor>().behavior.ToString() == "Thief (BrickAsset)")
                        {
							// call event
							manager.events.Add(new PerceptionEvent(this.gameObject, hit.collider.gameObject, SENSE.VISION, TYPE.SPOT));
							event_set = true;

							Debug.DrawRay(ray.origin, ray.direction * frustum.farClipPlane, Color.red);
						}
					}
					else if (hit.collider.gameObject.CompareTag("Thief") && !event_set)
					{
						if (this.gameObject.GetComponent<BehaviorExecutor>().behavior.ToString() == "Policeman (BrickAsset)")
						{
							// call event
							manager.events.Add(new PerceptionEvent(this.gameObject, hit.collider.gameObject, SENSE.VISION, TYPE.SPOT));

							Debug.DrawRay(ray.origin, ray.direction * frustum.farClipPlane, Color.yellow);
						}
					}
					else if (hit.collider.gameObject.CompareTag("Bench") && !event_set)
					{
						if (this.gameObject.GetComponent<Oldman>())
						{
							// call event
							manager.events.Add(new PerceptionEvent(this.gameObject, hit.collider.gameObject, SENSE.VISION, TYPE.SPOT));
							event_set = true;

							Debug.DrawRay(ray.origin, ray.direction * frustum.farClipPlane, Color.green);
						}
					}
				}
			}
		}
	}
}
