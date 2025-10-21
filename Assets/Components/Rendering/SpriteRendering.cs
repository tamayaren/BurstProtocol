using UnityEngine;

public class SpriteRendering : MonoBehaviour
{
    private void Start()
    {
        CameraMain.instance?.rotationChanged.AddListener(UpdateSprites);

        if (CameraMain.instance != null) UpdateSprites(CameraMain.instance.rotation);
    }

    private void UpdateSprites(Quaternion rotation)
    {
        Vector3 euler = rotation.eulerAngles;
        foreach (GameObject sprite in GameObject.FindGameObjectsWithTag("RenderSprite"))
        {
            SpriteRenderer renderer = sprite.GetComponent<SpriteRenderer>();
            if (renderer != null)
                renderer.renderingLayerMask = unchecked((uint)~0);
            sprite.transform.rotation = Quaternion.Euler(euler.x, euler.y, 0f);
            
            GameObject spriteShadow = GameObject.Find("SpriteShadow");
            if (spriteShadow)
                spriteShadow.transform.rotation = Quaternion.Euler(-euler.x, euler.y, 0f);
        }
    }
}
