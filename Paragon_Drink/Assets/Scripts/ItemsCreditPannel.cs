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
            if (_gameParameters.language == Language.French)
            {
                alternateEnd = "Bah alors, il s'est passé quoi ?";
            } else if (_gameParameters.language == Language.English)
            {
                alternateEnd = "The fuck happened ?";
            }
        } else if (_gameManager.itemsCollected <= 5)
        {
            if (_gameParameters.language == Language.French)
            {
                alternateEnd = "Pas mal, pas mal.";
            } else if (_gameParameters.language == Language.English)
            {
                alternateEnd = "Okaaay, we’re getting somewhere !";
            }
        } else if (_gameManager.itemsCollected == 6)
        {
            if (_gameParameters.language == Language.French)
            {
                alternateEnd = "RAYAYAY BIEN JOUÉ MA GUEULE !!!";
            } else if (_gameParameters.language == Language.English)
            {
                alternateEnd = "Get a life, weirdo.";
            }
        }

        if (_gameParameters.language == Language.French)
        {
            _text.text = "PS : Ah au fait, vous avez collecté " + _gameManager.itemsCollected + " canettes sur 6. " + alternateEnd;
        } else if (_gameParameters.language == Language.English)
        {
            _text.text = "Oh by the way, you collected " + _gameManager.itemsCollected + " cans out of 6. " + alternateEnd;
        }
    }
}
