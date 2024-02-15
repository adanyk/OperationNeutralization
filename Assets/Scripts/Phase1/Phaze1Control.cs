using UnityEngine;

public class Phaze1Control : MonoBehaviour
{
    [SerializeField] GameObject Kask;
    [SerializeField] GameObject Buty;
    [SerializeField] GameObject Rekawice;
    [SerializeField] GameObject Kamizelka;
    [SerializeField] GameObject Pas;
    [SerializeField] GameObject[] Mps;
    [SerializeField] GameObject Bron;
    [SerializeField] GameObject Miernik;
    private static GameObject kask, buty, rekawice, kamizelka, pas, bron, miernik;
    private static GameObject[] mps;

    [SerializeField] GameObject[] Drawers;
    private static GameObject[] drawers;

    [SerializeField] GameObject Selection;
    [SerializeField] GameObject SceneTansition;
    private static GameObject selection;
    private static GameObject sceneTansition;

    void Start()
    {
        kask = Kask;
        buty = Buty;
        rekawice = Rekawice;
        kamizelka = Kamizelka;
        pas = Pas;
        mps = Mps;
        bron = Bron;
        miernik = Miernik;

        drawers = Drawers;

        selection = Selection;
        sceneTansition = SceneTansition;
    }

    public static void CloseOpenDrawer()
    {
        foreach (var drawer in drawers)
        {
            if (drawer.GetComponent<Drawer>().isOpen)
            {
                drawer.GetComponent<Drawer>().Close();
                break;
            }
        }
    }

    public static void PutOffOtherMask()
    {
        foreach (var mp in mps)
        {
            if (mp.GetComponent<DragAndDrop>().isOnAvatar)
            {
                mp.GetComponent<DragAndDrop>().PutOff();
                break;
            }
        }
    }

    public static void IsAwatarFullyEquiped()
    {
        if (kask.GetComponent<DragAndDrop>().isOnAvatar &&
            buty.GetComponent<DragAndDrop>().isOnAvatar &&
            rekawice.GetComponent<DragAndDrop>().isOnAvatar &&
            kamizelka.GetComponent<DragAndDrop>().isOnAvatar &&
            pas.GetComponent<DragAndDrop>().isOnAvatar &&
            (mps[0].GetComponent<DragAndDrop>().isOnAvatar ||
             mps[1].GetComponent<DragAndDrop>().isOnAvatar ||
             mps[2].GetComponent<DragAndDrop>().isOnAvatar) &&
            bron.GetComponent<DragAndDrop>().isOnAvatar &&
            miernik.GetComponent<DragAndDrop>().isOnAvatar)
        {
            Debug.Log("Avatar fully equiped.");
            StopGame();
            sceneTansition.SetActive(true);
        }
    }

    private static void StopGame()
    {
        selection.SetActive(false);
    }
}
