using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public enum carryingSlotType { Empty = 0, Log = 1, Sticks = 2 }
public enum itemType { Log = 0, Stick = 1 }


[System.Serializable]
public struct CarryingSlot {
	public carryingSlotType _SlotType;
	public GameObject _SlotGameObject;

	public bool _Full;
	public int _StickAmount;

	public GameObject _Log;
	public GameObject[] _sticks;
}

public class CharController : MonoBehaviour {
	public static bool _InMenu;

	public Animator _CharacterAnimator;
	public Rigidbody _RigidBody;
	public Transform _CharacterTransform;

	public float _WalkSpeed = 1;
	public float _TurnSpeed = 1;

	public float _inputX;
	public float _inputY;

	[Header("Animation parameters")]
	public float _WalkAnimationSpeed = 1;
	public AudioSource _Audiosource;
	public AudioClip _PickupSoundLog;
	public AudioClip _PickupSoundStick;
	public AudioClip _FootStep;

	[Header("Inventory parameters")]
	public GameObject _SlotParent;
	public float _CarryWeight;
	public CarryingSlot[] _CarrySlots = new CarryingSlot[3];


	void Update() {
		//_RigidBody.AddForce(_CharacterTransform.forward, ForceMode.Impulse);

		if (!_InMenu) {
			if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
				Vector3 newForward = new Vector3(0 - Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

				if (newForward != Vector3.zero) {
					Quaternion qf = Quaternion.LookRotation(newForward);
					float newRotation = Mathf.LerpAngle(_CharacterTransform.localEulerAngles.y, qf.eulerAngles.y, Time.deltaTime * _TurnSpeed);
					_CharacterTransform.localEulerAngles = new Vector3(0, newRotation, 0);
				}
				_RigidBody.AddForce((newForward * _WalkSpeed) * Time.deltaTime, ForceMode.Impulse);
			}
			_CharacterAnimator.SetFloat("WalkSpeed", _RigidBody.velocity.magnitude * _WalkAnimationSpeed);
		}
	}

	public void UpdateInventory(itemType item, GameObject go) {
		float LogWeight = 1f;
		float StickWeight = 0.1f;

		// what type is the new item
		if (item == itemType.Stick) {

			// is there a slot free?
			bool slotFound = false;
			int foundSlot = 0;
			for (int i = 0; i < _CarrySlots.Length; i++) {
				if (!slotFound) {
					if (_CarrySlots[i]._Full == false) {
						slotFound = true;
						foundSlot = i;
					}
				}
			}
			if (!slotFound) {
				return;
			}

			// activate stick in slot destroy item on ground
			_CarrySlots[foundSlot]._StickAmount++;
			_CarryWeight += StickWeight;

			for (int i = 0; i < _CarrySlots[foundSlot]._sticks.Length; i++) {
				if (i <= _CarrySlots[foundSlot]._StickAmount) {
					_CarrySlots[foundSlot]._sticks[i].SetActive(true);
				}
			}
			_CarrySlots[foundSlot]._SlotType = carryingSlotType.Sticks;

			Destroy(go);
			_CharacterAnimator.SetBool("Carrying", true);

			_Audiosource.PlayOneShot(_PickupSoundStick);

			// if stack is full set slot to full
			if (_CarrySlots[foundSlot]._StickAmount == 25) {
				_CarrySlots[foundSlot]._Full = true;
			}

		}

		// if log
		if (item == itemType.Log) {
			// is there a slot free or is it meant for sticks?
			bool slotFound = false;
			int foundSlot = 0;
			for (int i = 0; i < _CarrySlots.Length; i++) {
				if (!slotFound) {
					if (_CarrySlots[i]._Full == false && _CarrySlots[i]._SlotType == carryingSlotType.Empty) {
						slotFound = true;
						foundSlot = i;
					}
				}
			}
			if (!slotFound) {
				return;
			}

			// activate log in slot destroy item on ground
			_CarrySlots[foundSlot]._Log.SetActive(true);
			_CarrySlots[foundSlot]._SlotType = carryingSlotType.Log;

			_CarryWeight += LogWeight;

			Destroy(go);
			_CharacterAnimator.SetBool("Carrying", true);
			_Audiosource.PlayOneShot(_PickupSoundLog);

			_CarrySlots[foundSlot]._Full = true;
		}
	}

	public void EmptyInventory() {

		GameSystem._GameSystem._FireFuel += _CarryWeight;
		_CarryWeight = 0;

		for (int i = 0; i < _CarrySlots.Length; i++) {
			_CarrySlots[i]._Log.SetActive(false);
			_CarrySlots[i]._StickAmount = 0;
			for (int x = 0; x < _CarrySlots[i]._sticks.Length; x++) {
				_CarrySlots[i]._sticks[x].SetActive(false);
			}
			_CharacterAnimator.SetBool("Carrying", false);

			_CarrySlots[i]._Full = false;
			_CarrySlots[i]._SlotType = carryingSlotType.Empty;
		}
	}
}