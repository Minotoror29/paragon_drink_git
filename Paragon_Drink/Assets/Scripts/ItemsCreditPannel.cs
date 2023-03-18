using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCreditPannel : CreditPannel
{
    public override void ActivatePannel()
    {
        base.ActivatePannel();

        string alternateEnd = "";

        if (_gameManager.itemsCollected <= 2)
        {
            alternateEnd = "Bah alors, il s'est passé quoi ?";
        } else if (_gameManager.itemsCollected <= 5)
        {
            alternateEnd = "Pas mal, pas mal.";
        } else if (_gameManager.itemsCollected == 6)
        {
            alternateEnd = "RAYAYAY BS SALE!!!";
        }

        _text.text = "PS : Ah au fait, vous avez collecté " + _gameManager.itemsCollected + " sur 6 machins là, je sais pas c’est quoi. " + alternateEnd;
    }
}
