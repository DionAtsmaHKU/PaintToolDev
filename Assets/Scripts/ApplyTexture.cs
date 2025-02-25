using UnityEngine;

public class ApplyTexture : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            var tex = SaveLoad.Instance.LoadImage();
            var mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            spriteRenderer.sprite = mySprite;
        }
    }
}
