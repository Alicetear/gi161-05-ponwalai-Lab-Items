using UnityEngine;

public class Potion : Items
{
    public override void use(Player player)
    {
        if (player)
            player.Heal(ItemValue);
    }
}

