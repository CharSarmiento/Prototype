using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public GameObject backgroundPrefab;
    public float parallaxSpeedX = 0.5f;
    public float parallaxSpeedY = 0.1f;

    [HideInInspector] public List<GameObject> backgrounds = new List<GameObject>();
    [HideInInspector] public float spriteRealWidth;
}

public class ParallaxManager : MonoBehaviour
{
    public List<ParallaxLayer> layers = new List<ParallaxLayer>();

    private Camera mainCamera;
    private Vector3 previousCameraPosition;

    void Start()
    {
        mainCamera = Camera.main;
        previousCameraPosition = mainCamera.transform.position;

        foreach (ParallaxLayer layer in layers)
        {
            if (layer.backgroundPrefab == null) continue;

            // Instanciar temporal para medir
            GameObject temp = Instantiate(layer.backgroundPrefab);
            SpriteRenderer sr = temp.GetComponent<SpriteRenderer>();

            // Calcular tamaño real del sprite considerando escala
            layer.spriteRealWidth = sr.bounds.size.x;
            Destroy(temp);

            // Calcular cuántos necesitamos
            float screenWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
            int neededCopies = Mathf.CeilToInt(screenWidth / layer.spriteRealWidth) + 3;

            // Crear copias
            for (int i = 0; i < neededCopies; i++)
            {
                GameObject bg = Instantiate(layer.backgroundPrefab, transform);
                bg.transform.position = new Vector3(
                    mainCamera.transform.position.x - screenWidth / 2f + i * layer.spriteRealWidth,
                    layer.backgroundPrefab.transform.position.y,
                    layer.backgroundPrefab.transform.position.z
                );
                layer.backgrounds.Add(bg);
            }
        }
    }

    void Update()
    {
        Vector3 deltaMovement = mainCamera.transform.position - previousCameraPosition;

        foreach (ParallaxLayer layer in layers)
        {
            foreach (GameObject bg in layer.backgrounds)
            {
                bg.transform.position += new Vector3(
                    -deltaMovement.x * layer.parallaxSpeedX,
                    -deltaMovement.y * layer.parallaxSpeedY,
                    0f
                );
            }

            foreach (GameObject bg in layer.backgrounds)
            {
                if (IsOutOfLeftScreen(bg, layer.spriteRealWidth))
                {
                    float rightMostX = FindRightmostBackground(layer);
                    bg.transform.position = new Vector3(
                        rightMostX + layer.spriteRealWidth,
                        bg.transform.position.y,
                        bg.transform.position.z
                    );
                }
            }
        }

        previousCameraPosition = mainCamera.transform.position;
    }

    private bool IsOutOfLeftScreen(GameObject bg, float spriteWidth)
    {
        float leftBound = mainCamera.transform.position.x - (mainCamera.orthographicSize * mainCamera.aspect) - spriteWidth;
        return bg.transform.position.x < leftBound;
    }

    private float FindRightmostBackground(ParallaxLayer layer)
    {
        float maxX = layer.backgrounds[0].transform.position.x;

        foreach (GameObject bg in layer.backgrounds)
        {
            if (bg.transform.position.x > maxX)
            {
                maxX = bg.transform.position.x;
            }
        }

        return maxX;
    }
}
