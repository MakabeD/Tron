using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinigame
{
    void StartMinigame();      // Llamado cuando comienza
    void EndMinigame();        // Llamado para terminar (win/lose)
    void OnWin();              // Llamado por minigame cuando gana
    void OnLose();             // Llamado por minigame cuando pierde
}
