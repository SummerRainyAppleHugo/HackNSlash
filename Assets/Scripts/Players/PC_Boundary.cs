using UnityEngine;

public class PC_Boundary : MonoBehaviour
{
    public Collider2D playArea;

    private Rigidbody2D rb;
    private Vector2 lastValidPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastValidPosition = rb.position;
        SetPlayArea(playArea);
    }

    public void SetPlayArea(Collider2D newPlayArea)
    {
        if (playArea == null)
        {
            GameObject tileObject = GameObject.Find("Ground");
            if (tileObject != null)
            {
                playArea = tileObject.GetComponent<Collider2D>();
                if (playArea == null)
                {
                    Debug.LogError("Le GameObject 'Ground' n'a pas de Collider2D attaché!");
                }
            }
            else
            {
                Debug.LogError("Aucun GameObject nommé 'Ground' trouvé dans la scène!");
            }
        }
    }


    void Update()
    {
        if (playArea != null && playArea.OverlapPoint(rb.position))
        {
            lastValidPosition = rb.position;
        }
        else
        {
            rb.position = lastValidPosition;
        }
    }
}
