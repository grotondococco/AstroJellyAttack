using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera: MonoBehaviour
{
    [SerializeField] protected Transform m_Focus_Transform = default;
    [SerializeField] protected Transform m_Transform = default;

    protected readonly float smoothSpeed = 0.125f;
    [SerializeField] protected Vector3 offset = default;
    protected virtual void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(m_Focus_Transform.position.x, m_Focus_Transform.position.y, -10) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        m_Transform.position = smoothedPosition;
    }

  
}
