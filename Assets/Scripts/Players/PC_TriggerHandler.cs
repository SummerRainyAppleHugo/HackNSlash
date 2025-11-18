using UnityEngine;

public class PC_TriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        OBJ_Objects obj = other.GetComponent<OBJ_Objects>();
        if (obj != null && obj.IsPickupable)
        {
            obj.Pickup();
        }
    }
}
