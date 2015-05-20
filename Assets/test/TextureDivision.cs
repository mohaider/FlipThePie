using UnityEngine;
using System.Collections;

public class TextureDivision : MonoBehaviour
{

    public Texture2D source;
    public int ddd;

    // Use this for initialization
    void Start()
    {

        GameObject spritesRoot = GameObject.Find("SpritesRoot");

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Sprite newSprite = Sprite.Create(source, new Rect(i * 128, j * 128, 128, 128), new Vector2(0.5f, 0.5f));
                GameObject n = new GameObject();
                SpriteRenderer sr = n.AddComponent<SpriteRenderer>();
                sr.sprite = newSprite;
                n.transform.position = new Vector3(i * 2, j * 2, 0);
                n.transform.parent = spritesRoot.transform;
            }
        }
    }
}