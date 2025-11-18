using UnityEngine;

public class Coin : Items
{
    public override void use(Player player)
    {
        if (player)
        {
            player.AddCoin(ItemValue);
        }
    }



}
