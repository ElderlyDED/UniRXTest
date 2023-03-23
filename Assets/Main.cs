using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Button _button;
    [SerializeField] GameObject _cube;

    CompositeDisposable _disposable = new CompositeDisposable();

    public ReactiveProperty<int> ValueProp = new ReactiveProperty<int>();
    public ReactiveProperty<bool> BoolProp = new ReactiveProperty<bool>();
    public BoolReactiveProperty textBool = new BoolReactiveProperty();
    public ReactiveCommand showCube = new ReactiveCommand();
    void Start()
    {
        _button.OnClickAsObservable().Subscribe(value =>
        {
            ValueProp.Value++;
            _text.text = string.Format("n = {0}", ValueProp.Value);
        }).AddTo(_disposable);

        textBool.Subscribe(value => {
            _text.text = value ? "gay" : "noGay";
        }).AddTo(_disposable);

        showCube.Subscribe(value => {
            ShowCube();
        }).AddTo(_disposable);

        ValueProp.Where(v => v > 5).Subscribe(value => {
            showCube.Execute();
        }).AddTo(_disposable);
    }

    void ShowCube()
    {
        _cube.SetActive(true);
    }
}
