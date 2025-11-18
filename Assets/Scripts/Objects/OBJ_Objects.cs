using UnityEngine;

public class OBJ_Objects : MonoBehaviour
{

    [SerializeField] private bool _isPickupable = false;
    [SerializeField] private bool _isInteractable = false;
    [SerializeField] private float _interactionRange = 2f;

    // Propriétés pour accéder aux booléens depuis les classes enfants
    public bool IsPickupable => _isPickupable;
    public bool IsInteractable => _isInteractable;
    public float InteractionRange => _interactionRange;
    public virtual void Pickup()
    {
        if (!_isPickupable) return;
        //Destroy(gameObject); // (Optionnel)
    }

    // Méthode pour l'Interaction (appelée par un script externe ou un événement UI)
    public virtual void Interact()
    {
        if (!_isInteractable) return;
    }
}
