using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(PlayerPhysic))]
public class PlayerAttack : MonoBehaviour {
	PlayerInput input;
	PlayerPhysic physic;

	[SerializeField]
	private GameObject shruikenPrefab;
	[SerializeField]
	private float shruikenSpeed, bodyVelocityEffectness, shruikenColddown;
	private float shruikenColddownCount;

	void Awake() {
		input = GetComponent<PlayerInput>();
		physic = GetComponent<PlayerPhysic>();
	}

	void Update() {
		if (shruikenColddownCount > 0) { shruikenColddownCount -= Time.deltaTime; return; }

		if (input.ShootDown) {
			Vector2 direction = input.ShootDirection;
			direction.y *= -1f;
			if (direction == Vector2.zero) direction = physic.Direction == 1? Vector2.right: Vector2.left;

			Vector2 shootSpeed = direction * shruikenSpeed;
			if ((shootSpeed.x > 0 && physic.Velocity.x > 0) || (shootSpeed.x < 0 && physic.Velocity.x < 0)) shootSpeed.x += physic.Velocity.x;
			if ((shootSpeed.y > 0 && physic.Velocity.y > 0) || (shootSpeed.y < 0 && physic.Velocity.y < 0)) shootSpeed.y += physic.Velocity.y;
			direction.Normalize();
			Shruiken.Spawn(
				transform.position + (Vector3) direction,
				shootSpeed);
			shruikenColddownCount = shruikenColddown;
		}
	}
}
