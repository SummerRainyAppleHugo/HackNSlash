using UnityEngine;

public class OBJ_CoinTest : OBJ_Objects
{
    public override void Pickup()
    {
        Debug.Log("Coin picked up!");

        Destroy(gameObject);

    }
}
