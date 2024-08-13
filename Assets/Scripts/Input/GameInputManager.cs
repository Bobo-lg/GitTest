using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour
{

    public static GameInputManager Instance { get; private set; }

    private GameInputAction _gameInputAction;


    private void Awake()
    {
        Instance = this;
        _gameInputAction ??= new GameInputAction();
    }

    /*����WASD���뷵��ֵ*/
    public Vector2 Movement1 => _gameInputAction.GameInput.Movement.ReadValue<Vector2>();

    /*���������������뷵��ֵ*/
    public Vector2 Movement2 => _gameInputAction.GameInputPlayer1.Movement.ReadValue<Vector2>();


    private void OnEnable()
    {
        _gameInputAction.Enable();
    }

    private void OnDisable()
    {
        _gameInputAction.Disable();
    }

}
