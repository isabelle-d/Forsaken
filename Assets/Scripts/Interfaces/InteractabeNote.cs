using UnityEngine;

public class InteractabeNote : MonoBehaviour
{
    [SerializeField] private GameObject display;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            display.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            display.SetActive(false);
        }
    }
}
