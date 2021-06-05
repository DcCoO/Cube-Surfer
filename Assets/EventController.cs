using UnityEngine;
using UnityEngine.Events;

public class EventController : SingletonMonoBehaviour<EventController>
{
    [SerializeField] UnityEvent onStartGame;
    [SerializeField] UnityEvent onGameOver;
    [SerializeField] UnityEvent onVictory;

    public void OnStartGame() => onStartGame?.Invoke();
    public void OnGameOver() => onGameOver?.Invoke();
    public void OnVictory() => onVictory?.Invoke();
}
