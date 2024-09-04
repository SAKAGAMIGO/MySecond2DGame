using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

public class Fade : MonoBehaviour
{

    enum Mode
    {
        FadeIn,
        FadeOut,
    }

    [SerializeField, Header("フェードインの時間")]
    private float _fadeTime;

    [SerializeField, Header("フェードの種類")]
    private Mode _mode;

    [SerializeField, Header("フェードの見た目")]
    private Image _image;

    private bool _bFade;
    private float _fadeCount;
    private UnityEvent _onFageCompiete = new UnityEvent();

    void Start()
    {
        _image = GetComponent<Image>();

        switch (_mode)
        {
            case Mode.FadeIn:
                _fadeCount = _fadeTime;
                break;
            case Mode.FadeOut:
                _fadeCount = 0;
                break;
        }
    }

    void Update()
    {

    }

    private void _Fade()
    {
        if (!_bFade)
        {
            return;
        }

        switch (_mode)
        {
            case Mode.FadeIn:
                FadeIn();
                break;
            case Mode.FadeOut:
                FadeOut();
                break;
        }
        float alpha = _fadeCount / _fadeTime;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
    }

    public void FadeIn()
    {
        _fadeCount -= Time.deltaTime;
        if (_fadeCount <= 0)
        {
            _mode = Mode.FadeOut;
            _bFade = false;
            _onFageCompiete.Invoke();
        }
    }

    public void FadeOut()
    {
        _fadeCount += Time.deltaTime;
        if (_fadeCount >= _fadeTime)
        {
            _mode = Mode.FadeIn;
            _bFade = false;
            _onFageCompiete.Invoke();
        }
    }

    public void FadeStart(UnityAction listener)
    {
        if (_bFade)
        {
            return;
        }
        _bFade = true;
        _onFageCompiete.AddListener(listener);
    }

}
