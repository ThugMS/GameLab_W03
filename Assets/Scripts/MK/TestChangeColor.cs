using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TestChangeColor : MonoBehaviour
{
    public Color targetColor = Color.red; // 변경하려는 색상
    public float duration = 2.0f; // 변경 지속 시간
    public Renderer renderer;
    public MaterialPropertyBlock mpb;

    private Material material;
    public Color[] TreeColors;
    public Color originalColor;
    private void Awake()
    {

    }
    private void Start()
    {
        mpb = new MaterialPropertyBlock();
        mpb.SetColor(Shader.PropertyToID("_Color"), Color.black);

        
        //material = GetComponent<Renderer>().material;
        renderer = GetComponent<Renderer>();
        //originalColor = renderer.material.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // DOTween을 사용하여 색상을 서서히 변경
            //material.DOColor(targetColor, duration);
            //renderer.SetPropertyBlock(mpb);
            StartCoroutine(ChangeColorOverTime(Color.yellow,1f));
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(ChangeColorOverTime(originalColor, 1f));
            //renderer.SetPropertyBlock(null);
            // 원래 색상으로 되돌리기
            //material.DOColor(originalColor, duration);
            //renderer.SetPropertyBlock(null);
        }
    }


    private IEnumerator ChangeColorOverTime(Color endColor, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // 시간에 따라 색상 보간
            Color lerpedColor = Color.Lerp(originalColor, endColor, elapsedTime / time);
            Debug.Log(lerpedColor);
            mpb.SetColor(Shader.PropertyToID("_Color"), lerpedColor);
            renderer.SetPropertyBlock(mpb);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 색상 설정
        mpb.SetColor(Shader.PropertyToID("_Color"), endColor);
        renderer.SetPropertyBlock(mpb);
    }

}



