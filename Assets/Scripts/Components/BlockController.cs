using System;
using System.Collections;
using UnityEngine;

public class BlockController: MonoBehaviour {
    public float randomColorSaturation = 0.92f;
    public float randomColorLightness = 0.86f;
    [SerializeField]
    private float collisionForce = 1.0f;

    private void OnCollisionEnter2D(Collision2D collision) {
        gameObject.layer = (int)Layers.IgnoreCollision;
        SpriteRenderer spriceRenderer = GetComponent<SpriteRenderer>();
        spriceRenderer.color = GetRandomColor();
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Vector2 forcePosition = collision.GetContact(0).point;
        rigidbody.AddForceAtPosition(collision.relativeVelocity * collisionForce, forcePosition, ForceMode2D.Force);
        rigidbody.velocity += collision.relativeVelocity * 0.1f;
    }

    private Color GetRandomColor() {
        return Color.HSVToRGB(UnityEngine.Random.Range(0.0f, 1.0f), randomColorSaturation, randomColorLightness);
    }
}
