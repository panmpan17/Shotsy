using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class Shruiken : MonoBehaviour {
	private static List<Shruiken> shruikens = new List<Shruiken>();
	private static GameObject prefab;

	public static void Spawn(Vector3 pos, Vector2 vec) {
		if (prefab == null) prefab = Resources.Load<GameObject>("Prefab/Shruiken");

		Shruiken shruiken;
		if (shruikens.Count > 0) {
			shruiken = shruikens[0];
			shruikens.RemoveAt(0);
		} else shruiken = Instantiate(prefab).GetComponent<Shruiken>();

		shruiken.gameObject.SetActive(true);
		shruiken.transform.position = pos;
		shruiken.GetComponent<Rigidbody2D>().velocity = vec;
	}

	private static void PutBack(Shruiken shruiken) {
		shruikens.Add(shruiken);
		shruiken.gameObject.SetActive(false);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Random.Range(-2f, 2f) * 100f));
		StartCoroutine(BounceOut());
	}

	IEnumerator BounceOut() {
		yield return new WaitForSeconds(0.2f);
		Shruiken.PutBack(this);
	}
}
