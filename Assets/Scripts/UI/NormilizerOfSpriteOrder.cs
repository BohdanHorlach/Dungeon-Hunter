using UnityEngine;

public class NormilizerOfSpriteOrder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Update()
    {
        spriteRenderer.sortingOrder = -(int)transform.position.y;
    }
}
