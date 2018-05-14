using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
	public int id {
		get;
		set;
	}
	public string name
	{
		get;
		set;
	}
	public Texture2D iconName
	{
		get;
		set;
	}
	public GameObject meshName
	{
		get;
		set;
	}
}
public class Weapon : Item
{
	public int clipSize
	{
		get;
		set;
	}
	public int damage
	{
		get;
		set;
	}
	public float fireRate
	{
		get;
		set;
	}
	public float weaponRange
	{
		get;
		set;
	}
	public string  ammoType
	{
		get;
		set;
	}

	public Weapon(int id, string name, int clipSize, int damage, float fireRate, float weaponRange, string ammoType, string iconName, string meshName)
	{
		this.id = id;
		this.name = name;
		this.clipSize = clipSize;
		this.damage = damage;
		this.fireRate = fireRate;
		this.weaponRange = weaponRange;
		this.ammoType = ammoType;
		this.iconName = Resources.Load ("Icons/" + iconName) as Texture2D;
		this.meshName = Resources.Load ("Prefabs/" + meshName) as GameObject;


	}
}
