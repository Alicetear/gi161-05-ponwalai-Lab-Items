using UnityEngine;

public abstract class Items : MonoBehaviour
{

    [field: SerializeField]protected int ItemValue { get; set; }

    public abstract void use(Player player);
    public void Pickup (Player player)
    {
        use(player);
        Destroy(this.gameObject);
    }


}
