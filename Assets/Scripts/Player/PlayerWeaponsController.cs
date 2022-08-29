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

	[Inject] private void Construct(DiContainer diContainer)
	{
		playerInput = diContainer.Resolve<PlayerInput>();
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
			mainArmWeapon = Instantiate(mainWeaponStats.prefab, rightHandWeaponPos).GetComponent<Weapon>();
		}
	}

    private void FireAction_canceled(InputAction.CallbackContext obj)
    {
		if (mainArmWeapon) mainArmWeapon.StartAttacking();
	}

    private void FireAction_performed(InputAction.CallbackContext obj)
	{
		if (mainArmWeapon) mainArmWeapon.StopAttacking();
	}
}
