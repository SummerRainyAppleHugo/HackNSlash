using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDatas", menuName = "GameDatas/PlayerDatas")]
public class PlayerDatas : ScriptableObject
{

    public static PlayerDatas Instance { get; private set; }

    public string s_Name;
    public string s_Element;
    public string s_Class;

    public int i_Level = 1; //Voir le level Up plus tard
    public int i_XP = 0; //Voir le level Up plus tard

    public int i_maxHealth = 100;
    public int i_currentHealth;
    public float f_moveSpeed = 5f;

    public int i_token = 0;

    // Méthodes
    public void TakeDamage(int damage)
    {
        i_currentHealth = Mathf.Max(0, i_currentHealth - damage);
    }

    public void Heal(int amount)
    {
        i_currentHealth = Mathf.Min(i_maxHealth, i_currentHealth + amount);
    }

    public void AddXP(int amount)
    {
        Debug.Log("XP Gagné : " + amount);
    }

    public void LevelUp()
    {
        // Logique de montée de niveau
        i_Level++;
    }
}
