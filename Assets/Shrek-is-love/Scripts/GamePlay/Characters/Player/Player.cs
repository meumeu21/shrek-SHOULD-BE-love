using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDataPersistence
{
    private PlayerMovement _movement;
    private PlayerJump _jump;
    private PlayerCombat _combat;
    private PlayerAnimationController _animationController;
    private ManaSystem _manaSystem;
    private void Awake()
    {
        _manaSystem = GetComponent<ManaSystem>();
        _animationController = GetComponent<PlayerAnimationController>();

        _movement = GetComponent<PlayerMovement>();
        _jump = GetComponent<PlayerJump>();
        _combat = GetComponent<PlayerCombat>();

        _movement.Initialize(_animationController);
        _combat.Initialize(_manaSystem, _animationController);
        _jump.Initialize(_animationController);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadData(GameData gameData)
    {
        this.transform.position = gameData.PlayerPosition;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.PlayerPosition = this.transform.position;
    }

}
