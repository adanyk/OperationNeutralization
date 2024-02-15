using TMPro;
using UnityEngine;

public class GunRegulation : MonoBehaviour
{
    public GameObject regulation;
    public int gunPower = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket) && gunPower > 0)
        {
            DecreaseGunPower();
        }

        if (Input.GetKeyDown(KeyCode.RightBracket) && gunPower < 9)
        {
            IncreaseGunPower();
        }
    }

    private void DecreaseGunPower()
    {
        gunPower--;
        regulation.GetComponent<TextMeshProUGUI>().text = gunPower.ToString();
    }

    private void IncreaseGunPower()
    {
        gunPower++;
        regulation.GetComponent<TextMeshProUGUI>().text = gunPower.ToString();
    }

}
