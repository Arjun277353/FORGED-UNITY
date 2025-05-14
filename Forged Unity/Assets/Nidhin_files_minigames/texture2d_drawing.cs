using UnityEngine;
using UnityEngine.UI;

public class texture2d_drawing : MonoBehaviour
{
    public int textureWidth = 2048;
    public int textureHeight = 2048;
    public int brushSize = 10;
    public Color drawColor = new Color(0, 0, 0, 1);

    private Vector2? lastDrawPosition = null;
    public float spacing = 1f;

    private Texture2D drawTexture;
    private RawImage rawImage;

    void Start()
    {
        drawTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        drawTexture.filterMode = FilterMode.Bilinear;

        ClearTexture();

        rawImage = GetComponent<RawImage>();
        rawImage.texture = drawTexture;

        rawImage.material = new Material(Shader.Find("UI/Unlit/Transparent"));
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rawImage.rectTransform,
                Input.mousePosition,
                null,
                out localPoint
            );

            Rect rect = rawImage.rectTransform.rect;

            float u = (localPoint.x - rect.x) / rect.width;
            float v = (localPoint.y - rect.y) / rect.height;

            int x = Mathf.RoundToInt(u * textureWidth);
            int y = Mathf.RoundToInt(v * textureHeight);

            Vector2 currentPos = new Vector2(x, y);

            if (lastDrawPosition == null)
            {
                DrawAt(x, y);
            }
            else
            {
                Vector2 lastPos = lastDrawPosition.Value;
                float dist = Vector2.Distance(lastPos, currentPos);

                for (float i = 0; i <= dist; i += spacing)
                {
                    Vector2 point = Vector2.Lerp(lastPos, currentPos, i / dist);
                    DrawAt((int)point.x, (int)point.y);
                }
            }

            lastDrawPosition = currentPos;
        }
        else
        {
            lastDrawPosition = null;
        }
    }

    void DrawAt(int centerX, int centerY)
    {
        for (int x = -brushSize; x <= brushSize; x++)
        {
            for (int y = -brushSize; y <= brushSize; y++)
            {
                int drawX = centerX + x;
                int drawY = centerY + y;

                if (drawX >= 0 && drawX < textureWidth && drawY >= 0 && drawY < textureHeight)
                {
                    if (x * x + y * y <= brushSize * brushSize)
                    {
                        drawTexture.SetPixel(drawX, drawY, drawColor);
                    }
                }
            }
        }

        drawTexture.Apply();
    }

    void ClearTexture()
    {
        
        Color[] clearColors = new Color[textureWidth * textureHeight];
        for (int i = 0; i < clearColors.Length; i++)
            clearColors[i] = new Color(0, 0, 0, 0);

        drawTexture.SetPixels(clearColors);
        drawTexture.Apply();
    }
}
