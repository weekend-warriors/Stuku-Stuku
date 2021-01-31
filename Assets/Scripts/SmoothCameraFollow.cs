using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform Target;
    public Transform camTransform;
    public Vector3 Offset;
    public float SmoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        Offset = Vector3.Scale(camTransform.position - Target.position, new Vector3(0, 1, 1));
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position + Offset;
        camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

        //transform.LookAt(Target);
    }
}