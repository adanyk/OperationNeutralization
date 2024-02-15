using System.Collections;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public GameObject[] items;
    public Transform[] outsideWaypoints;

    public bool isOpen = false;

    public void Open()
    {
        Phaze1Control.CloseOpenDrawer();
        isOpen = true;
        StartCoroutine(AnimateOpen());
    }
    public void Close()
    {
        isOpen = false;
        StartCoroutine(AnimateClose());
    }

    private IEnumerator AnimateOpen()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetComponent<DragAndDrop>().isOnAvatar == false)
            {
                StartCoroutine(MoveItem(items[i].transform, outsideWaypoints[i].position, .3f));
            }
        }
        yield return null;
    }

    private IEnumerator AnimateClose()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetComponent<DragAndDrop>().isOnAvatar == false)
            {
                StartCoroutine(MoveItem(items[i].transform, items[i].GetComponent<DragAndDrop>().initialPosition, .3f));
            }
        }
        yield return null;
    }

    private IEnumerator MoveItem(Transform itemTransform, Vector3 finalPosition, float duration)
    {
        Vector3 initialPosition = itemTransform.position;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            itemTransform.position = Vector3.Lerp(initialPosition, finalPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        itemTransform.position = finalPosition; // Ensure the final position is set exactly
    }
}
