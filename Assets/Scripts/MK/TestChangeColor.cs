using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TestChangeColor : MonoBehaviour
{
    public Color targetColor = Color.red; // �����Ϸ��� ����
    public float duration = 2.0f; // ���� ���� �ð�
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
            // DOTween�� ����Ͽ� ������ ������ ����
            //material.DOColor(targetColor, duration);
            //renderer.SetPropertyBlock(mpb);
            StartCoroutine(ChangeColorOverTime(Color.yellow,1f));
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(ChangeColorOverTime(originalColor, 1f));
            //renderer.SetPropertyBlock(null);
            // ���� �������� �ǵ�����
            //material.DOColor(originalColor, duration);
            //renderer.SetPropertyBlock(null);
        }
    }


    private IEnumerator ChangeColorOverTime(Color endColor, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // �ð��� ���� ���� ����
            Color lerpedColor = Color.Lerp(originalColor, endColor, elapsedTime / time);
            Debug.Log(lerpedColor);
            mpb.SetColor(Shader.PropertyToID("_Color"), lerpedColor);
            renderer.SetPropertyBlock(mpb);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� ���� ����
        mpb.SetColor(Shader.PropertyToID("_Color"), endColor);
        renderer.SetPropertyBlock(mpb);
    }

}



