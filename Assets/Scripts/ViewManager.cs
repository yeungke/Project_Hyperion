using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private static ViewManager _instance;

    [SerializeField] private View _startingView;

    [SerializeField] private View[] _views;

    private View _current;


    private void Awake()
    {
        _instance = this;
    }

    public static View GetView<T>()
    {
        for (int i = 0; i < _instance._views.Length; i++)
        {
            if (_instance._views[i] is View)
            {
                return _instance._views[i];
            }
        }
        return null;
    }

    public static void Show<T>() where T : View
    {
        for (int i = 0; i < _instance._views.Length; i++)
        {
            if (_instance._views[i] is T)
            {
                if (_instance._current != null)
                {
                    _instance._current.Hide();
                }
                _instance._views[i].Show();

                _instance._current = _instance._views[i];
            }
        }
    }


    public static void Show(View view)
    {
        if (_instance._current != null)
        {
            _instance._current.Hide();
        }

        view.Show();

        _instance._current = view;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _views.Length; i++)
        {
            _views[i].Initialize();

            _views[i].Hide();
        }

        if (_startingView != null)
        {
            Show(_startingView);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
