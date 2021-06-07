using UnityEngine;
using UnityEngine.Events;

public class EventController : SingletonMonoBehaviour<EventController>
{
    [SerializeField] UnityEvent onLoadLevel;
    [SerializeField] UnityEvent onStartGame;
    [SerializeField] UnityEvent onGameOver;
    [SerializeField] UnityEvent onGameWin;

    public void OnStartGame() => onStartGame?.Invoke();
    public void OnGameOver() => onGameOver?.Invoke();
    public void OnGameWin() => onGameWin?.Invoke();
}
