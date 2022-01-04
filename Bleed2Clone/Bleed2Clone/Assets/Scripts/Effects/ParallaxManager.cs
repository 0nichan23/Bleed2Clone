using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Sprite[] foregrounds;
    [Range(0f, 1f)]
    [SerializeField] private float[] backgroundParallaxAmounts;
    [Range(-1f, 0f)]
    [SerializeField] private float[] foregroundParallaxAmounts;
    [SerializeField] private string BGSortingLayerName;
    [SerializeField] private string FGSortingLayerName;

    public void GenerateParallaxGameobjects()
    {
        if(foregroundParallaxAmounts.Length < foregrounds.Length || backgroundParallaxAmounts.Length < backgrounds.Length)
        {
            Debug.LogError("Not enough parallax amounts in 'background / foreground parallax amounts' to generate the parallax gameobjects");
            return;
        }

        DestroyChildrenInEditor(this.transform);
        CreateParallaxGameobjects(true);
        CreateParallaxGameobjects(false);
    }
    
    private void DestroyChildrenInEditor(Transform trans)
    {
        for (int i = trans.transform.childCount; i > 0; --i)
            DestroyImmediate(trans.transform.GetChild(0).gameObject, false);
    }
    
    private void CreateParallaxGameobjects(bool BG_FG)
    {
        Vector3 camPos = Camera.main.transform.position;
        camPos.z = 0;
        transform.position = camPos;
        int loopInt = BG_FG ? backgrounds.Length : foregrounds.Length;

        for (int i = 0; i < loopInt; i++)
        {
            GameObject newParallax = new GameObject();
            newParallax.name = BG_FG? ($"Background {i}") : ($"Foreground {i}");
            SpriteRenderer newParallaxSR = newParallax.AddComponent<SpriteRenderer>();
            newParallaxSR.sprite = BG_FG? backgrounds[i] : foregrounds[i];
            newParallaxSR.sortingLayerName = BG_FG ? BGSortingLayerName : FGSortingLayerName;
            newParallaxSR.sortingOrder = loopInt - i;
            newParallax.transform.parent = transform;
            newParallax.transform.localPosition = Vector3.zero;

            for (int c = -1; c < 2; c += 2)
            {
                GameObject childBG = Instantiate(newParallax);
                DestroyChildrenInEditor(childBG.transform);
                childBG.transform.parent = newParallax.transform;
                childBG.name = BG_FG ? ($"Background {i} child {c}") : ($"Foreground {i} child {c}");
                Vector3 offsetPosition = newParallax.transform.position;
                offsetPosition.x += backgrounds[i].bounds.size.x * c;
                childBG.transform.position = offsetPosition;
            }

            ParallaxEffect paraEffect = newParallax.AddComponent<ParallaxEffect>();
            float paraAmount = BG_FG ? backgroundParallaxAmounts[i] : foregroundParallaxAmounts[i];
            paraEffect.InitializeParallax(Camera.main.transform, paraAmount, backgrounds[i].bounds.size.x);

        }
    }
}
