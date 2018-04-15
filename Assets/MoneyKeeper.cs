using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyKeeper : MonoBehaviour {

    static int money = 0;
    public Text t1;
    //public Text t2;
	// Use this for initialization


    public void setMoney() {
        //GemsImage.text = money.ToString();
        t1.text = money.ToString();
        //t2.text = money.ToString();

    }

    public void setCurrentGems(int gems)
    {
        if (gems >= 0) {
            money = money + gems;
        } else if ((gems*-1)<= money) {
            money = money + gems;
        }

        
        

    }

}
