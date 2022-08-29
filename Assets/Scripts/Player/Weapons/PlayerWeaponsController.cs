using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerWeaponsController : MonoBehaviour
{

	public WeaponStats mainWeaponStats;
	private Weapon mainArmWeapon;

	public Transform rightHandWeaponPos;
	public Transform leftHandWeaponPos;

	private PlayerInput playerInput;
	private SoundManager soundManager;

	[Inject] private void Construct(DiContainer diContainer)
	{
		playerInput = diContainer.Resolve<PlayerInput>();
		soundManager = diContainer.Resolve<SoundManager>();
	}

	private void Start()
	{
		var fireAction = playerInput.currentActionMap.FindAction("Fire");
		if (fireAction != null)
        {
			fireAction.performed += FireAction_performed;
			fireAction.canceled += FireAction_canceled;
		}

		if (mainWeaponStats != null)
		{
			//todo: di factory
			mainArmWeapon = Instantiate(mainWeaponStats.prefab, rightHandWeaponPos).GetComponent<Weapon>();
			mainArmWeapon.soundManager = soundManager;
		}
	}


    private void FireAction_performed(InputAction.CallbackContext obj)
	{
		if (mainArmWeapon) mainArmWeapon.StartAttacking();
	}

	private void FireAction_canceled(InputAction.CallbackContext obj)
	{
		if (mainArmWeapon) mainArmWeapon.StopAttacking();
	}
}
