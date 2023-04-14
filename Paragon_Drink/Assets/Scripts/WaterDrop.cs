using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    [SerializeField] private ParticleSystem dropParticles;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> dropSprites;

    private Collider2D _collider;
    private float enableCollTimer = 0.5f;

    public void Initialize()
    {
        Sprite randomSprite = dropSprites[Random.Range(0, dropSprites.Count)];
        spriteRenderer.sprite = randomSprite;

        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;

        StartCoroutine(EnableCollider());
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(enableCollTimer);

        _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CreateDropParticles();
        Destroy(gameObject);
    }

    private void CreateDropParticles()
    {
        ParticleSystem newParticles = Instantiate(dropParticles, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(newParticles.gameObject, 0.6f);
    }
}
