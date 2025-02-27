using UnityEngine;

public class ApplyTexture : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    private ExampleClass exampleClass = new ExampleClass();

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

[System.Serializable]
public class ExampleClass
{
    public string someData = "DATA";
    protected string name = "ExampleClass";
    private string secret = "I love snacks";
    public string GetName()
    {
        return name;
    }
    public void SetName(string newName)
    {
        name = newName;
    }
    public void Prepare()
    {
        Debug.Log("Preparing...");
    }
    public void DoSomething()
    {
        Debug.Log("DO SOMETHING!");
    }
    private void SetSecret(string newSecret)
    {
        secret = newSecret;
    }
    private string GetSecret()
    {
        return secret;
    }
}
