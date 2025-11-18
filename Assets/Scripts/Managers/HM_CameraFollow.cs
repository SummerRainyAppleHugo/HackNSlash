using UnityEngine;

public class HM_CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Settings")]
    [Range(-10, 10)] public float height = 0f;
    [Range(-20, -5)] public float zOffset = -10f;
    [Range(-20, 20)] public float angle = 0f;
    public float followSpeed = 5f;

    [Header("Aspect Correction")]
    public bool autoCorrectAspect = true;
    [Range(0.5f, 2f)] public float aspectCorrection = 1.2f;

    private Camera isoCamera;

    void Start()
    {
        // Utilise directement la Main Camera existante
        isoCamera = Camera.main;

        if (isoCamera == null)
        {
            Debug.LogError("Aucune Main Camera trouvée dans la scène !");
            return;
        }

        // Force la caméra à être orthographique (optionnel)
        isoCamera.orthographic = true;

        UpdateCameraPosition();
    }

    void LateUpdate()
    {
        if (isoCamera == null || target == null) return;

        UpdateCameraPosition();
        ApplyAspectCorrection();
    }

    void UpdateCameraPosition()
    {
        Vector3 targetPosition = target.position +
                               new Vector3(0, height, zOffset);

        isoCamera.transform.position = Vector3.Lerp(
            isoCamera.transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        isoCamera.transform.rotation = Quaternion.Euler(angle, 0, 0);
    }

    void ApplyAspectCorrection()
    {
        if (!autoCorrectAspect || !isoCamera) return;

        float angleRad = angle * Mathf.Deg2Rad;
        float correctedSize = height * Mathf.Sin(angleRad) * aspectCorrection;

        isoCamera.orthographicSize = Mathf.Clamp(correctedSize, 5, 30);
    }

    // Méthode pour rafraîchir les paramètres manuellement (optionnel)
    public void RefreshCameraSettings()
    {
        UpdateCameraPosition();
        ApplyAspectCorrection();
    }

    //void LateUpdate() //Version Sans Lerp
    //{
    //    if (instantiatedCamera != null && player != null)
    //    {
    //        instantiatedCamera.transform.position = player.transform.position + positionOffset;
    //    }
    //}
}