using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
	public GameObject go_manager;
	EventManager manager;

	public Camera frustum;
	public LayerMask mask;
	Ray debug_ray;

    void Start()
    {
		manager = go_manager.GetComponent<EventManager>();
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
					if (hit.collider.gameObject.CompareTag("Oldman"))
					{
                        if (!this.gameObject.GetComponent<Policeman>())
                        {
							// call event
							manager.events.Add(new PerceptionEvent(this.gameObject, SENSE.VISION, TYPE.SPOT));
						}

						Debug.DrawRay(ray.origin, ray.direction * frustum.farClipPlane, Color.red);
					}
				}
			}
		}
	}
}
