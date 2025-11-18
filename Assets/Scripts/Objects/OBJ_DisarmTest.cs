using UnityEngine;

public class OBJ_DisarmTest : OBJ_Objects
{

    public override void Interact()
    {
        
        Debug.Log("Disarm Test Interacted with!");

        Destroy(gameObject);

    }

}
